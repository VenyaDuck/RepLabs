using System;

class Subject
{
    public string SubjectName { get; set; }

    public Subject(string subjectName)
    {
        SubjectName = subjectName;
    }
}

class Game
{
    public string GameName { get; set; }

    public Game(string gameName)
    {
        GameName = gameName;
    }
}

class Student
{
    private string _name;

    public int Age { get; set; }

    public Subject FavoriteSubject { get; set; }
    public Game FavoriteGame { get; set; }

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
        return $"Имя студента: {_name}, Возраст: {Age}\nЛюбимый предмет: {FavoriteSubject?.SubjectName}, Любимая игра: {FavoriteGame?.GameName}";
    }

    public void BecomeOlder()
    {
        Age++;
    }

    public string GetFavorites()
    {
        return $"Любимый предмет: {FavoriteSubject?.SubjectName}, Любимая игра: {FavoriteGame?.GameName}";
    }
}

class Program
{
    static void ChangeAgeByValue(Student student)
    {
        student.Age = 30;
    }

    static void ChangeAgeByReference(ref Student student)
    {
        student.Age = 40;
    }

    static void Main()
    {
        Student student1 = new Student("Алина");
        student1.Age = 17;
        student1.FavoriteSubject = new Subject("Математика"); 
        student1.FavoriteGame = new Game("Шахматы"); 

        Console.WriteLine("Информация о первом студенте:");
        Console.WriteLine(student1.WriteInfo());

        student1.BecomeOlder();
        Console.WriteLine("После увеличения возраста на 1 год:");
        Console.WriteLine(student1.WriteInfo() + "\n");

        Student student2 = new Student("Тимофей", 20);
        student2.FavoriteSubject = new Subject("Физика"); 
        student2.FavoriteGame = new Game("Футбол"); 
        Console.WriteLine("Информация о втором студенте:");
        Console.WriteLine(student2.WriteInfo());

        student2.SetName("Игнат");
        Console.WriteLine("После изменения имени:");
        Console.WriteLine(student2.WriteInfo() + "\n");

        ChangeAgeByValue(student1);
        Console.WriteLine("После передачи по значению (изменение не сохраняется):");
        Console.WriteLine(student1.WriteInfo() + "\n");

        ChangeAgeByReference(ref student1);
        Console.WriteLine("После передачи по ссылке (изменение сохраняется):");
        Console.WriteLine(student1.WriteInfo() + "\n");

        Console.WriteLine("Любимый предмет и игра студента1:");
        Console.WriteLine(student1.GetFavorites());
        Console.WriteLine("Любимый предмет и игра студента2:");
        Console.WriteLine(student2.GetFavorites());
    }
}
