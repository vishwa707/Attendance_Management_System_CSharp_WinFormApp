using System;
using System.Windows.Forms;
using AttendanceManagementSystem.Data;
using System.Data.SQLite;

namespace AttendanceManagementSystem.Forms
{
    public partial class StudentForm : Form
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly int? _studentId;
        private TextBox txtName = null!;
        private TextBox txtRollNumber = null!;
        private TextBox txtClass = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private Label lblName = null!;
        private Label lblRollNumber = null!;
        private Label lblClass = null!;

        public StudentForm(int? studentId = null)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _studentId = studentId;
            this.Text = _studentId.HasValue ? "Edit Student" : "Add Student";

            if (_studentId.HasValue)
            {
                LoadStudentData();
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentForm));
            txtName = new TextBox();
            txtRollNumber = new TextBox();
            txtClass = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblName = new Label();
            lblRollNumber = new Label();
            lblClass = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.None;
            txtName.Location = new Point(138, 27);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(228, 20);
            txtName.TabIndex = 0;
            // 
            // txtRollNumber
            // 
            txtRollNumber.BorderStyle = BorderStyle.None;
            txtRollNumber.Location = new Point(138, 80);
            txtRollNumber.Margin = new Padding(3, 4, 3, 4);
            txtRollNumber.Name = "txtRollNumber";
            txtRollNumber.Size = new Size(228, 20);
            txtRollNumber.TabIndex = 1;
            // 
            // txtClass
            // 
            txtClass.BorderStyle = BorderStyle.None;
            txtClass.Location = new Point(138, 133);
            txtClass.Margin = new Padding(3, 4, 3, 4);
            txtClass.Name = "txtClass";
            txtClass.Size = new Size(228, 20);
            txtClass.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSave.Location = new Point(24, 208);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(307, 208);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(103, 40);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblName.ForeColor = SystemColors.Control;
            lblName.Location = new Point(45, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(73, 28);
            lblName.TabIndex = 5;
            lblName.Text = "Name:";
            // 
            // lblRollNumber
            // 
            lblRollNumber.AutoSize = true;
            lblRollNumber.BackColor = Color.Transparent;
            lblRollNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRollNumber.ForeColor = SystemColors.Control;
            lblRollNumber.Location = new Point(1, 72);
            lblRollNumber.Name = "lblRollNumber";
            lblRollNumber.Size = new Size(138, 28);
            lblRollNumber.TabIndex = 6;
            lblRollNumber.Text = "Roll Number:";
            // 
            // lblClass
            // 
            lblClass.AutoSize = true;
            lblClass.BackColor = Color.Transparent;
            lblClass.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblClass.ForeColor = SystemColors.Control;
            lblClass.Location = new Point(45, 126);
            lblClass.Name = "lblClass";
            lblClass.Size = new Size(64, 28);
            lblClass.TabIndex = 7;
            lblClass.Text = "Class:";
            // 
            // StudentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(439, 281);
            Controls.Add(lblClass);
            Controls.Add(lblRollNumber);
            Controls.Add(lblName);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtClass);
            Controls.Add(txtRollNumber);
            Controls.Add(txtName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "StudentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Student Details";
            ResumeLayout(false);
            PerformLayout();
        }

        private void LoadStudentData()
        {
            var query = "SELECT Name, RollNumber, Class FROM Students WHERE StudentId = @StudentId";
            var parameters = new[]
            {
                new SQLiteParameter("@StudentId", _studentId.Value)
            };

            var student = _dbHelper.ExecuteQuery(query, parameters);
            if (student.Rows.Count > 0)
            {
                txtName.Text = student.Rows[0]["Name"].ToString()!;
                txtRollNumber.Text = student.Rows[0]["RollNumber"].ToString()!;
                txtClass.Text = student.Rows[0]["Class"].ToString()!;
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtRollNumber.Text) ||
                string.IsNullOrWhiteSpace(txtClass.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_studentId.HasValue)
                {
                    // Update existing student
                    var query = @"
                        UPDATE Students 
                        SET Name = @Name, 
                            RollNumber = @RollNumber, 
                            Class = @Class 
                        WHERE StudentId = @StudentId";

                    var parameters = new[]
                    {
                        new SQLiteParameter("@Name", txtName.Text),
                        new SQLiteParameter("@RollNumber", txtRollNumber.Text),
                        new SQLiteParameter("@Class", txtClass.Text),
                        new SQLiteParameter("@StudentId", _studentId.Value)
                    };

                    _dbHelper.ExecuteNonQuery(query, parameters);
                }
                else
                {
                    // Insert new student
                    var query = @"
                        INSERT INTO Students (Name, RollNumber, Class)
                        VALUES (@Name, @RollNumber, @Class)";

                    var parameters = new[]
                    {
                        new SQLiteParameter("@Name", txtName.Text),
                        new SQLiteParameter("@RollNumber", txtRollNumber.Text),
                        new SQLiteParameter("@Class", txtClass.Text)
                    };

                    _dbHelper.ExecuteNonQuery(query, parameters);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 