using System.Collections.ObjectModel;
using GradeManagement.Core.Enums;
using GradeManagement.Core.Models;
using GradeManagement.Core.Utilities;

namespace GradeManagement.Core.Services;

/// <summary>
/// Provides an in-memory store for student grade records with rich query helpers and validation.
/// </summary>
public sealed class StudentGradeService
{
    private readonly Dictionary<string, int> _students;

    public StudentGradeService()
        : this(new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase))
    {
    }

    /// <summary>
    /// Initializes the service with a preexisting set of student data.
    /// </summary>
    /// <param name="initialData">Dictionary of student names and grade values.</param>
    public StudentGradeService(Dictionary<string, int> initialData)
    {
        _students = initialData ?? throw new ArgumentNullException(nameof(initialData));
    }

    /// <summary>
    /// Returns a snapshot of all tracked student records, ordered alphabetically.
    /// </summary>
    public IReadOnlyCollection<StudentRecord> Students =>
        new ReadOnlyCollection<StudentRecord>(_students
            .OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
            .Select(pair => new StudentRecord(pair.Key, pair.Value))
            .ToList());

    /// <summary>
    /// Adds a new student record to the store.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the student already exists.</exception>
    public StudentRecord AddStudent(string name, int grade)
    {
        ValidateName(name);
        ValidateGrade(grade);

        if (_students.ContainsKey(name))
        {
            throw new InvalidOperationException($"A record for '{name}' already exists. Use UpdateStudent to change the grade.");
        }

        _students[name] = grade;
        return new StudentRecord(name, grade);
    }

    /// <summary>
    /// Updates an existing student's grade.
    /// </summary>
    /// <exception cref="KeyNotFoundException">Thrown when the student does not exist.</exception>
    public StudentRecord UpdateStudent(string name, int grade)
    {
        ValidateName(name);
        ValidateGrade(grade);

        if (!_students.ContainsKey(name))
        {
            throw new KeyNotFoundException($"No student found with the name '{name}'.");
        }

        _students[name] = grade;
        return new StudentRecord(name, grade);
    }

    /// <summary>
    /// Attempts to locate a student record by name.
    /// </summary>
    public bool TryGetStudent(string name, out StudentRecord? student)
    {
        ValidateName(name);

        if (_students.TryGetValue(name, out var grade))
        {
            student = new StudentRecord(name, grade);
            return true;
        }

        student = null;
        return false;
    }

    /// <summary>
    /// Removes a student from the store.
    /// </summary>
    public bool RemoveStudent(string name, out StudentRecord? removed)
    {
        ValidateName(name);

        if (_students.TryGetValue(name, out var grade))
        {
            _students.Remove(name);
            removed = new StudentRecord(name, grade);
            return true;
        }

        removed = null;
        return false;
    }

    /// <summary>
    /// Calculates aggregate statistics using LINQ functions.
    /// </summary>
    public GradeStatistics GetStatistics()
    {
        if (_students.Count == 0)
        {
            return new GradeStatistics(0, 0, Array.Empty<StudentRecord>(), Array.Empty<StudentRecord>());
        }

        var students = _students
            .Select(pair => new StudentRecord(pair.Key, pair.Value))
            .ToList();

        var average = Math.Round(students.Average(student => student.Grade), 2);
        var maxGrade = students.Max(student => student.Grade);
        var minGrade = students.Min(student => student.Grade);

        var highest = students
            .Where(student => student.Grade == maxGrade)
            .OrderBy(student => student.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var lowest = students
            .Where(student => student.Grade == minGrade)
            .OrderBy(student => student.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        return new GradeStatistics(students.Count, average, highest, lowest);
    }

    /// <summary>
    /// Counts the number of students in each grade category.
    /// </summary>
    public IReadOnlyDictionary<GradeCategory, int> GetCategoryCounts()
    {
        if (_students.Count == 0)
        {
            return new ReadOnlyDictionary<GradeCategory, int>(
                Enum.GetValues<GradeCategory>()
                    .ToDictionary(category => category, _ => 0));
        }

        var groups = _students
            .GroupBy(pair => GradeCategoryFactory.FromGrade(pair.Value))
            .ToDictionary(group => group.Key, group => group.Count());

        foreach (var category in Enum.GetValues<GradeCategory>())
        {
            groups.TryAdd(category, 0);
        }

        return new ReadOnlyDictionary<GradeCategory, int>(groups);
    }

    /// <summary>
    /// Removes all student entries.
    /// </summary>
    public void Clear() => _students.Clear();

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Student name cannot be empty.", nameof(name));
        }
    }

    private static void ValidateGrade(int grade)
    {
        if (grade is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");
        }
    }
}

