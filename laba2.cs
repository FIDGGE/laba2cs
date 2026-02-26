using System;
using System.Collections.Generic;

namespace ZooApp
{
  
  // Базовый класс для всех животных
  public class Animal
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string Diet { get; set; }

    public Animal(string name, int age, string habitat, string diet)
    {
      Name = name;
      Age = age;
      Habitat = habitat;
      Diet = diet;
    }

    public virtual string GetInfo()
    {
      return $"{Name} | Возраст: {Age} | Среда обитания: {Habitat} | Питание: {Diet}";
    }
  }

  // Класс млекопитающего
  public class Mammal : Animal
  {
    public bool HasFur { get; set; }

    public Mammal(string name, int age, string habitat, string diet, bool hasFur)
      : base(name, age, habitat, diet)
    {
      HasFur = hasFur;
    }

    public override string GetInfo()
    {
      string furStatus = HasFur ? "есть" : "нет";
      return $"{base.GetInfo()} | Тип: Млекопитающее | Шерсть: {furStatus}";
    }
  }

  // Класс птицы
  public class Bird : Animal
  {
    public double WingSpan { get; set; }

    public Bird(string name, int age, string habitat, string diet, double wingSpan)
      : base(name, age, habitat, diet)
    {
      WingSpan = wingSpan;
    }

    public override string GetInfo()
    {
      return $"{base.GetInfo()} | Тип: Птица | Размах крыльев: {WingSpan}м";
    }
  }

  // Класс рыбы
  public class Fish : Animal
  {
    public string WaterType { get; set; }

    public Fish(string name, int age, string habitat, string diet, string waterType)
      : base(name, age, habitat, diet)
    {
      WaterType = waterType;
    }

    public override string GetInfo()
    {
      return $"{base.GetInfo()} | Тип: Рыба | Тип воды: {WaterType}";
    }
  }

  // Класс пресмыкающегося
  public class Reptile : Animal
  {
    public bool IsVenomous { get; set; }

    public Reptile(string name, int age, string habitat, string diet, bool isVenomous)
      : base(name, age, habitat, diet)
    {
      IsVenomous = isVenomous;
    }

    public override string GetInfo()
    {
      string venomStatus = IsVenomous ? "ядовитое" : "неядовитое";
      return $"{base.GetInfo()} | Тип: Пресмыкающееся | Ядовитость: {venomStatus}";
    }
  }

  // Класс земноводного
  public class Amphibian : Animal
  {
    public string SkinMoisture { get; set; }

    public Amphibian(string name, int age, string habitat, string diet, string skinMoisture)
      : base(name, age, habitat, diet)
    {
      SkinMoisture = skinMoisture;
    }

    public override string GetInfo()
    {
      return $"{base.GetInfo()} | Тип: Земноводное | Влажность кожи: {SkinMoisture}";
    }
  }

  // Менеджер для управления коллекцией животных (Singleton)
  public class AnimalManager
  {
    private static AnimalManager s_instance;
    private List<Animal> _animals;

    private AnimalManager()
    {
      _animals = new List<Animal>();
    }

    public static AnimalManager Instance
    {
      get
      {
        if (s_instance == null)
        {
          s_instance = new AnimalManager();
        }
        return s_instance;
      }
    }

    public void AddAnimal(Animal newAnimal)
    {
      if (newAnimal == null)
      {
        throw new ArgumentNullException(nameof(newAnimal));
      }

      _animals.Add(newAnimal);
      Console.WriteLine($"Животное {newAnimal.Name} успешно добавлено в зоопарк");
    }

    public void ShowAllAnimals()
    {
      if (_animals.Count == 0)
      {
        Console.WriteLine("В зоопарке пока нет животных");
        return;
      }

      Console.WriteLine("\nСписок всех животных:");
      for (int animalIndex = 0; animalIndex < _animals.Count; ++animalIndex)
      {
        Console.WriteLine($"{animalIndex + 1}. {_animals[animalIndex].GetInfo()}");
      }
    }

    public void ShowAnimalByIndex(int animalIndex)
    {
      if (animalIndex >= 0 && animalIndex < _animals.Count)
      {
        Console.WriteLine(_animals[animalIndex].GetInfo());
      }
      else
      {
        Console.WriteLine("Животное с указанным номером не найдено");
      }
    }

    public void ShowAnimalByName(string animalName)
    {
      if (string.IsNullOrWhiteSpace(animalName))
      {
        Console.WriteLine("Имя животного не может быть пустым");
        return;
      }

      foreach (var currentAnimal in _animals)
      {
        if (currentAnimal.Name.Equals(animalName, StringComparison.OrdinalIgnoreCase))
        {
          Console.WriteLine(currentAnimal.GetInfo());
          return;
        }
      }

      Console.WriteLine($"Животное с именем '{animalName}' не найдено");
    }
  }
  
  //Главный класс программы
  internal class Program
  {
    private const string ExitCommand = "5";
    private const string ShowAllCommand = "1";
    private const string FindByIndexCommand = "2";
    private const string FindByNameCommand = "3";
    private const string AddAnimalCommand = "4";

    private const string MammalType = "1";
    private const string BirdType = "2";
    private const string FishType = "3";
    private const string ReptileType = "4";
    private const string AmphibianType = "5";

    private const string PositiveAnswer = "да";
    private const string NegativeAnswer = "нет";

    private static void Main()
    {
      AnimalManager manager = AnimalManager.Instance;
      AddTestAnimals(manager);

      while (true)
      {
        DisplayMenu();
        string userChoice = Console.ReadLine();

        if (userChoice == ShowAllCommand)
        {
          manager.ShowAllAnimals();
        }
        else if (userChoice == FindByIndexCommand)
        {
          FindAnimalByIndex(manager);
        }
        else if (userChoice == FindByNameCommand)
        {
          FindAnimalByName(manager);
        }
        else if (userChoice == AddAnimalCommand)
        {
          AddNewAnimal(manager);
        }
        else if (userChoice == ExitCommand)
        {
          Console.WriteLine("Программа завершена. До свидания!");
          break;
        }
        else
        {
          Console.WriteLine("Неверный ввод. Пожалуйста, выберите действие от 1 до 5");
        }
      }
    }

    private static void AddTestAnimals(AnimalManager manager)
    {
      manager.AddAnimal(new Mammal("Барсик", 5, "Лес", "Хищник", true));
      manager.AddAnimal(new Bird("Кеша", 2, "Тропики", "Всеядное", 0.3));
      manager.AddAnimal(new Fish("Немо", 1, "Океан", "Всеядное", "Морская"));
    }

    private static void DisplayMenu()
    {
      Console.WriteLine("\n=== МЕНЮ ЗООПАРКА ===");
      Console.WriteLine("1. Показать всех животных");
      Console.WriteLine("2. Найти животное по номеру");
      Console.WriteLine("3. Найти животное по имени");
      Console.WriteLine("4. Добавить новое животное");
      Console.WriteLine("5. Выход из программы");
      Console.Write("Выберите действие: ");
    }

    private static void FindAnimalByIndex(AnimalManager manager)
    {
      Console.Write("Введите номер животного: ");
      if (int.TryParse(Console.ReadLine(), out int animalNumber) && animalNumber > 0)
      {
        manager.ShowAnimalByIndex(animalNumber - 1);
      }
      else
      {
        Console.WriteLine("Пожалуйста, введите корректный положительный номер");
      }
    }

    private static void FindAnimalByName(AnimalManager manager)
    {
      Console.Write("Введите имя животного: ");
      string searchName = Console.ReadLine();
      manager.ShowAnimalByName(searchName);
    }

    private static void AddNewAnimal(AnimalManager manager)
    {
      string animalType = SelectAnimalType();
      if (animalType == null) return;

      Animal newAnimal = CreateAnimalByType(animalType);
      if (newAnimal != null)
      {
        manager.AddAnimal(newAnimal);
      }
    }

    private static string SelectAnimalType()
    {
      Console.WriteLine("\nВыберите тип животного:");
      Console.WriteLine("1. Млекопитающее");
      Console.WriteLine("2. Птица");
      Console.WriteLine("3. Рыба");
      Console.WriteLine("4. Пресмыкающееся");
      Console.WriteLine("5. Земноводное");
      Console.Write("Ваш выбор: ");

      string animalTypeChoice = Console.ReadLine();
      
      if (animalTypeChoice != MammalType && 
          animalTypeChoice != BirdType && 
          animalTypeChoice != FishType && 
          animalTypeChoice != ReptileType && 
          animalTypeChoice != AmphibianType)
      {
        Console.WriteLine("Неверный тип животного");
        return null;
      }

      return animalTypeChoice;
    }

    private static Animal CreateAnimalByType(string animalType)
    {
      Console.Write("Введите имя: ");
      string animalName = Console.ReadLine();

      Console.Write("Введите возраст: ");
      if (!int.TryParse(Console.ReadLine(), out int animalAge) || animalAge < 0)
      {
        Console.WriteLine("Неверный формат возраста");
        return null;
      }

      Console.Write("Введите среду обитания: ");
      string animalHabitat = Console.ReadLine();

      Console.Write("Введите тип питания: ");
      string animalDiet = Console.ReadLine();

      if (animalType == MammalType)
      {
        Console.Write("Есть шерсть? (да/нет): ");
        bool hasFur = Console.ReadLine()?.ToLower() == PositiveAnswer;
        return new Mammal(animalName, animalAge, animalHabitat, animalDiet, hasFur);
      }
      else if (animalType == BirdType)
      {
        Console.Write("Размах крыльев (м): ");
        if (!double.TryParse(Console.ReadLine(), out double wingSpan) || wingSpan < 0)
        {
          Console.WriteLine("Неверный формат размаха крыльев");
          return null;
        }
        return new Bird(animalName, animalAge, animalHabitat, animalDiet, wingSpan);
      }
      else if (animalType == FishType)
      {
        Console.Write("Тип воды: ");
        string waterType = Console.ReadLine();
        return new Fish(animalName, animalAge, animalHabitat, animalDiet, waterType);
      }
      else if (animalType == ReptileType)
      {
        Console.Write("Ядовитое? (да/нет): ");
        bool isVenomous = Console.ReadLine()?.ToLower() == PositiveAnswer;
        return new Reptile(animalName, animalAge, animalHabitat, animalDiet, isVenomous);
      }
      else if (animalType == AmphibianType)
      {
        Console.Write("Влажность кожи: ");
        string skinMoisture = Console.ReadLine();
        return new Amphibian(animalName, animalAge, animalHabitat, animalDiet, skinMoisture);
      }

      return null;
    }
  }
}
