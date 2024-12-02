using System;

// Интерфейс IPerson
interface IPerson
{
    string Name { get; set; }
    string WriteInfo();
}

// Интерфейс ISpecialist
interface ISpecialist : IPerson
{
    string Specialization { get; set; }
    string GetSpecializationInfo();
}

// Класс Subject
class Subject
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Subject(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Title}: {Description}";
    }
}

// Класс Student
class Student : IPerson, ICloneable, IComparable<Student>
{
    private string _name;
    public int Age { get; set; }
    public Subject FavoriteSubject { get; set; } // Любимый предмет

    public Student(string name, int age, Subject favoriteSubject)
    {
        _name = name;
        Age = age;
        FavoriteSubject = favoriteSubject;
    }

    // Реализация IPerson
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string WriteInfo()
    {
        return $"Имя студента: {Name}, Возраст: {Age}, Любимый предмет: {FavoriteSubject}";
    }

    // Реализация ICloneable
    public object Clone()
    {
        return new Student(Name, Age, new Subject(FavoriteSubject.Title, FavoriteSubject.Description));
    }

    // Реализация IComparable<Student>
    public int CompareTo(Student other)
    {
        return Age.CompareTo(other.Age);
    }

    public override string ToString()
    {
        return $"[Student] Имя: {Name}, Возраст: {Age}, Любимый предмет: {FavoriteSubject}";
    }
}

// Класс ITStudent
class ITStudent : Student, ISpecialist
{
    public string ProgrammingLanguage { get; set; }

    // Реализация ISpecialist
    string ISpecialist.Specialization { get; set; }

    string ISpecialist.GetSpecializationInfo()
    {
        return $"IT-специалист с уклоном на язык: {ProgrammingLanguage}";
    }

    public ITStudent(string name, int age, Subject favoriteSubject, string programmingLanguage)
        : base(name, age, favoriteSubject)
    {
        ProgrammingLanguage = programmingLanguage;
    }

    public new string WriteInfo()
    {
        return $"Имя IT-студента: {Name}, Возраст: {Age}, Любимый предмет: {FavoriteSubject}, Язык программирования: {ProgrammingLanguage}";
    }
}

class Program
{
    static void Main()
    {
        // Создание объекта класса Subject
        Subject subject = new Subject("Математика", "Фундаментальный предмет для IT.");

        // Создание объекта класса Student
        Student student = new Student("Алина", 18, subject);
        Console.WriteLine(student.WriteInfo());

        // Клонирование объекта
        Student clonedStudent = (Student)student.Clone();
        Console.WriteLine("Клон: " + clonedStudent.WriteInfo());

        // Сравнение студентов
        Student student2 = new Student("Тимофей", 20, new Subject("Программирование", "Основы разработки."));
        Console.WriteLine($"Сравнение студентов (по возрасту): {student.CompareTo(student2)}");

        // Создание объекта ITStudent
        ITStudent itStudent = new ITStudent("Игнат", 21, new Subject("Физика", "Теория и практика моделирования."), "C#");

        // Использование явной реализации ISpecialist
        ISpecialist specialist = itStudent;
        Console.WriteLine(specialist.GetSpecializationInfo());

        // Использование метода WriteInfo через ITStudent
        Console.WriteLine(itStudent.WriteInfo());
    }
}
