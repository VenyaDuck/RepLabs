using System;

abstract class Person
{
    // Абстрактное свойство
    public abstract string Name { get; set; }

    // Абстрактный метод
    public abstract string WriteInfo();
}

class Student : Person
{
    private string _name;
    public int Age { get; set; }

    public Student(string name)
    {
        _name = name;
        Age = 0;
    }

    public Student(string name, int age)
    {
        _name = name;
        Age = age;
    }

    // Реализация абстрактного свойства
    public override string Name
    {
        get => _name;
        set => _name = value;
    }

    // Реализация абстрактного метода
    public override string WriteInfo()
    {
        return $"Имя студента: {Name}, Возраст: {Age}";
    }


    public void BecomeOlder()
    {
        Age++;
    }


    public override string ToString()
    {
        return $"[Student] Имя: {Name}, Возраст: {Age}";
    }
}

class ITStudent : Student
{
    public string ProgrammingLanguage { get; set; }

    public ITStudent(string name, int age, string programmingLanguage)
        : base(name, age)
    {
        ProgrammingLanguage = programmingLanguage;
    }


    public new string WriteInfo()
    {
        return $"Имя IT-студента: {Name}, Возраст: {Age}, Язык программирования: {ProgrammingLanguage}";
    }
}

class Program
{
    static void Main()
    {

        Student student1 = new Student("Алина", 18);
        Console.WriteLine(student1.WriteInfo());
        Console.WriteLine(student1.ToString()); 

        // Создание объекта класса ITStudent
        ITStudent itStudent = new ITStudent("Тимофей", 20, "C#");
        Console.WriteLine(itStudent.WriteInfo()); 

        // Работа через базовый класс Person
        Person person = itStudent;
        Console.WriteLine(person.WriteInfo());

        // Демонстрация работы ToString
        Console.WriteLine(itStudent.ToString());
    }
}
