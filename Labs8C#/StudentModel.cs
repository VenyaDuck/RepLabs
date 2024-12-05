//Model — это взаимодействие с базой данных, например, с использованием ADO.NET для выполнения SQL-запросов.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Labs8C_
{
    public class StudentModel
    {
        private string connectionString = "Server=VENYADUCK;Database=Lab8;Integrated Security=True;";

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.Table_Lab8";
                SqlCommand command = new SqlCommand(query, connection);
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
            return students;
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO dbo.Table_Lab8 (StudentName, RecordBook, GroupNumber, Department, Specification, DateOfAdmission) VALUES (@StudentName, @RecordBook, @GroupNumber, @Department, @Specification, @DateOfAdmission)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentName", student.StudentName);
                command.Parameters.AddWithValue("@RecordBook", student.RecordBook);
                command.Parameters.AddWithValue("@GroupNumber", student.GroupNumber);
                command.Parameters.AddWithValue("@Department", student.Department);
                command.Parameters.AddWithValue("@Specification", student.Specification);
                command.Parameters.AddWithValue("@DateOfAdmission", student.DateOfAdmission);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(string recordBook)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM dbo.Table_Lab8 WHERE RecordBook = @RecordBook";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RecordBook", recordBook);
                command.ExecuteNonQuery();
            }
        }
    }
}
