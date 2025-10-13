using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeManagement
{
    enum GradeCategory
    {
        Failing,
        Passing,
        Good,
        Excellent
    }

    struct StudentRecord
    {
        public string Name { get; set; }
        public int Grade { get; set; }

        public StudentRecord(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }
    }

    class Program
    {
        static Dictionary<string, int> studentGrades = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        static void Main(string[] args)
        {
            Console.WriteLine("=== Student Grade Management System ===");
            bool exit = false;

            while (!exit)
            {
                PrintMenu();
                Console.Write("\nSelect an option (1-8): ");
                string choice = (Console.ReadLine() ?? "").Trim();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        DisplayAllStudents();
                        break;
                    case "3":
                        SearchStudent();
                        break;
                    case "4":
                        CalculateAverageGrade();
                        break;
                    case "5":
                        FindHighestLowest();
                        break;
                    case "6":
                        UpdateGrade();
                        break;
                    case "7":
                        RemoveStudent();
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please choose a number from 1 to 8.");
                        break;
                }
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Display All Students");
            Console.WriteLine("3. Search for a Student");
            Console.WriteLine("4. Calculate Average Grade");
            Console.WriteLine("5. Find Highest and Lowest Grades");
            Console.WriteLine("6. Update a Student's Grade");
            Console.WriteLine("7. Remove a Student");
            Console.WriteLine("8. Exit");
        }

        static void AddStudent()
        {
            Console.Write("\nEnter student name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Operation cancelled.");
                return;
            }

            Console.Write("Enter grade (0 - 100): ");
            string gradeInput = (Console.ReadLine() ?? "").Trim();

            if (!int.TryParse(gradeInput, out int grade))
            {
                Console.WriteLine("Invalid grade input. Please enter an integer number.");
                return;
            }

            if (grade < 0 || grade > 100)
            {
                Console.WriteLine("Grade must be between 0 and 100.");
                return;
            }

            if (studentGrades.ContainsKey(name))
            {
                Console.WriteLine($"A student named '{name}' already exists with grade {studentGrades[name]}.");
                Console.Write("Do you want to overwrite the grade? (y/n): ");
                string ans = (Console.ReadLine() ?? "").Trim().ToLower();
                if (ans == "y" || ans == "yes")
                {
                    studentGrades[name] = grade;
                    Console.WriteLine($"Grade updated for {name} -> {grade}");
                }
                else
                {
                    Console.WriteLine("Operation cancelled. Student not added/updated.");
                }
            }
            else
            {
                studentGrades.Add(name, grade);
                Console.WriteLine($"Student '{name}' added with grade {grade}.");
            }
        }

        static void DisplayAllStudents()
        {
            if (studentGrades.Count == 0)
            {
                Console.WriteLine("\nNo students found.");
                return;
            }

            Console.WriteLine("\nAll students and grades:");
            int i = 1;
            foreach (var kvp in studentGrades.OrderBy(k => k.Key))
            {
                Console.WriteLine($"{i}. {kvp.Key} - {kvp.Value} ({GetCategory(kvp.Value)})");
                i++;
            }
        }

        static void SearchStudent()
        {
            Console.Write("\nEnter student name to search: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            if (studentGrades.TryGetValue(name, out int grade))
            {
                Console.WriteLine($"Found: {name} -> {grade} ({GetCategory(grade)})");
            }
            else
            {
                Console.WriteLine($"Student '{name}' not found.");
            }
        }

        static void CalculateAverageGrade()
        {
            if (studentGrades.Count == 0)
            {
                Console.WriteLine("\nNo students available to compute average.");
                return;
            }

            double average = studentGrades.Values.Average();
            Console.WriteLine($"\nAverage grade: {average:F2}");
        }

        static void FindHighestLowest()
        {
            if (studentGrades.Count == 0)
            {
                Console.WriteLine("\nNo students available to find highest/lowest grades.");
                return;
            }

            int maxGrade = studentGrades.Values.Max();
            int minGrade = studentGrades.Values.Min();

            var maxStudents = studentGrades.Where(kvp => kvp.Value == maxGrade).Select(kvp => kvp.Key).ToList();
            var minStudents = studentGrades.Where(kvp => kvp.Value == minGrade).Select(kvp => kvp.Key).ToList();

            Console.WriteLine($"\nHighest grade: {maxGrade} - Student(s): {string.Join(", ", maxStudents)} ({GetCategory(maxGrade)})");
            Console.WriteLine($"Lowest grade: {minGrade} - Student(s): {string.Join(", ", minStudents)} ({GetCategory(minGrade)})");
        }

        static void UpdateGrade()
        {
            Console.Write("\nEnter student name to update grade: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            if (!studentGrades.ContainsKey(name))
            {
                Console.WriteLine($"Student '{name}' does not exist. Use Add Student to add them first.");
                return;
            }

            Console.Write($"Current grade for {name} is {studentGrades[name]}. Enter new grade (0-100): ");
            string input = (Console.ReadLine() ?? "").Trim();

            if (!int.TryParse(input, out int newGrade) || newGrade < 0 || newGrade > 100)
            {
                Console.WriteLine("Invalid grade. Operation cancelled.");
                return;
            }

            studentGrades[name] = newGrade;
            Console.WriteLine($"Grade updated: {name} -> {newGrade}");
        }

        static void RemoveStudent()
        {
            Console.Write("\nEnter student name to remove: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            if (!studentGrades.ContainsKey(name))
            {
                Console.WriteLine($"Student '{name}' not found.");
                return;
            }

            Console.Write($"Are you sure you want to remove '{name}'? (y/n): ");
            string ans = (Console.ReadLine() ?? "").Trim().ToLower();

            if (ans == "y" || ans == "yes")
            {
                studentGrades.Remove(name);
                Console.WriteLine($"Student '{name}' removed.");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static GradeCategory GetCategory(int grade)
        {
            if (grade < 50) return GradeCategory.Failing;
            if (grade < 65) return GradeCategory.Passing;
            if (grade < 80) return GradeCategory.Good;
            return GradeCategory.Excellent;
        }
    }
}