using GradeManagement.Core.Enums;
using GradeManagement.Core.Utilities;

namespace GradeManagement.Core.Models;

public sealed class StudentRecord
{
    public StudentRecord(string name, int grade)
    {
        Name = name;
        Grade = grade;
        Category = GradeCategoryFactory.FromGrade(grade);
    }

    public string Name { get; }

    public int Grade { get; }

    public GradeCategory Category { get; }

    public StudentRecord WithUpdatedGrade(int newGrade) => new(Name, newGrade);
}

