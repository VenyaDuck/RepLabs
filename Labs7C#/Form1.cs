using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Labs7C_
{
    public partial class Form1 : Form
    {

        public class Student
        {
            public string StudentName { get; set; }
            public string RecordBook { get; set; }
            public string GroupNumber { get; set; }
            public string Department { get; set; }
            public string Specification { get; set; }
            public DateTime DateOfAdmission { get; set; }
        }
        List<string> economicsAndManagementSpecializations = new List<string>
    {
        "Туризм",
        "Экономика",
        "Менеджмент"
    };
        List<string> exactScienceAndITSpecializations = new List<string>
    {
        "Прикладная информатика",
        "Прикладная математика",
        "Информационная безопасность"
    };
        List<string> institutes = new List<string>
        {
            "Институт точных наук и информационных технологий",
            "Институт экономики и управления"
        };

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = institutes;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string studentName = textBox3.Text;
            string recordBook = textBox2.Text;
            string groupNumber = textBox1.Text;
            string department = comboBox1.SelectedItem?.ToString();
            string specification = comboBox2.SelectedItem?.ToString();
            DateTime dateOfAdmission = dateTimePicker1.Value;

            // Проверка на заполнение всех обязательных полей
            if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(recordBook) ||
                string.IsNullOrEmpty(groupNumber) || string.IsNullOrEmpty(department) ||
                string.IsNullOrEmpty(specification))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка уникальности номера зачётной книжки
            if (recordBookNumbers.Contains(recordBook))
            {
                MessageBox.Show("Такой номер зачётной книжки уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            Student student = new Student
            {
                StudentName = studentName,
                RecordBook = recordBook,
                GroupNumber = groupNumber,
                Department = department,
                Specification = specification,
                DateOfAdmission = dateOfAdmission
            };

           
            dataGridView1.Rows.Add(
                student.StudentName,
                student.RecordBook,
                student.GroupNumber,
                student.Department,
                student.Specification,
                student.DateOfAdmission.ToShortDateString()
            );

            
            recordBookNumbers.Add(recordBook);

            
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
            comboBox1.SelectedIndex = -1; 
            comboBox2.SelectedIndex = -1; 
            dateTimePicker1.Value = DateTime.Now;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, что выбран элемент в comboBox1
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() == "Институт точных наук и информационных технологий")
                {
                    comboBox2.DataSource = exactScienceAndITSpecializations;
                }
                else if (comboBox1.SelectedItem.ToString() == "Институт экономики и управления")
                {
                    comboBox2.DataSource = economicsAndManagementSpecializations;
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private List<string> recordBookNumbers = new List<string>();
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string recordBook = textBox2.Text;

            // Проверяем, что номер содержит ровно 8 символов
            if (recordBook.Length != 8 || !int.TryParse(recordBook, out _))
            {
                errorProvider1.SetError(textBox2, "Номер зачетной книжки должен состоять из 8 цифр.");
            }
            else if (recordBookNumbers.Contains(recordBook)) // Проверяем уникальность
            {
                errorProvider1.SetError(textBox2, "Такой номер зачетной книжки уже существует!");
            }
            else
            {
                errorProvider1.SetError(textBox2, ""); // Ошибок нет
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем индекс выбранной строки
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Удаляем номер зачётной книжки из списка уникальных номеров
                string recordBookToRemove = dataGridView1.Rows[rowIndex].Cells["RecordBook"].Value?.ToString();
                if (!string.IsNullOrEmpty(recordBookToRemove))
                {
                    recordBookNumbers.Remove(recordBookToRemove);
                }

                // Удаляем строку из dataGridView
                dataGridView1.Rows.RemoveAt(rowIndex);

                MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string input = textBox1.Text;

            
            if (input.Length != 8 ||
                !char.IsDigit(input[0]) || !char.IsDigit(input[1]) || !char.IsDigit(input[2]) || // Первые 3 символа - цифры
                !char.IsLetter(input[3]) ||                                                  // Следующий символ - буква
                input[4] != '-' ||                                                          // Затем дефис
                !char.IsLetter(input[5]) || !char.IsLetter(input[6]) || !char.IsLetter(input[7])) // Последние 3 символа - буквы
            {
                errorProvider1.SetError(textBox1, "Формат должен быть: 3 цифры, буква, дефис, 3 буквы (например, 131б-пио)");
            }
            else
            {
                errorProvider1.SetError(textBox1, ""); // Нет ошибок
            }
        }
    }

}

