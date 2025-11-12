using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GradeManagement.Core.Models;
using GradeManagement.Core.Services;

namespace GradeManagement.Desktop;

public sealed class MainForm : Form
{
    private static readonly Color BackgroundColor = Color.FromArgb(255, 228, 234);
    private static readonly Color CardColor = Color.FromArgb(255, 240, 246);
    private static readonly Color PrimaryColor = Color.FromArgb(174, 62, 108);
    private static readonly Color AccentColor = Color.FromArgb(212, 94, 136);

    private readonly StudentGradeService _service = new();

    private readonly TextBox _nameTextBox = new()
    {
        PlaceholderText = "Student Name",
        BorderStyle = BorderStyle.None,
        Font = new Font("Segoe UI", 10F),
        Margin = new Padding(0)
    };

    private readonly NumericUpDown _gradeNumericUpDown = new()
    {
        Minimum = 0,
        Maximum = 100,
        Increment = 1,
        DecimalPlaces = 0,
        Width = 320,
        BorderStyle = BorderStyle.None,
        Margin = new Padding(0)
    };

    private readonly DataGridView _studentsGrid = new()
    {
        ReadOnly = true,
        MultiSelect = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.None,
        BackgroundColor = Color.White,
        RowHeadersVisible = false
    };

    private bool _suppressSelectionSync;

    public MainForm()
    {
        Text = "Student Grade Management - Desktop";
        MinimumSize = new Size(900, 700);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = BackgroundColor;

        InitializeLayout();
        InitializeGrid();
        RefreshDashboard();
    }

    private void InitializeLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(30, 24, 30, 24),
            BackColor = Color.Transparent
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titleLabel = new Label
        {
            Text = "Student Grade Management System",
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
            ForeColor = PrimaryColor,
            Margin = new Padding(0, 0, 0, 20)
        };
        root.Controls.Add(titleLabel, 0, 0);

