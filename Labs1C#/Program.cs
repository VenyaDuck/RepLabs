using System;

class Student
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
    public string GetName()
    {
        return _name;
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public string WriteInfo()
    {
        return $"Имя студента: {_name}, Возраст: {Age}";
    }
    public void BecomeOlder()
    {
        Age++;
    }
}

class Program
{
    static void Main()
    {
        // Использование конструктора с одним параметром
        Student student1 = new Student("Алина");
        student1.Age = 17; // Устанавливаем возраст
        Console.WriteLine(student1.WriteInfo());

        // Увеличиваем возраст на 1
        student1.BecomeOlder();
        Console.WriteLine("После увеличения возраста: \n" + student1.WriteInfo() + "\n");

        // Использование конструктора с двумя параметрами
        Student student2 = new Student("Тимофей", 20);
        Console.WriteLine(student2.WriteInfo());

        // Меняем имя и показываем измененную информацию
        student2.SetName("Игнат");
        Console.WriteLine("После изменения имени: \n" + student2.WriteInfo());
    }
}