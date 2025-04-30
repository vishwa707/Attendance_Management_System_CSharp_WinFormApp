# Attendance Management System

## 📋 Abstract

This project presents the design and development of a **Windows-based Attendance Management System** using **C# (Windows Forms)** and **SQLite**. The system simplifies and streamlines the process of recording and tracking student attendance in educational institutions. Featuring a modern GUI and embedded local database, it ensures ease of use, data persistence, and operational efficiency.

---

## 1️⃣ Introduction

In educational institutions, attendance plays a crucial role in evaluating student discipline and academic involvement. Traditional methods are paper-based, error-prone, and difficult to scale. This project introduces a desktop application that automates attendance recording, student management, and report generation — all within an intuitive Windows Forms interface. The application is designed for small to medium institutions and operates entirely offline.

---

## 🎯 2. Objective

- Provide a **secure**, **efficient**, and **user-friendly** attendance management solution.
- Allow administrators to:
  - Log in securely.
  - View and manage student records.
  - Mark attendance for specific dates.
  - Persist data locally using SQLite.
- Enable future enhancements like:
  - PDF report generation.
  - Role-based access.
  - Biometric integration.
  - Cloud synchronization.

---

## 🏗️ 3. System Architecture and Technology

- **Frontend:** Windows Forms (.NET Framework)
- **Backend:** SQLite (embedded, lightweight, serverless)
- **Language:** C#
- **Core Component:** `DatabaseHelper.cs`
  - Encapsulates database interactions.
  - Uses parameterized queries for SQL injection protection.
  - Provides modular methods (`ExecuteQuery`, `ExecuteNonQuery`, etc.).

---

## ⚙️ 4. Functional Modules

### 4.1 🔐 User Authentication

- Login via `LoginForm`.
- Credentials verified using `Users` table.
- On first launch, a default admin is created if no users exist.
- Safe login via parameterized queries.

### 4.2 🧑‍🎓 Student Management

- Student data stored in `Students` table.
- Fields: `Name`, `RollNumber` (unique), `Class`, `IsActive`.
- Only active students are displayed in attendance interface.

### 4.3 🗓️ Attendance Recording

- `MarkAttendanceForm` displays a list of active students for a selected date.
- Each student’s attendance is marked as **Present** or **Absent**.
- Existing attendance records for the date are replaced upon saving.

### 4.4 🗄️ Database Management

- Tables initialized on first run:
  - `Users` (Authentication)
  - `Students` (Student Info)
  - `Attendance` (Daily Attendance)
- Local database file: `AttendanceDB.db`

---

## 🖼️ 5. User Interface Design

- Clean, fixed-size UI with subtle color themes and background images.
- Main controls:
  - `TextBox`, `Label`, `Button`, `DataGridView`, `ComboBox`, `MonthCalendar`
- **LoginForm:** Simple and validated login.
- **MarkAttendanceForm:** Interactive grid with Save/Cancel buttons.
- **MainForm:**
  - Calendar highlighting attendance-recorded dates.
  - Export to PDF via `iTextSharp`.

---

## 🔐 6. Security Considerations

- ✅ Uses parameterized SQL queries.
- ✅ Default admin created only on first run.
- ❌ Currently lacks password encryption.
- 🔜 Future upgrades:
  - Password hashing (SHA-256, bcrypt).
  - Database encryption.

---

## 🚀 7. Future Enhancements

- 👤 User Management Interface (Add/Edit Users, Roles)
- 📊 Chart-Based Dashboard (Attendance Analytics)
- 📧 Email Notifications for Absent Students

---

## ✅ 8. Conclusion

This project demonstrates how a robust, offline **Attendance Management System** can be built with **C# and SQLite**. It is reliable, user-friendly, and ideal for educational institutions seeking a standalone solution. With its extensible architecture, it is well-suited for future development and real-world deployment.

---

## 📦 Dependencies

- [.NET Framework](https://dotnet.microsoft.com/)
- [System.Data.SQLite](https://system.data.sqlite.org/)
- [iTextSharp (for PDF export)](https://github.com/itext/itextsharp)

---

## 🖥️ Screenshots

_Add screenshots of your forms here (LoginForm, MarkAttendanceForm, MainForm, PDF export, etc.)_

---

## 🧑‍💻 Author

- [Your Name or GitHub Username]

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