        var mainArea = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            AutoSize = false,
            Margin = new Padding(0)
        };
        mainArea.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420));
        mainArea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var commandCard = CreateCardPanel();
        commandCard.Margin = new Padding(0, 0, 20, 0);
        commandCard.MaximumSize = new Size(400, 0);
        commandCard.Dock = DockStyle.Fill;
        commandCard.Controls.Add(CreateFormPanel());
        mainArea.Controls.Add(commandCard, 0, 0);

        var recordsCard = CreateCardPanel();
        recordsCard.AutoSize = false;
        recordsCard.Dock = DockStyle.Fill;
        recordsCard.Padding = new Padding(24);

        var recordsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2
        };
        recordsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        recordsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        recordsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var recordsHeader = CreateSectionHeader("Student Records");
        recordsHeader.Margin = new Padding(0, 0, 0, 12);
        recordsLayout.Controls.Add(recordsHeader, 0, 0);
        recordsLayout.Controls.Add(_studentsGrid, 0, 1);
        recordsCard.Controls.Add(recordsLayout);

        mainArea.Controls.Add(recordsCard, 1, 0);
        root.Controls.Add(mainArea, 0, 1);

        Controls.Add(root);
    }

    private Control CreateFormPanel()
    {
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            Padding = new Padding(0),
            MaximumSize = new Size(360, 0),
            MinimumSize = new Size(360, 0)
        };

        layout.Controls.Add(CreatePrimaryButton("Add Student", (_, _) => HandleSafeAction(AddStudent)));
        layout.Controls.Add(CreatePrimaryButton("Display All Records", (_, _) => DisplayAllStudents()));
        layout.Controls.Add(CreatePrimaryButton("Search Student", (_, _) => HandleSafeAction(SearchStudent)));
        layout.Controls.Add(CreatePrimaryButton("Calculate Average Grade", (_, _) => ShowAverageGradeDialog()));
        layout.Controls.Add(CreatePrimaryButton("View Highest and Lowest Grades", (_, _) => ShowExtremesDialog()));

        layout.Controls.Add(CreateInputGroup("Student Name", _nameTextBox));
        layout.Controls.Add(CreateInputGroup("Grade", _gradeNumericUpDown));

        layout.Controls.Add(CreatePrimaryButton("Submit", (_, _) => SubmitStudentForm()));

        var secondaryPanel = new FlowLayoutPanel
        {
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            Margin = new Padding(0, 12, 0, 0)
        };
        secondaryPanel.Controls.Add(CreateSecondaryButton("Update Grade", (_, _) => HandleSafeAction(UpdateStudent)));
        secondaryPanel.Controls.Add(CreateSecondaryButton("Remove Student", (_, _) => HandleSafeAction(RemoveStudent)));
        secondaryPanel.Controls.Add(CreateSecondaryButton("Clear All", (_, _) => HandleSafeAction(ClearAllStudents, requireName: false)));

        layout.Controls.Add(secondaryPanel);

        _studentsGrid.SelectionChanged += (_, _) => SyncInputsWithSelection();

        return layout;
    }

    private static Panel CreateCardPanel() =>
        new()
        {
            BackColor = CardColor,
            Padding = new Padding(28),
            Margin = new Padding(0),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

    private static Label CreateSectionHeader(string text) =>
        new()
        {
            Text = text,
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 14F),
            ForeColor = PrimaryColor,
            Margin = new Padding(0, 0, 0, 12)
        };

    private Control CreateInputGroup(string labelText, Control inputControl)
    {
        var container = new Panel
        {
            Width = 360,
            AutoSize = true,
            Margin = new Padding(0, 12, 0, 0),
            BackColor = Color.Transparent
        };

        var label = new Label
        {
            Text = labelText,
            AutoSize = true,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = PrimaryColor,
            Margin = new Padding(0, 0, 0, 6),
            Dock = DockStyle.Top,
            BackColor = Color.Transparent
        };
        container.Controls.Add(label);

        var wrapper = new Panel
        {
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(14, 10, 14, 10),
            Size = new Size(360, 44),
            Margin = new Padding(0)
        };

        inputControl.Dock = DockStyle.Fill;
        inputControl.BackColor = Color.White;
        inputControl.ForeColor = PrimaryColor;
        inputControl.Margin = new Padding(0);
        inputControl.Font = new Font("Segoe UI", 10F);

        if (inputControl is TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.None;
        }
        else if (inputControl is NumericUpDown numeric)
        {
            numeric.BorderStyle = BorderStyle.None;
            numeric.TextAlign = HorizontalAlignment.Left;
            numeric.ThousandsSeparator = false;
            if (numeric.Controls.Count > 0)
            {
                numeric.Controls[0].Visible = false;
            }
        }

        wrapper.Controls.Add(inputControl);
        container.Controls.Add(wrapper);

        return container;
    }

    private static Button CreatePrimaryButton(string text, EventHandler onClick)
    {
        var button = new Button
        {
            Text = text,
            BackColor = PrimaryColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI Semibold", 11F),
            Width = 360,
            Height = 48,
            Margin = new Padding(0, 6, 0, 6),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        button.Click += onClick;
        return button;
    }

    private static Button CreateSecondaryButton(string text, EventHandler onClick)
    {
        var button = new Button
        {
            Text = text,
            BackColor = Color.Transparent,
            ForeColor = PrimaryColor,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Margin = new Padding(0, 0, 12, 0),
            AutoSize = true,
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        button.Click += onClick;
        return button;
    }

    private void InitializeGrid()
    {
        _studentsGrid.EnableHeadersVisualStyles = false;
        _studentsGrid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
        _studentsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        _studentsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
        _studentsGrid.DefaultCellStyle.SelectionBackColor = AccentColor;
        _studentsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
        _studentsGrid.DefaultCellStyle.ForeColor = PrimaryColor;
        _studentsGrid.DefaultCellStyle.BackColor = Color.White;
        _studentsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 245, 248);
        _studentsGrid.GridColor = AccentColor;

        _studentsGrid.Columns.Clear();
        _studentsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = nameof(StudentRecord.Name) });
        _studentsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Grade", DataPropertyName = nameof(StudentRecord.Grade), Width = 60 });
        _studentsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category", DataPropertyName = nameof(StudentRecord.Category), Width = 80 });
        _studentsGrid.CellDoubleClick += (_, _) => SyncInputsWithSelection();
    }

    private void RefreshDashboard()
    {
        RefreshStudentList();
    }

    private void RefreshStudentList()
    {
        var students = _service.Students
            .Select(student => new StudentView(student.Name, student.Grade, student.Category.ToString()))
            .ToList();

        _suppressSelectionSync = true;
        try
        {
            _studentsGrid.DataSource = null;
            _studentsGrid.DataSource = students;
            if (_studentsGrid.Rows.Count > 0)
            {
                _studentsGrid.ClearSelection();
            }
        }
        finally
        {
            _suppressSelectionSync = false;
        }
    }

    private void DisplayAllStudents()
    {
        RefreshStudentList();

        if (_studentsGrid.Rows.Count == 0)
        {
            ShowToast("No student records available.");
            return;
        }

        _studentsGrid.ClearSelection();
        _studentsGrid.Focus();
        ShowToast($"Displaying {_studentsGrid.Rows.Count} student(s).");
    }

    private void ShowAverageGradeDialog()
    {
        var stats = _service.GetStatistics();
        if (!stats.HasData)
        {
            ShowToast("No student records to summarize.");
            return;
        }

        ShowCustomMessage($"Average grade: {stats.AverageGrade:F2}", "Average Grade");
    }

    private void ShowExtremesDialog()
    {
        var stats = _service.GetStatistics();
        if (!stats.HasData)
        {
            ShowToast("No student records to analyze.");
            return;
        }

        var highest = string.Join(Environment.NewLine, stats.HighestGrades.Select(FormatRecord));
        var lowest = string.Join(Environment.NewLine, stats.LowestGrades.Select(FormatRecord));

        ShowCustomMessage(
            $"Highest Grade(s):{Environment.NewLine}{highest}{Environment.NewLine}{Environment.NewLine}Lowest Grade(s):{Environment.NewLine}{lowest}",
            "Grade Extremes");
    }

    private void SubmitStudentForm()
    {
        try
        {
            ValidateNameInput();
            if (_service.TryGetStudent(_nameTextBox.Text.Trim(), out _))
            {
                UpdateStudent();
            }
            else
            {
                AddStudent();
            }
        }
        catch (Exception ex)
        {
            ShowCustomMessage(ex.Message, "Error", useErrorColors: true);
        }
    }

    private void AddStudent()
    {
        var name = _nameTextBox.Text.Trim();
        var grade = (int)_gradeNumericUpDown.Value;
        _service.AddStudent(name, grade);
        RefreshDashboard();
        ResetInputFields();
        ShowToast($"Added {name} with grade {grade}.");
    }

    private void UpdateStudent()
    {
        var name = _nameTextBox.Text.Trim();
        var grade = (int)_gradeNumericUpDown.Value;
        _service.UpdateStudent(name, grade);
        RefreshDashboard();
        ShowToast($"Updated {name} to grade {grade}.");
    }

    private void RemoveStudent()
    {
        var name = _nameTextBox.Text.Trim();
        if (_service.RemoveStudent(name, out _))
        {
            RefreshDashboard();
            ResetInputFields();
            ShowToast($"Removed {name}.");
        }
        else
        {
            throw new InvalidOperationException($"Student '{name}' does not exist.");
        }
    }

    private void SearchStudent()
    {
        var name = _nameTextBox.Text.Trim();

        if (_service.TryGetStudent(name, out var record) && record is not null)
        {
            _gradeNumericUpDown.Value = record.Grade;
            HighlightStudent(record.Name);
            ShowToast($"{record.Name}: {record.Grade} ({record.Category})");
        }
        else
        {
            throw new InvalidOperationException($"Student '{name}' was not found.");
        }
    }

    private void ClearAllStudents()
    {
        var confirmed = ShowConfirmationDialog(
            "This will remove all student records. Continue?",
            "Clear All");

        if (confirmed)
        {
            _service.Clear();
            RefreshDashboard();
            ResetInputFields();
            ShowToast("All records removed.");
        }
    }

    private void SyncInputsWithSelection()
    {
        if (_suppressSelectionSync)
        {
            return;
        }

        if (_studentsGrid.CurrentRow?.DataBoundItem is null)
        {
            return;
        }

        var name = _studentsGrid.CurrentRow.Cells[0].Value?.ToString();
        var gradeValue = _studentsGrid.CurrentRow.Cells[1].Value?.ToString();

        if (string.IsNullOrWhiteSpace(name) || !int.TryParse(gradeValue, out var grade))
        {
            return;
        }

        _nameTextBox.Text = name;
        _gradeNumericUpDown.Value = Math.Clamp(grade, 0, 100);
    }

    private void HighlightStudent(string name)
    {
        foreach (DataGridViewRow row in _studentsGrid.Rows)
        {
            if (string.Equals(row.Cells[0].Value?.ToString(), name, StringComparison.OrdinalIgnoreCase))
            {
                row.Selected = true;
                _studentsGrid.CurrentCell = row.Cells[0];
                _studentsGrid.FirstDisplayedScrollingRowIndex = row.Index;
                break;
            }
        }
    }

    private static string FormatRecord(StudentRecord record) => $"{record.Name} ({record.Grade})";

    private static void ShowToast(string message) => ShowCustomMessage(message, "Student Grade Management");

    private void HandleSafeAction(Action action, bool requireName = true)
    {
        try
        {
            if (requireName)
            {
                ValidateNameInput();
            }

            action();
        }
        catch (Exception ex)
        {
            ShowCustomMessage(ex.Message, "Error", useErrorColors: true);
        }
    }

    private void ValidateNameInput()
    {
        if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
        {
            throw new ArgumentException("Please enter a student name.");
        }
    }

    private static void ShowCustomMessage(string message, string caption, bool useErrorColors = false)
    {
        using var dialog = new Form
        {
            Text = caption,
            Size = new Size(420, 220),
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            StartPosition = FormStartPosition.CenterParent,
            BackColor = BackgroundColor
        };

        var iconPanel = new Panel
        {
            Width = 60,
            Dock = DockStyle.Left,
            BackColor = useErrorColors ? Color.FromArgb(220, 80, 120) : PrimaryColor
        };

        var iconLabel = new Label
        {
            Text = useErrorColors ? "!" : "i",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 26F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };
        iconPanel.Controls.Add(iconLabel);

        var contentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            BackColor = BackgroundColor
        };

        var messageLabel = new Label
        {
            Text = message,
            ForeColor = PrimaryColor,
            Font = new Font("Segoe UI", 11F),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        var okButton = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            BackColor = PrimaryColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Width = 100,
            Height = 36,
            Top = dialog.ClientSize.Height - 60,
            Left = dialog.ClientSize.Width - 140,
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        };
        okButton.FlatAppearance.BorderSize = 0;

        contentPanel.Controls.Add(messageLabel);
        contentPanel.Controls.Add(okButton);

        dialog.Controls.Add(contentPanel);
        dialog.Controls.Add(iconPanel);
        dialog.AcceptButton = okButton;

        dialog.ShowDialog();
    }

    private static bool ShowConfirmationDialog(string message, string caption)
    {
        using var dialog = new Form
        {
            Text = caption,
            Size = new Size(440, 230),
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            StartPosition = FormStartPosition.CenterParent,
            BackColor = BackgroundColor
        };

        var iconPanel = new Panel
        {
            Width = 60,
            Dock = DockStyle.Left,
            BackColor = AccentColor
        };

        var iconLabel = new Label
        {
            Text = "?",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 26F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };
        iconPanel.Controls.Add(iconLabel);

        var contentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            BackColor = BackgroundColor
        };

        var messageLabel = new Label
        {
            Text = message,
            ForeColor = PrimaryColor,
            Font = new Font("Segoe UI", 11F),
            Dock = DockStyle.Top,
            Height = 100
        };

        var buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Bottom,
            Height = 60,
            Padding = new Padding(0, 10, 0, 0)
        };

        var yesButton = new Button
        {
            Text = "Yes",
            DialogResult = DialogResult.Yes,
            BackColor = PrimaryColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Width = 100,
            Height = 36
        };
        yesButton.FlatAppearance.BorderSize = 0;

        var noButton = new Button
        {
            Text = "No",
            DialogResult = DialogResult.No,
            BackColor = Color.White,
            ForeColor = PrimaryColor,
            FlatStyle = FlatStyle.Flat,
            Width = 100,
            Height = 36
        };
        noButton.FlatAppearance.BorderSize = 0;

        buttonPanel.Controls.Add(yesButton);
        buttonPanel.Controls.Add(noButton);

        contentPanel.Controls.Add(buttonPanel);
        contentPanel.Controls.Add(messageLabel);

        dialog.Controls.Add(contentPanel);
        dialog.Controls.Add(iconPanel);
        dialog.AcceptButton = yesButton;
        dialog.CancelButton = noButton;

        return dialog.ShowDialog() == DialogResult.Yes;
    }

    private void ResetInputFields()
    {
        _suppressSelectionSync = true;
        try
        {
            _nameTextBox.Text = string.Empty;
            _gradeNumericUpDown.Value = 0;
            if (_studentsGrid.Rows.Count > 0)
            {
                _studentsGrid.ClearSelection();
            }
        }
        finally
        {
            _suppressSelectionSync = false;
        }

        _nameTextBox.Focus();
    }

    private sealed record StudentView(string Name, int Grade, string Category);
}

