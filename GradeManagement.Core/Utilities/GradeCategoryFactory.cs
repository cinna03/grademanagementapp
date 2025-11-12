using GradeManagement.Core.Enums;

namespace GradeManagement.Core.Utilities;

public static class GradeCategoryFactory
{
    public static GradeCategory FromGrade(int grade) =>
        grade switch
        {
            < 50 => GradeCategory.Failing,
            < 70 => GradeCategory.Passing,
            < 85 => GradeCategory.Good,
            _ => GradeCategory.Excellent
        };
}

