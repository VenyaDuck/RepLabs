using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Labs8C_
{
    public partial class Form1 : Form, IStudentView
    {
        private StudentPresenter _presenter;
        private ErrorProvider _errorProvider;
        private HashSet<string> _recordBookNumbers; // Хранит уникальные номера зачётных книжек

        // Словарь для хранения институтов и их специализаций
        private Dictionary<string, List<string>> _instituteSpecializations = new Dictionary<string, List<string>>
        {
            {
                "Институт точных наук и информационных технологий",
                new List<string> { "Прикладная информатика", "Прикладная математика", "Информационная безопасность" }
            },
            {
                "Институт экономики и управления",
                new List<string> { "Туризм", "Экономика", "Менеджмент" }
            },
            {
                "Институт гуманитарных наук",
                new List<string> { "Психология", "История", "Социология" }
            }
        };

        public Form1()
        {
            InitializeComponent();
            _presenter = new StudentPresenter(this);
            _errorProvider = new ErrorProvider();
            _recordBookNumbers = new HashSet<string>();
            InitializeDataGridView();
            InitializeComboBoxes();
            InitializeTextBoxValidation();
            LoadData();
        }

        // Реализация свойств интерфейса IStudentView
        public string StudentName => textBox3.Text;
        public string RecordBook => textBox2.Text;
        public string GroupNumber => textBox1.Text;
        public string Department => comboBox1.SelectedItem?.ToString();
        public string Specification => comboBox2.SelectedItem?.ToString();
        public DateTime DateOfAdmission => dateTimePicker1.Value;

        public string SelectedStudentRecordBook
        {
            get
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    return dataGridView1.SelectedRows[0].Cells["RecordBook"].Value?.ToString();
                }
                return null;
            }
        }

        public void SetStudents(List<Student> students)
        {
            dataGridView1.Rows.Clear();
            foreach (var student in students)
            {
                dataGridView1.Rows.Add(
                    student.StudentName,
                    student.RecordBook,
                    student.GroupNumber,
                    student.Department,
                    student.Specification,
                    student.DateOfAdmission.ToShortDateString()
                );
            }
        }

        private void LoadData()
        {
            _presenter.LoadStudents();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("StudentName", "ФИО");
            dataGridView1.Columns.Add("RecordBook", "Номер зачетки");
            dataGridView1.Columns.Add("GroupNumber", "Номер группы");
            dataGridView1.Columns.Add("Department", "Институт");
            dataGridView1.Columns.Add("Specification", "Специальность");
            dataGridView1.Columns.Add("DateOfAdmission", "Дата поступления");
        }

        private void InitializeComboBoxes()
        {
            // Устанавливаем источники данных для comboBox1
            comboBox1.DataSource = new List<string>(_instituteSpecializations.Keys);
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void InitializeTextBoxValidation()
        {
            textBox2.TextChanged += TextBox2_TextChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем выбранный институт
            string selectedInstitute = comboBox1.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedInstitute) && _instituteSpecializations.ContainsKey(selectedInstitute))
            {
                // Устанавливаем специальности, связанные с выбранным институтом
                comboBox2.DataSource = _instituteSpecializations[selectedInstitute];
            }
            else
            {
                // Если институт не выбран, очищаем второй комбобокс
                comboBox2.DataSource = null;
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string recordBook = textBox2.Text;

            // Проверка: номер зачётной книжки должен состоять ровно из 8 цифр
            if (recordBook.Length != 8 || !recordBook.All(char.IsDigit))
            {
                _errorProvider.SetError(textBox2, "Номер зачётной книжки должен содержать ровно 8 цифр.");
                return;
            }

            // Проверка: номер зачётной книжки должен быть уникальным
            if (_recordBookNumbers.Contains(recordBook))
            {
                _errorProvider.SetError(textBox2, "Такой номер зачётной книжки уже существует!");
            }
            else
            {
                _errorProvider.SetError(textBox2, ""); // Ошибок нет
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Проверяем ошибки перед добавлением
            if (!string.IsNullOrEmpty(_errorProvider.GetError(textBox2)))
            {
                MessageBox.Show("Исправьте ошибки в номере зачётной книжки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Добавляем номер зачётной книжки в список уникальных значений
            _recordBookNumbers.Add(RecordBook);

            // Добавление студента
            _presenter.AddStudent();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedStudentRecordBook))
            {
                _presenter.DeleteStudent();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
