
using System;

interface IPerson
{
    string Name { get; set; }
    string WriteInfo();
}

interface ISpecialist : IPerson
{
    string Specialization { get; set; }
    string GetSpecializationInfo();
}

class Subject
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Subject(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название предмета не может быть пустым.", nameof(title));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание предмета не может быть пустым.", nameof(description));

        Title = title;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Title}: {Description}";
    }
}

class Student : IPerson, ICloneable, IComparable<Student>
{
    private string _name;
    public int Age { get; set; }
    public Subject FavoriteSubject { get; set; }

    public Student(string name, int age, Subject favoriteSubject)
    {
        if (age <= 0)
            throw new ArgumentException("Возраст должен быть положительным числом.", nameof(age));
        if (age < 16)
            throw new ArgumentOutOfRangeException(nameof(age), "Студент должен быть старше 16 лет.");

        _name = name;
        Age = age;
        FavoriteSubject = favoriteSubject;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Имя студента не может быть пустым.", nameof(Name));
            _name = value;
        }
    }

    public string WriteInfo()
    {
        return $"Имя студента: {Name}, Возраст: {Age}, Любимый предмет: {FavoriteSubject}";
    }

    public object Clone()
    {
        return new Student(Name, Age, new Subject(FavoriteSubject.Title, FavoriteSubject.Description));
    }

    public int CompareTo(Student other)
    {
        if (Age < 16 || other.Age < 16)
            throw new InvalidOperationException("Студент должен быть старше 16 лет для выполнения сравнения.");

        return Age.CompareTo(other.Age);
    }

    public override string ToString()
    {
        return $"[Student] Имя: {Name}, Возраст: {Age}, Любимый предмет: {FavoriteSubject}";
    }
}

class ITStudent : Student, ISpecialist
{
    public string ProgrammingLanguage { get; set; }

    string ISpecialist.Specialization { get; set; }

    string ISpecialist.GetSpecializationInfo()
    {
        return $"IT-специалист с уклоном на язык: {ProgrammingLanguage}";
    }

    public ITStudent(string name, int age, Subject favoriteSubject, string programmingLanguage)
        : base(name, age, favoriteSubject)
    {
        if (string.IsNullOrWhiteSpace(programmingLanguage))
            throw new ArgumentException("Язык программирования не может быть пустым.", nameof(programmingLanguage));

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
        try
        {
           
            Subject subject = new Subject("Математика", "Фундаментальный предмет для IT.");

            
            Student student = new Student("Алина", 18, subject);
            Console.WriteLine(student.WriteInfo());

            
            Student clonedStudent = (Student)student.Clone();
            Console.WriteLine("Клон: " + clonedStudent.WriteInfo());

            
            Student student2 = new Student("Тимофей", 20, new Subject("Программирование", "Основы разработки."));
            Console.WriteLine($"Сравнение студентов (по возрасту): {student.CompareTo(student2)}");

            
            ITStudent itStudent = new ITStudent("Игнат", 21, new Subject("Физика", "Теория и практика моделирования."), "C#");

            
            ISpecialist specialist = itStudent;
            Console.WriteLine(specialist.GetSpecializationInfo());

            
            Console.WriteLine(itStudent.WriteInfo());
        }
        catch (ArgumentException ex) when (ex.ParamName == "age")
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "age")
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}
