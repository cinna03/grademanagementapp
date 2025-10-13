using System;
using System.Collections.Generic;
using System.Data; // Required for DataTable
using System.Linq;
using System.Windows.Forms;

namespace StudentGradeManagementUI
{
    public partial class Form1 : Form
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
            public string Category { get; set; } // Added for UI display

            public StudentRecord(string name, int grade, string category)
            {
                Name = name;
                Grade = grade;
                Category = category;
            }
        }

        Dictionary<string, int> studentGrades = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
            DisplayAll();
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initial display when form loads
            DisplayAll();
        }

        private void ShowStatusMessage(string message, bool isError = false)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        private void SetupDataGridView()
        {
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.ColumnCount = 3;

            dgvStudents.Columns[0].Name = "Name";
            dgvStudents.Columns[0].HeaderText = "Student Name";
            dgvStudents.Columns[0].DataPropertyName = "Name";
            dgvStudents.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvStudents.Columns[1].Name = "Grade";
            dgvStudents.Columns[1].HeaderText = "Grade";
            dgvStudents.Columns[1].DataPropertyName = "Grade";
            dgvStudents.Columns[1].Width = 80;

            dgvStudents.Columns[2].Name = "Category";
            dgvStudents.Columns[2].HeaderText = "Category";
            dgvStudents.Columns[2].DataPropertyName = "Category";
            dgvStudents.Columns[2].Width = 100;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowStatusMessage("Invalid name. Operation cancelled.", true);
                return;
            }

            if (!int.TryParse(txtGrade.Text.Trim(), out int grade))
            {
                ShowStatusMessage("Invalid grade input. Please enter an integer number.", true);
                return;
            }

            if (grade < 0 || grade > 100)
            {
                ShowStatusMessage("Grade must be between 0 and 100.", true);
                return;
            }

            if (studentGrades.ContainsKey(name))
            {
                DialogResult dialogResult = MessageBox.Show($"A student named '{name}' already exists with grade {studentGrades[name]}. Do you want to overwrite the grade?", "Student Exists", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    studentGrades[name] = grade;
                    ShowStatusMessage($"Grade updated for {name} -> {grade}");
                }
                else
                {
                    ShowStatusMessage("Operation cancelled. Student not added/updated.");
                }
            }
            else
            {
                studentGrades.Add(name, grade);
                ShowStatusMessage($"Student '{name}' added with grade {grade}.");
            }
            DisplayAll();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowStatusMessage("Invalid name.", true);
                return;
            }

            if (!studentGrades.ContainsKey(name))
            {
                ShowStatusMessage($"Student '{name}' does not exist. Use Add Student to add them first.", true);
                return;
            }

            if (!int.TryParse(txtGrade.Text.Trim(), out int newGrade) || newGrade < 0 || newGrade > 100)
            {
                ShowStatusMessage("Invalid grade. Operation cancelled.", true);
                return;
            }

            studentGrades[name] = newGrade;
            ShowStatusMessage($"Grade updated: {name} -> {newGrade}");
            DisplayAll();
            ClearInputs();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowStatusMessage("Invalid name.", true);
                return;
            }

            if (!studentGrades.ContainsKey(name))
            {
                ShowStatusMessage($"Student '{name}' not found.", true);
                return;
            }

            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to remove '{name}'?", "Confirm Removal", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                studentGrades.Remove(name);
                ShowStatusMessage($"Student '{name}' removed.");
            }
            else
            {
                ShowStatusMessage("Operation cancelled.");
            }
            DisplayAll();
            ClearInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearchName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowStatusMessage("Invalid name for search.", true);
                lblSearchResult.Text = "Search Result:";
                return;
            }

            if (studentGrades.TryGetValue(name, out int grade))
            {
                lblSearchResult.Text = $"Found: {name} -> {grade} ({GetCategory(grade)})";
                ShowStatusMessage($"Student '{name}' found.");
            }
            else
            {
                lblSearchResult.Text = "Search Result: Not Found";
                ShowStatusMessage($"Student '{name}' not found.", true);
            }
        }

        private void btnCalculateAverage_Click(object sender, EventArgs e)
        {
            if (studentGrades.Count == 0)
            {
                ShowStatusMessage("No students available to compute average.", true);
                lblAverageGrade.Text = "Average Grade:";
                return;
            }

            double average = studentGrades.Values.Average();
            lblAverageGrade.Text = $"Average Grade: {average:F2}";
            ShowStatusMessage("Average grade calculated.");
        }

        private void btnFindHighLow_Click(object sender, EventArgs e)
        {
            if (studentGrades.Count == 0)
            {
                ShowStatusMessage("No students available to find highest/lowest grades.", true);
                lblHighestGrade.Text = "Highest Grade:";
                lblLowestGrade.Text = "Lowest Grade:";
                return;
            }

            int maxGrade = studentGrades.Values.Max();
            int minGrade = studentGrades.Values.Min();

            var maxStudents = studentGrades.Where(kvp => kvp.Value == maxGrade).Select(kvp => kvp.Key).ToList();
            var minStudents = studentGrades.Where(kvp => kvp.Value == minGrade).Select(kvp => kvp.Key).ToList();

            lblHighestGrade.Text = $"Highest Grade: {maxGrade} - Student(s): {string.Join(", ", maxStudents)} ({GetCategory(maxGrade)})";
            lblLowestGrade.Text = $"Lowest Grade: {minGrade} - Student(s): {string.Join(", ", minStudents)} ({GetCategory(minGrade)})";
            ShowStatusMessage("Highest and lowest grades found.");
        }

        private void DisplayAll()
        {
            dgvStudents.DataSource = null; // Clear previous data
            if (studentGrades.Count == 0)
            {
                ShowStatusMessage("No students found.");
                return;
            }

            var studentList = studentGrades.Select(kvp => new StudentRecord(kvp.Key, kvp.Value, GetCategory(kvp.Value))).ToList();
            dgvStudents.DataSource = studentList;
            ShowStatusMessage("All students displayed.");
        }

        private string GetCategory(int grade)
        {
            if (grade < 50) return GradeCategory.Failing.ToString();
            if (grade < 65) return GradeCategory.Passing.ToString();
            if (grade < 80) return GradeCategory.Good.ToString();
            return GradeCategory.Excellent.ToString();
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtGrade.Clear();
            txtSearchName.Clear();
            lblSearchResult.Text = "Search Result:";
            lblAverageGrade.Text = "Average Grade:";
            lblHighestGrade.Text = "Highest Grade:";
            lblLowestGrade.Text = "Lowest Grade:";
            ShowStatusMessage(""); // Clear status message
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            ShowStatusMessage("Input fields cleared.");
        }
    }
}
