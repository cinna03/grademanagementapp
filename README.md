# Student Grade Management System

## Overview
This is a **Student Grade Management System** written in C#.  
It allows users to manage student records and grades efficiently while practicing essential programming concepts like **data structures**, **enums**, and **exception handling**.

---

## Features

1. **Add Student** – Add a student's name and grade (0–100). Handles invalid input.
2. **Display All Students** – Lists all students and their grades along with grade categories.
3. **Search for a Student** – Search by name and display grade. Shows a message if the student is not found.
4. **Calculate Average Grade** – Computes the average of all student grades.
5. **Find Highest and Lowest Grades** – Shows students with the highest and lowest grades, including ties.
6. **Update a Student's Grade** – Modify a student's grade safely.
7. **Remove a Student** – Delete a student from the system with confirmation.
8. **Grade Categories using Enum** – Classifies grades into Failing, Passing, Good, and Excellent.
9. **Error Handling** – Prevents crashes from invalid input or operations on non-existent students.

---

## Technical Details

- **Data Structure Used:** `Dictionary<string, int>` to store student names and grades efficiently.
- **Enum:** `GradeCategory` to categorize grades.
- **Exception Handling:** Input validation using `int.TryParse` and logical checks to prevent errors.

---

## How to Run

1. **Clone the repository**
```bash
https://github.com/audreym101/student-grade-management.git
dotnet run --project StudentGradeManagementUI/StudentGradeManagementUI.csproj
---
## Demo video
https://www.loom.com/share/1656025f2b4942eea0558380a64206eb?sid=c34fb62f-08ba-4e36-9244-dd99eb044ba1
