using System;
using System.Windows.Forms;
using AttendanceManagementSystem.Data;
using System.Data.SQLite;
using System.Data;

namespace AttendanceManagementSystem.Forms
{
    public partial class MainForm : Form
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly string _userRole;
        private TabControl tabControl = null!;
        private TabPage tabAttendance = null!;
        private TabPage tabStudents = null!;
        private DataGridView dgvStudents = null!;
        private DataGridView dgvAttendance = null!;
        private Button btnMarkAttendance = null!;
        private Button btnAddStudent = null!;
        private Button btnEditStudent = null!;
        private Button btnDeleteStudent = null!;
        private Button btnClearAttendance = null!;
        private Label lblDate = null!;
        private Label lblSummary;
        private Button button1;
        private MonthCalendar monthCalendar;
        private DateTimePicker dtpDate = null!;

        public MainForm(string userRole)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _userRole = userRole;
            LoadStudents();
            LoadAttendance();
            UpdateSummary();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tabControl = new TabControl();
            tabAttendance = new TabPage();
            monthCalendar = new MonthCalendar();
            button1 = new Button();
            lblSummary = new Label();
            dgvAttendance = new DataGridView();
            btnMarkAttendance = new Button();
            btnClearAttendance = new Button();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            tabStudents = new TabPage();
            dgvStudents = new DataGridView();
            btnAddStudent = new Button();
            btnEditStudent = new Button();
            btnDeleteStudent = new Button();
            tabControl.SuspendLayout();
            tabAttendance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            tabStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabAttendance);
            tabControl.Controls.Add(tabStudents);
            tabControl.Cursor = Cursors.AppStarting;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(667, 853);
            tabControl.TabIndex = 0;
            // 
            // tabAttendance
            // 
            tabAttendance.BackColor = Color.Transparent;
            tabAttendance.BackgroundImage = (Image)resources.GetObject("tabAttendance.BackgroundImage");
            tabAttendance.BackgroundImageLayout = ImageLayout.Stretch;
            tabAttendance.Controls.Add(monthCalendar);
            tabAttendance.Controls.Add(button1);
            tabAttendance.Controls.Add(lblSummary);
            tabAttendance.Controls.Add(dgvAttendance);
            tabAttendance.Controls.Add(btnMarkAttendance);
            tabAttendance.Controls.Add(btnClearAttendance);
            tabAttendance.Controls.Add(lblDate);
            tabAttendance.Controls.Add(dtpDate);
            tabAttendance.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabAttendance.Location = new Point(4, 37);
            tabAttendance.Name = "tabAttendance";
            tabAttendance.Size = new Size(659, 812);
            tabAttendance.TabIndex = 0;
            tabAttendance.Text = "Attendance";
            tabAttendance.Click += tabAttendance_Click;
            // 
            // monthCalendar
            // 
            monthCalendar.Enabled = false;
            monthCalendar.Location = new Point(14, 428);
            monthCalendar.MaxDate = new DateTime(2025, 4, 24, 0, 0, 0, 0);
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point(395, 647);
            button1.Name = "button1";
            button1.Size = new Size(171, 61);
            button1.TabIndex = 6;
            button1.Text = "Export to PDF";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.ForeColor = SystemColors.Control;
            lblSummary.Location = new Point(3, 733);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(64, 28);
            lblSummary.TabIndex = 5;
            lblSummary.Text = "Total:";
            // 
            // dgvAttendance
            // 
            dgvAttendance.AllowUserToAddRows = false;
            dgvAttendance.AllowUserToDeleteRows = false;
            dgvAttendance.BackgroundColor = Color.AliceBlue;
            dgvAttendance.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAttendance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAttendance.ColumnHeadersHeight = 34;
            dgvAttendance.Dock = DockStyle.Top;
            dgvAttendance.Location = new Point(0, 0);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.ReadOnly = true;
            dgvAttendance.RowHeadersWidth = 51;
            dgvAttendance.RowTemplate.Height = 40;
            dgvAttendance.RowTemplate.Resizable = DataGridViewTriState.True;
            dgvAttendance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendance.Size = new Size(659, 416);
            dgvAttendance.TabIndex = 0;
            dgvAttendance.CellContentClick += dgvAttendance_CellContentClick;
            dgvAttendance.CellFormatting += dgvAttendance_CellFormatting;
            // 
            // btnMarkAttendance
            // 
            btnMarkAttendance.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnMarkAttendance.Location = new Point(385, 445);
            btnMarkAttendance.Name = "btnMarkAttendance";
            btnMarkAttendance.Size = new Size(194, 56);
            btnMarkAttendance.TabIndex = 1;
            btnMarkAttendance.Text = "Mark Attendance";
            btnMarkAttendance.Click += btnMarkAttendance_Click;
            // 
            // btnClearAttendance
            // 
            btnClearAttendance.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnClearAttendance.Location = new Point(378, 550);
            btnClearAttendance.Name = "btnClearAttendance";
            btnClearAttendance.Size = new Size(201, 57);
            btnClearAttendance.TabIndex = 2;
            btnClearAttendance.Text = "Clear Attendance";
            btnClearAttendance.Click += btnClearAttendance_Click;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDate.ForeColor = SystemColors.Control;
            lblDate.Location = new Point(13, 658);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(57, 21);
            lblDate.TabIndex = 3;
            lblDate.Text = "Date:";
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Arial Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(76, 647);
            dtpDate.MaxDate = new DateTime(2025, 4, 24, 0, 0, 0, 0);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(166, 36);
            dtpDate.TabIndex = 4;
            dtpDate.Value = new DateTime(2025, 4, 24, 0, 0, 0, 0);
            dtpDate.ValueChanged += dtpDate_ValueChanged;
            // 
            // tabStudents
            // 
            tabStudents.BackColor = Color.Transparent;
            tabStudents.BackgroundImage = (Image)resources.GetObject("tabStudents.BackgroundImage");
            tabStudents.BackgroundImageLayout = ImageLayout.Stretch;
            tabStudents.Controls.Add(dgvStudents);
            tabStudents.Controls.Add(btnAddStudent);
            tabStudents.Controls.Add(btnEditStudent);
            tabStudents.Controls.Add(btnDeleteStudent);
            tabStudents.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabStudents.Location = new Point(4, 37);
            tabStudents.Name = "tabStudents";
            tabStudents.Size = new Size(659, 812);
            tabStudents.TabIndex = 1;
            tabStudents.Text = "Students";
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.BackgroundColor = Color.AliceBlue;
            dgvStudents.ColumnHeadersHeight = 34;
            dgvStudents.Location = new Point(0, 0);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(911, 300);
            dgvStudents.TabIndex = 0;
            dgvStudents.CellContentClick += dgvStudents_CellContentClick;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Location = new Point(10, 310);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(75, 39);
            btnAddStudent.TabIndex = 1;
            btnAddStudent.Text = "Add Student";
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // btnEditStudent
            // 
            btnEditStudent.Location = new Point(120, 310);
            btnEditStudent.Name = "btnEditStudent";
            btnEditStudent.Size = new Size(75, 39);
            btnEditStudent.TabIndex = 2;
            btnEditStudent.Text = "Edit Student";
            btnEditStudent.Click += btnEditStudent_Click;
            // 
            // btnDeleteStudent
            // 
            btnDeleteStudent.Location = new Point(230, 310);
            btnDeleteStudent.Name = "btnDeleteStudent";
            btnDeleteStudent.Size = new Size(94, 39);
            btnDeleteStudent.TabIndex = 3;
            btnDeleteStudent.Text = "Delete Student";
            btnDeleteStudent.Click += btnDeleteStudent_Click;
            // 
            // MainForm
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(667, 853);
            Controls.Add(tabControl);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Attendance Management System";
            Load += MainForm_Load;
            tabControl.ResumeLayout(false);
            tabAttendance.ResumeLayout(false);
            tabAttendance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            tabStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
        }

        private void LoadStudents()
        {
            var query = "SELECT StudentId, Name, RollNumber, Class FROM Students WHERE IsActive = 1 ORDER BY RollNumber";
            var students = _dbHelper.ExecuteQuery(query);
            dgvStudents.DataSource = students;
            dgvStudents.Columns["StudentId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStudents.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStudents.Columns["RollNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStudents.Columns["Class"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Resize columns to fit content
            dgvStudents.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            UpdateSummary();
        }

        private void LoadAttendance()
        {
            // Modified SQL query to exclude AttendanceId
            var query = @"
        SELECT s.RollNumber, s.Name, s.Class, a.Date, a.Status
        FROM Attendance a
        JOIN Students s ON a.StudentId = s.StudentId
        WHERE a.Date = @Date
        ORDER BY RollNumber";

            var parameters = new[]
            {
        new SQLiteParameter("@Date", dtpDate.Value.ToString("yyyy-MM-dd"))
    };

            var attendance = _dbHelper.ExecuteQuery(query, parameters);
            dgvAttendance.DataSource = attendance;

            // Adjust columns
            dgvAttendance.Columns["RollNumber"].DisplayIndex = 0;
            dgvAttendance.Columns["Name"].DisplayIndex = 1;
            dgvAttendance.Columns["Class"].DisplayIndex = 2;
            dgvAttendance.Columns["Date"].DisplayIndex = 3;
            dgvAttendance.Columns["Status"].DisplayIndex = 4;

            // Align text in each column
            dgvAttendance.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAttendance.Columns["RollNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAttendance.Columns["Class"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAttendance.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAttendance.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Auto resize columns based on content
            dgvAttendance.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            btnClearAttendance.Enabled = dgvAttendance.Rows.Count > 0;
            DateTime selectedDate = dtpDate.Value;
            if (IsDataAvailableForDate(selectedDate))
            {
                // If data exists, highlight the selected date
                HighlightDatesWithAttendanceData();HighlightDatesWithAttendanceData();
            }
            else
            {
                // If no data exists, remove the highlight
                RemoveHighlight();
            }
        }


        private void btnMarkAttendance_Click(object? sender, EventArgs e)
        {
            var markAttendanceForm = new MarkAttendanceForm(dtpDate.Value,this);
            markAttendanceForm.ShowDialog();
            LoadAttendance();
        }

        private void btnClearAttendance_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all attendance records for this date?",
            "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var deleteQuery = "DELETE FROM Attendance WHERE Date = @Date";
                var resetSeqQuery = "DELETE FROM sqlite_sequence WHERE name = 'Attendance'";

                using (var conn = new SQLiteConnection("Data Source=AttendanceDB.db"))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Clear attendance records
                            using (var deleteCmd = new SQLiteCommand(deleteQuery, conn, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@Date", dtpDate.Value.ToString("yyyy-MM-dd"));
                                deleteCmd.ExecuteNonQuery();
                            }

                            // Reset auto-increment sequence
                            using (var resetCmd = new SQLiteCommand(resetSeqQuery, conn, transaction))
                            {
                                resetCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            RefreshCalendar();
                            LoadAttendance();
                            UpdateSummary();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error clearing attendance: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnAddStudent_Click(object? sender, EventArgs e)
        {
            var studentForm = new StudentForm();
            studentForm.ShowDialog();
            LoadStudents();
        }

        private void btnEditStudent_Click(object? sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentId"].Value);
                var studentForm = new StudentForm(studentId);
                studentForm.ShowDialog();
                LoadStudents();
            }
            else
            {
                MessageBox.Show("Please select a student to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteStudent_Click(object? sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this student?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentId"].Value);
                    var query = "UPDATE Students SET IsActive = 0 WHERE StudentId = @StudentId";
                    var parameters = new[]
                    {
                        new SQLiteParameter("@StudentId", studentId)
                    };

                    _dbHelper.ExecuteNonQuery(query, parameters);
                    LoadStudents();
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabAttendance_Click(object sender, EventArgs e)
        {

        }

        private void dgvAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadAttendance();
            UpdateSummary();
        }
        private void dgvAttendance_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAttendance.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString()!;
                DataGridViewRow row = dgvAttendance.Rows[e.RowIndex];

                if (status == "Absent")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral; // soft red
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black; // readable text
                }
                else if (status == "Present")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; // light yellow
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White; // reset for others
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                }
            }
        }
        public void UpdateSummary()
        {
            int totalStudents = dgvAttendance.Rows.Count;
            int totalPresent = 0;
            int totalAbsent = 0;

            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString()!;
                    if (status == "Present")
                        totalPresent++;
                    else if (status == "Absent")
                        totalAbsent++;
                }
            }
            DateTime selectedDate = dtpDate.Value;
            if (IsDataAvailableForDate(selectedDate))
            {
                // If data exists, highlight the selected date
                HighlightDatesWithAttendanceData();
            }
            else
            {
                // If no data exists, remove the highlight
                RemoveHighlight();
            }

            // Update the label with the summary
            lblSummary.Text = "Total Students: "+totalStudents+" | Present: "+totalPresent+" | Absent: "+totalAbsent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=AttendanceDB.db"))
            {
                conn.Open();

                string query = @"
                SELECT a.AttendanceId, s.Name, s.RollNumber, s.Class, a.Date, a.Status
                FROM Attendance a
                JOIN Students s ON a.StudentId = s.StudentId
                WHERE a.Date = @Date
                ORDER BY s.Name";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", dtpDate.Value.ToString("yyyy-MM-dd"));

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    System.Data.DataTable table = new System.Data.DataTable();
                    adapter.Fill(table);

                    string pdfPath = "C:\\Users\\ixsaa\\OneDrive\\Desktop\\Attendance Management\\attendance.pdf";
                    using (var fs = new FileStream(pdfPath, FileMode.Create))
                    {
                        var document = new iTextSharp.text.Document();
                        var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                        document.Open();

                        foreach (System.Data.DataRow row in table.Rows)
                        {
                            string line = $"Date: {row["Date"]} | Name: {row["Name"]} | Roll: {row["RollNumber"]} | Class: {row["Class"]} | Status: {row["Status"]}";
                            document.Add(new iTextSharp.text.Paragraph(line));
                        }

                        document.Close();
                    }

                    MessageBox.Show("Exported to PDF successfully!");
                }
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        public void LoadAttendanceForDate(DateTime date)
        {
            // Clear current data and load attendance again from DB
            var query = "SELECT * FROM Attendance WHERE Date = @Date";
            var parameters = new[] {
        new SQLiteParameter("@Date", date.ToString("yyyy-MM-dd"))
    };
            var dt = _dbHelper.ExecuteQuery(query, parameters);
            dgvAttendance.DataSource = dt;

            // Optional: call UpdateSummary right here
            UpdateSummary();
        }
        private void RemoveHighlight()
        {
            // Reset any custom highlighting
            dtpDate.CalendarMonthBackground = System.Drawing.Color.White; // Reset the background to default
        }
        private bool IsDataAvailableForDate(DateTime date)
        {
            string query = "SELECT COUNT(*) FROM Attendance WHERE Date = @Date";
            var parameters = new[] { new SQLiteParameter("@Date", date.ToString("yyyy-MM-dd")) };

            var result = _dbHelper.ExecuteScalar(query, parameters);

            return Convert.ToInt32(result) > 0;
        }
        private List<DateTime> GetDatesWithAttendance()
        {
            string query = "SELECT DISTINCT Date FROM Attendance";

            var result = new List<DateTime>();

            // Execute the query and populate the list with dates that have attendance data
            var attendanceData = _dbHelper.ExecuteQuery(query);

            foreach (DataRow row in attendanceData.Rows)
            {
                DateTime date = DateTime.Parse(row["Date"].ToString()!);
                result.Add(date);
            }

            return result;
        }
        private void HighlightDatesWithAttendanceData()
        {
            // Get all dates that have attendance data
            var datesWithAttendance = GetDatesWithAttendance();

            foreach (var date in datesWithAttendance)
            {
                // Highlight the date in the calendar
                monthCalendar.AddBoldedDate(date); // Bold the date to highlight
            }

            monthCalendar.UpdateBoldedDates(); // Refresh the calendar with updated bolded dates
        }
        public void RefreshCalendar()
        {
            // Clear all bolded dates from the calendar
            monthCalendar.RemoveAllBoldedDates();

            // Highlight the dates with attendance data
            HighlightDatesWithAttendanceData();
        }
    }
} 