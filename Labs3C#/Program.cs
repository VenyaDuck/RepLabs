using System;

class Student
{
    private string _name;
    public int Age { get; set; }

    // Статическое поле для подсчета созданных объектов Student
    private static int _studentCount;

    // Статическое свойство для доступа к количеству студентов
    public static int StudentCount => _studentCount;

    // Статический метод для вывода информации о количестве студентов
    public static void DisplayStudentCount()
    {
        Console.WriteLine($"Количество студентов: {StudentCount}");
    }

    // Статический конструктор
    static Student()
    {
        _studentCount = 0;
        Console.WriteLine("Статический конструктор вызван." + "\n");
    }

    // Конструктор с одним параметром
    public Student(string name)
    {
        _name = name;
        Age = 0;
        _studentCount++;
    }

    // Конструктор с двумя параметрами
    public Student(string name, int age)
    {
        _name = name;
        Age = age;
        _studentCount++;
    }
    public string WriteInfo()
    {
        return $"Имя студента: {_name}, Возраст: {Age}";
    }
}

// Статический класс для работы с объектами Student
static class StudentHelper
{
    public static void DisplayStudentInfo(Student student)
    {
        Console.WriteLine(student.WriteInfo());
    }
}

class Program
{
    static void Main()
    {
        // Создание студентов разными способами
        Student student1 = new Student("Алина");
        Student student2 = new Student("Тимофей", 20);
        Student student3 = new Student(name: "Виктор", age: 22);

        StudentHelper.DisplayStudentInfo(student1);
        StudentHelper.DisplayStudentInfo(student2);
        StudentHelper.DisplayStudentInfo(student3);

        // Использование статического метода и свойства
        Student.DisplayStudentCount();
    }
}
