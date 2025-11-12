namespace GradeManagement.Core.Models;

public sealed class GradeStatistics
{
    public GradeStatistics(
        int totalStudents,
        double averageGrade,
        IReadOnlyList<StudentRecord> highestGrades,
        IReadOnlyList<StudentRecord> lowestGrades)
    {
        TotalStudents = totalStudents;
        AverageGrade = averageGrade;
        HighestGrades = highestGrades;
        LowestGrades = lowestGrades;
    }

    public int TotalStudents { get; }

    public double AverageGrade { get; }

    public IReadOnlyList<StudentRecord> HighestGrades { get; }

    public IReadOnlyList<StudentRecord> LowestGrades { get; }

    public bool HasData => TotalStudents > 0;
}

