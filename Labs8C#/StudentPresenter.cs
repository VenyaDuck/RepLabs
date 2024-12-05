//Presenter обрабатывает логику и связывает Model с View. В нем происходит добавление, удаление и обновление данных.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Labs8C_
{
    public class StudentPresenter
    {
        private readonly IStudentView _view;
        private readonly string _connectionString = "Server=VENYADUCK;Database=Lab8;Trusted_Connection=True;";

        public StudentPresenter(IStudentView view)
        {
            _view = view;
        }

        public void LoadStudents()
        {
            var students = new List<Student>();
            string query = "SELECT StudentName, RecordBook, GroupNumber, Department, Specification, DateOfAdmission FROM dbo.Table_Lab8";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentName = reader["StudentName"].ToString(),
                            RecordBook = reader["RecordBook"].ToString(),
                            GroupNumber = reader["GroupNumber"].ToString(),
                            Department = reader["Department"].ToString(),
                            Specification = reader["Specification"].ToString(),
                            DateOfAdmission = Convert.ToDateTime(reader["DateOfAdmission"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _view.SetStudents(students);
        }

        public void AddStudent()
        {
            var student = new Student
            {
                StudentName = _view.StudentName,
                RecordBook = _view.RecordBook,
                GroupNumber = _view.GroupNumber,
                Department = _view.Department,
                Specification = _view.Specification,
                DateOfAdmission = _view.DateOfAdmission
            };

            string query = "INSERT INTO dbo.Table_Lab8 (StudentName, RecordBook, GroupNumber, Department, Specification, DateOfAdmission) " +
                           "VALUES (@StudentName, @RecordBook, @GroupNumber, @Department, @Specification, @DateOfAdmission)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentName", student.StudentName);
                command.Parameters.AddWithValue("@RecordBook", student.RecordBook);
                command.Parameters.AddWithValue("@GroupNumber", student.GroupNumber);
                command.Parameters.AddWithValue("@Department", student.Department);
                command.Parameters.AddWithValue("@Specification", student.Specification);
                command.Parameters.AddWithValue("@DateOfAdmission", student.DateOfAdmission);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Студент успешно добавлен в базу данных.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents(); // Обновляем данные в DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void DeleteStudent()
        {
            string recordBook = _view.SelectedStudentRecordBook;

            if (string.IsNullOrEmpty(recordBook)) return;

            string query = "DELETE FROM dbo.Table_Lab8 WHERE RecordBook = @RecordBook";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RecordBook", recordBook);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents(); // Обновляем данные в DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
