using System.Diagnostics;
using System.IO;
using System.Linq;
using GradeManagement.Core.Enums;
using GradeManagement.Core.Models;
using GradeManagement.Core.Services;

namespace GradeManagement.ConsoleApp;

internal static class Program
{
    private static readonly StudentGradeService Service = new();

    private static readonly Dictionary<string, Action> MenuActions = new(StringComparer.OrdinalIgnoreCase)
    {
        { "1", AddStudent },
        { "2", UpdateStudent },
        { "3", RemoveStudent },
        { "4", DisplayAllStudents },
        { "5", SearchStudent },
        { "6", ShowStatistics },
        { "7", ShowCategorySummary },
        { "UI", LaunchDesktopUi },
        { "C", ClearAllStudents },
        { "0", ExitApplication }
    };

    private static bool _isRunning = true;

    private static void Main()
    {
        Console.Title = "Student Grade Management - Console";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Welcome to the Student Grade Management System!");
        Console.ResetColor();

        while (_isRunning)
        {
            DisplayMenu();
            Console.Write("\nEnter a command: ");
            var input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (MenuActions.TryGetValue(input, out var action))
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    WriteError(ex.Message);
                }
            }
            else
            {
                WriteError("Unrecognized command. Please try again.");
            }

            if (_isRunning)
            {
                PromptToContinue();
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("""

            ──────────────────────────────────────────────────────────────────
             1) Add student                 5) Search for a student
             2) Update student grade        6) Calculate statistics
             3) Remove student              7) Show category summary
             4) Display all students        UI) Launch desktop experience
             C) Clear all records           0) Exit
            ──────────────────────────────────────────────────────────────────
            """);
    }

    private static void AddStudent()
    {
        var (name, grade) = ReadStudentDetails("Add Student");
        var record = Service.AddStudent(name, grade);
        WriteSuccess($"Added {SummarizeRecord(record)}");
    }

    private static void UpdateStudent()
    {
        var (name, grade) = ReadStudentDetails("Update Student Grade");
        var record = Service.UpdateStudent(name, grade);
        WriteSuccess($"Updated {SummarizeRecord(record)}");
    }

    private static void RemoveStudent()
    {
        var name = ReadName("Remove Student");
        if (Service.RemoveStudent(name, out var removed) && removed is not null)
        {
            WriteSuccess($"Removed {SummarizeRecord(removed)}");
        }
        else
        {
            WriteError($"Student '{name}' does not exist.");
        }
    }

    private static void DisplayAllStudents()
    {
        Console.WriteLine("\nAll Students:");

        var students = Service.Students;
        if (students.Count == 0)
        {
            WriteWarning("No student records available.");
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine($" • {SummarizeRecord(student)}");
        }
    }

    private static void SearchStudent()
    {
        var name = ReadName("Search Student");
        if (Service.TryGetStudent(name, out var record) && record is not null)
        {
            WriteSuccess($"Found {SummarizeRecord(record)}");
        }
        else
        {
            WriteWarning($"Student '{name}' was not found.");
        }
    }

    private static void ShowStatistics()
    {
        var stats = Service.GetStatistics();
        if (!stats.HasData)
        {
            WriteWarning("Cannot calculate statistics without student records.");
            return;
        }

        Console.WriteLine($"""

            Total Students: {stats.TotalStudents}
            Average Grade : {stats.AverageGrade:F2}
            Highest Grade : {string.Join(", ", stats.HighestGrades.Select(SummarizeRecord))}
            Lowest Grade  : {string.Join(", ", stats.LowestGrades.Select(SummarizeRecord))}
            """);
    }

    private static void ShowCategorySummary()
    {
        var categoryCounts = Service.GetCategoryCounts();
        if (categoryCounts.Values.Sum() == 0)
        {
            WriteWarning("No student records to summarize.");
            return;
        }

        Console.WriteLine("\nGrade Categories:");
        foreach (var category in Enum.GetValues<GradeCategory>())
        {
            Console.WriteLine($" • {category,-10} : {categoryCounts[category]}");
        }
    }

    private static void LaunchDesktopUi()
    {
        var executablePath = ResolveDesktopExecutablePath();
        if (executablePath is null)
        {
            WriteWarning("""
                Could not find the desktop UI executable.
                Build the solution (Debug or Release), then retry.
                """);
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = executablePath,
                UseShellExecute = true
            });
            WriteSuccess("Desktop UI launched. You can continue using the console or switch to the UI.");
        }
        catch (Exception ex)
        {
            WriteError($"Failed to launch the desktop UI: {ex.Message}");
        }
    }

    private static void ClearAllStudents()
    {
        Console.Write("\nAre you sure you want to clear all records? (y/N): ");
        var confirmation = Console.ReadLine();
        if (string.Equals(confirmation, "y", StringComparison.OrdinalIgnoreCase))
        {
            Service.Clear();
            WriteSuccess("All student records have been cleared.");
        }
        else
        {
            WriteWarning("Clear operation cancelled.");
        }
    }

    private static void ExitApplication()
    {
        _isRunning = false;
        Console.WriteLine("\nThank you for using the Student Grade Management System. Goodbye!");
    }

    private static (string name, int grade) ReadStudentDetails(string heading)
    {
        Console.WriteLine($"\n{heading}");
        var name = ReadName();
        var grade = ReadGrade();
        return (name, grade);
    }

    private static string ReadName(string? prompt = null)
    {
        var heading = prompt is null ? string.Empty : $"{prompt}\n";
        while (true)
        {
            Console.Write($"{heading}Enter student name: ");
            var name = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            WriteError("Name cannot be empty.");
        }
    }

    private static int ReadGrade()
    {
        while (true)
        {
            Console.Write("Enter grade (0-100): ");
            var input = Console.ReadLine()?.Trim();

            if (!int.TryParse(input, out var grade))
            {
                WriteError("Grade must be a number. Try again.");
                continue;
            }

            if (grade is < 0 or > 100)
            {
                WriteError("Grade must be between 0 and 100 inclusive. Try again.");
                continue;
            }

            return grade;
        }
    }

    private static string SummarizeRecord(StudentRecord record) =>
        $"{record.Name} (Grade: {record.Grade}, Category: {record.Category})";

    private static void PromptToContinue()
    {
        Console.WriteLine("\nPress Enter to return to the menu...");
        Console.ReadLine();
    }

    private static void WriteSuccess(string message) => WriteColored(message, ConsoleColor.Green);

    private static void WriteWarning(string message) => WriteColored(message, ConsoleColor.Yellow);

    private static void WriteError(string message) => WriteColored(message, ConsoleColor.Red);

    private static void WriteColored(string message, ConsoleColor color)
    {
        var original = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = original;
    }

    private static string? ResolveDesktopExecutablePath()
    {
        var probePaths = new[]
        {
            Path.Combine(AppContext.BaseDirectory, "GradeManagement.Desktop.exe"),
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "GradeManagement.Desktop", "bin", "Debug", "net8.0-windows", "GradeManagement.Desktop.exe")),
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "GradeManagement.Desktop", "bin", "Release", "net8.0-windows", "GradeManagement.Desktop.exe"))
        };

        return probePaths.FirstOrDefault(File.Exists);
    }
}

