using System;

class Student
{
    private string _name;
    public int Age { get; set; }

    
    private static int _studentCount;

   
    public static int StudentCount => _studentCount;

    
    public static void DisplayStudentCount()
    {
        Console.WriteLine($"Количество студентов: {StudentCount}");
    }

    
    static Student()
    {
        _studentCount = 0;
        Console.WriteLine("Статический конструктор вызван." + "\n");
    }

    
    public Student(string name)
    {
        _name = name;
        Age = 0;
        _studentCount++;
    }

   
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
        
        Student student1 = new Student("Алина");
        Student student2 = new Student("Тимофей", 20);
        Student student3 = new Student(name: "Виктор", age: 22);

        StudentHelper.DisplayStudentInfo(student1);
        StudentHelper.DisplayStudentInfo(student2);
        StudentHelper.DisplayStudentInfo(student3);

        
        Student.DisplayStudentCount();
    }
}
