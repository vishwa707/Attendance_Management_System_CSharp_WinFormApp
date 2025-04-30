using System;
using System.Data;
using System.Data.SQLite;

namespace AttendanceManagementSystem.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = "Data Source=AttendanceDB.db";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // Create Students table
                var createStudentsTable = @"
                    CREATE TABLE IF NOT EXISTS Students (
                        StudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        RollNumber TEXT UNIQUE NOT NULL,
                        Class TEXT NOT NULL,
                        IsActive INTEGER DEFAULT 1
                    )";

                // Create Attendance table
                var createAttendanceTable = @"
                    CREATE TABLE IF NOT EXISTS Attendance (
                        AttendanceId INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentId INTEGER NOT NULL,
                        Date TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
                    )";

                // Create Users table for login
                var createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT UNIQUE NOT NULL,
                        Password TEXT NOT NULL,
                        Role TEXT NOT NULL
                    )";

                using (var command = new SQLiteCommand(createStudentsTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createAttendanceTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createUsersTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert default admin user if not exists
                var checkAdmin = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
                using (var command = new SQLiteCommand(checkAdmin, connection))
                {
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        var insertAdmin = "INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin', 'Admin')";
                        using (var insertCommand = new SQLiteCommand(insertAdmin, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public DataTable ExecuteQuery(string query, SQLiteParameter[]? parameters = null)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string query, SQLiteParameter[]? parameters = null)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object? ExecuteScalar(string query, SQLiteParameter[]? parameters = null)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }
    }
} 