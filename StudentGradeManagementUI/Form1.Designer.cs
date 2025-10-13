namespace StudentGradeManagementUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Label lblSearchResult;
        private System.Windows.Forms.Label lblAverageGrade;
        private System.Windows.Forms.Label lblHighestGrade;
        private System.Windows.Forms.Label lblLowestGrade;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.Button btnCalculateAverage;
        private System.Windows.Forms.Button btnFindHighLow;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grpStudentInfo;
        private System.Windows.Forms.GroupBox grpDisplay;
        private System.Windows.Forms.GroupBox grpStatsSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpStudentInfo = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblGrade = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.grpDisplay = new System.Windows.Forms.GroupBox();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.grpStatsSearch = new System.Windows.Forms.GroupBox();
            this.lblLowestGrade = new System.Windows.Forms.Label();
            this.lblHighestGrade = new System.Windows.Forms.Label();
            this.btnFindHighLow = new System.Windows.Forms.Button();
            this.lblAverageGrade = new System.Windows.Forms.Label();
            this.btnCalculateAverage = new System.Windows.Forms.Button();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpStudentInfo.SuspendLayout();
            this.grpDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.grpStatsSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStudentInfo
            // 
            this.grpStudentInfo.Controls.Add(this.btnClear);
            this.grpStudentInfo.Controls.Add(this.btnRemove);
            this.grpStudentInfo.Controls.Add(this.btnUpdate);
            this.grpStudentInfo.Controls.Add(this.btnAdd);
            this.grpStudentInfo.Controls.Add(this.txtGrade);
            this.grpStudentInfo.Controls.Add(this.txtName);
            this.grpStudentInfo.Controls.Add(this.lblGrade);
            this.grpStudentInfo.Controls.Add(this.lblName);
            // 
            // grpStudentInfo
            // 
            this.grpStudentInfo.Controls.Add(this.btnClear);
            this.grpStudentInfo.Controls.Add(this.btnRemove);
            this.grpStudentInfo.Controls.Add(this.btnUpdate);
            this.grpStudentInfo.Controls.Add(this.btnAdd);
            this.grpStudentInfo.Controls.Add(this.txtGrade);
            this.grpStudentInfo.Controls.Add(this.txtName);
            this.grpStudentInfo.Controls.Add(this.lblGrade);
            this.grpStudentInfo.Controls.Add(this.lblName);
            // 
            // grpStudentInfo
            // 
            this.grpStudentInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grpStudentInfo.Controls.Add(this.btnClear);
            this.grpStudentInfo.Controls.Add(this.btnRemove);
            this.grpStudentInfo.Controls.Add(this.btnUpdate);
            this.grpStudentInfo.Controls.Add(this.btnAdd);
            this.grpStudentInfo.Controls.Add(this.txtGrade);
            this.grpStudentInfo.Controls.Add(this.txtName);
            this.grpStudentInfo.Controls.Add(this.lblGrade);
            this.grpStudentInfo.Controls.Add(this.lblName);
            this.grpStudentInfo.Location = new System.Drawing.Point(12, 12);
            this.grpStudentInfo.Name = "grpStudentInfo";
            this.grpStudentInfo.Size = new System.Drawing.Size(300, 200);
            this.grpStudentInfo.TabIndex = 0;
            this.grpStudentInfo.TabStop = false;
            this.grpStudentInfo.Text = "Student Information";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(155, 145);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 35);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear Inputs";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(155, 95);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(120, 35);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove Student";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(20, 145);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 35);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update Grade";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 95);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 35);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add Student";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(120, 55);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(155, 22);
            this.txtGrade.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(155, 22);
            this.txtName.TabIndex = 2;
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(20, 58);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(94, 16);
            this.lblGrade.TabIndex = 1;
            this.lblGrade.Text = "Grade (0-100):";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 28);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(96, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Student Name:";
            // 
            // grpDisplay
            // 
            this.grpDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDisplay.Controls.Add(this.dgvStudents);
            this.grpDisplay.Location = new System.Drawing.Point(318, 12);
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.Size = new System.Drawing.Size(670, 540);
            this.grpDisplay.TabIndex = 1;
            this.grpDisplay.TabStop = false;
            this.grpDisplay.Text = "All Students";
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudents.Location = new System.Drawing.Point(3, 18);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.RowHeadersWidth = 51;
            this.dgvStudents.RowTemplate.Height = 24;
            this.dgvStudents.Size = new System.Drawing.Size(664, 519);
            this.dgvStudents.TabIndex = 0;
            // 
            // grpStatsSearch
            // 
            this.grpStatsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpStatsSearch.Controls.Add(this.lblLowestGrade);
            this.grpStatsSearch.Controls.Add(this.lblHighestGrade);
            this.grpStatsSearch.Controls.Add(this.btnFindHighLow);
            this.grpStatsSearch.Controls.Add(this.lblAverageGrade);
            this.grpStatsSearch.Controls.Add(this.btnCalculateAverage);
            this.grpStatsSearch.Controls.Add(this.lblSearchResult);
            this.grpStatsSearch.Controls.Add(this.btnSearch);
            this.grpStatsSearch.Controls.Add(this.txtSearchName);
            this.grpStatsSearch.Location = new System.Drawing.Point(12, 218);
            this.grpStatsSearch.Name = "grpStatsSearch";
            this.grpStatsSearch.Size = new System.Drawing.Size(300, 334);
            this.grpStatsSearch.TabIndex = 2;
            this.grpStatsSearch.TabStop = false;
            this.grpStatsSearch.Text = "Statistics and Search";
            // 
            // lblLowestGrade
            // 
            this.lblLowestGrade.AutoSize = true;
            this.lblLowestGrade.Location = new System.Drawing.Point(20, 250);
            this.lblLowestGrade.Name = "lblLowestGrade";
            this.lblLowestGrade.Size = new System.Drawing.Size(94, 16);
            this.lblLowestGrade.TabIndex = 7;
            this.lblLowestGrade.Text = "Lowest Grade:";
            // 
            // lblHighestGrade
            // 
            this.lblHighestGrade.AutoSize = true;
            this.lblHighestGrade.Location = new System.Drawing.Point(20, 225);
            this.lblHighestGrade.Name = "lblHighestGrade";
            this.lblHighestGrade.Size = new System.Drawing.Size(98, 16);
            this.lblHighestGrade.TabIndex = 6;
            this.lblHighestGrade.Text = "Highest Grade:";
            // 
            // btnFindHighLow
            // 
            this.btnFindHighLow.Location = new System.Drawing.Point(20, 180);
            this.btnFindHighLow.Name = "btnFindHighLow";
            this.btnFindHighLow.Size = new System.Drawing.Size(255, 35);
            this.btnFindHighLow.TabIndex = 5;
            this.btnFindHighLow.Text = "Find Highest and Lowest Grades";
            this.btnFindHighLow.UseVisualStyleBackColor = true;
            this.btnFindHighLow.Click += new System.EventHandler(this.btnFindHighLow_Click);
            // 
            // lblAverageGrade
            // 
            this.lblAverageGrade.AutoSize = true;
            this.lblAverageGrade.Location = new System.Drawing.Point(20, 150);
            this.lblAverageGrade.Name = "lblAverageGrade";
            this.lblAverageGrade.Size = new System.Drawing.Size(100, 16);
            this.lblAverageGrade.TabIndex = 4;
            this.lblAverageGrade.Text = "Average Grade:";
            // 
            // btnCalculateAverage
            // 
            this.btnCalculateAverage.Location = new System.Drawing.Point(20, 110);
            this.btnCalculateAverage.Name = "btnCalculateAverage";
            this.btnCalculateAverage.Size = new System.Drawing.Size(255, 35);
            this.btnCalculateAverage.TabIndex = 3;
            this.btnCalculateAverage.Text = "Calculate Average Grade";
            this.btnCalculateAverage.UseVisualStyleBackColor = true;
            this.btnCalculateAverage.Click += new System.EventHandler(this.btnCalculateAverage_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.AutoSize = true;
            this.lblSearchResult.Location = new System.Drawing.Point(20, 70);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(92, 16);
            this.lblSearchResult.TabIndex = 2;
            this.lblSearchResult.Text = "Search Result:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(200, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(20, 25);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(170, 22);
            this.txtSearchName.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 565);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 3;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grpStatsSearch);
            this.Controls.Add(this.grpDisplay);
            this.Controls.Add(this.grpStudentInfo);
            this.Name = "Form1";
            this.Text = "Student Grade Management";
            this.grpStudentInfo.ResumeLayout(false);
            this.grpStudentInfo.PerformLayout();
            this.grpDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.grpStatsSearch.ResumeLayout(false);
            this.grpStatsSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
