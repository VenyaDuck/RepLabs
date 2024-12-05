using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Labs9C_
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();

        public Form1()
        {
            InitializeComponent();
            comboBox2.Items.AddRange(new string[] { "JSON", "XML" });
            comboBox2.SelectedIndex = 0; // Устанавливаем JSON как формат по умолчанию
            comboBox1.Items.AddRange(new string[] { "Программирование", "Инженерия", "Дизайн" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void UpdateListView() // Обновление ListView
        {
            listView1.Items.Clear();
            foreach (var student in students)
            {
                var item = new ListViewItem(new string[] { student.FullName, student.StudentID, student.Direction });
                listView1.Items.Add(item);
            }
        }

        private void ClearInputs() // Очистка полей ввода
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private string GetFilePath() // Относительный путь к файлу
        {
            string extension = comboBox2.SelectedItem.ToString().ToLower();
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"students.{extension}");
        }

        // Сериализация в JSON
        private void SaveToJson(string filePath, List<Student> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(data, options));
        }

        private List<Student> LoadFromJson(string filePath)
        {
            return JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(filePath));
        }

        // Сериализация в XML
        private void SaveToXml(string filePath, List<Student> data)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        private List<Student> LoadFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return (List<Student>)serializer.Deserialize(stream);
            }
        }

        // Класс данных
        public class Student
        {
            public string FullName { get; set; }
            public string StudentID { get; set; }
            public string Direction { get; set; }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string filePath = GetFilePath();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                students = comboBox2.SelectedItem.ToString() == "JSON"
                    ? LoadFromJson(filePath)
                    : LoadFromXml(filePath);

                UpdateListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string filePath = GetFilePath();

            try
            {
                if (comboBox2.SelectedItem.ToString() == "JSON")
                    SaveToJson(filePath, students);
                else
                    SaveToXml(filePath, students);

                MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            students.Add(new Student
            {
                FullName = textBox1.Text,
                StudentID = textBox2.Text,
                Direction = comboBox1.SelectedItem.ToString()
            });

            UpdateListView();
            ClearInputs();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            students.RemoveAt(listView1.SelectedIndices[0]);
            UpdateListView();
        }
    }
}
