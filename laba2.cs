/************************************
 *Лабораторная работа №2 "ООП на C#"*
 *Автор: Гончаров Роман             *
 ************************************/

using System;
using System.Collections.Generic;

namespace ZooApp {
  
  // Базовый класс для всех животных
  public class Animal {
    
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string Diet { get; set; }

    public Animal(string name, int age, string habitat, string diet) {
      Name = name;
      Age = age;
      Habitat = habitat;
      Diet = diet;
    }

    public virtual string GetInfo() {
      return $"{Name} | Age: {Age} | Habitat: {Habitat} | Diet: {Diet}";
    }
  }

  // Класс млекопитающего
  public class Mammal : Animal {
    public bool HasFur { get; set; 
  }

    public Mammal(string name, int age, string habitat, string diet, bool hasFur)
      : base(name, age, habitat, diet) {
      HasFur = hasFur;
      }

    public override string GetInfo() {
      string furStatus = HasFur ? "yes" : "no";
      return $"{base.GetInfo()} | Type: Mammal | Fur: {furStatus}";
    }
  }

  // Класс птицы
  public class Bird : Animal {
    public double WingSpan { get; set; }

    public Bird(string name, int age, string habitat, string diet, double wingSpan)
      : base(name, age, habitat, diet) {
        WingSpan = wingSpan;
      }

    public override string GetInfo() {
      return $"{base.GetInfo()} | Type: Bird | Wingspan: {WingSpan}m";
    }
  }

  // Класс рыбы
  public class Fish : Animal {
    public string WaterType { get; set; }

    public Fish(string name, int age, string habitat, string diet, string waterType)
      : base(name, age, habitat, diet) {
      WaterType = waterType;
      }

    public override string GetInfo() {
      return $"{base.GetInfo()} | Type: Fish | Water type: {WaterType}";
    }
  }

  // Класс пресмыкающегося
  public class Reptile : Animal {
    public bool IsVenomous { get; set; }

    public Reptile(string name, int age, string habitat, string diet, bool isVenomous)
      : base(name, age, habitat, diet) {
      IsVenomous = isVenomous;
      }

    public override string GetInfo() {
      
      string venomStatus = IsVenomous ? "venomous" : "non-venomous";
      return $"{base.GetInfo()} | Type: Reptile | Venomous: {venomStatus}";
    }
  }

  // Класс земноводного
  public class Amphibian : Animal {
    public string SkinMoisture { get; set; }

    public Amphibian(string name, int age, string habitat, string diet, string skinMoisture)
      : base(name, age, habitat, diet) {
      SkinMoisture = skinMoisture;
      }

    public override string GetInfo() {
      return $"{base.GetInfo()} | Type: Amphibian | Skin moisture: {SkinMoisture}";
    }
  }

  // Менеджер для управления коллекцией животных (Singleton)
  public class AnimalManager {
  
    private static AnimalManager s_instance;
    private List<Animal> _animals;
    //переменная для проверки наличия животных
    private const int absenceOfAnimals = 0;
    //переменная для проверки корректности введенных данных
    private const int incorrectData = 0;

    private AnimalManager() {
      _animals = new List<Animal>();
    }

    public static AnimalManager Instance {
      get {
        if (s_instance == null) {
          s_instance = new AnimalManager();
        }
        return s_instance;
      }
    }

    public void AddAnimal(Animal newAnimal) {
      if (newAnimal == null) {
        throw new ArgumentNullException(nameof(newAnimal));
      }

      _animals.Add(newAnimal);
      Console.WriteLine($"Animal {newAnimal.Name} successfully added to the zoo");
    }

    public void ShowAllAnimals() {
      if (_animals.Count <= absenceOfAnimals) {
        Console.WriteLine("There are no animals in the zoo yet");
        return;
      }

      Console.WriteLine("\nList of all animals:");
      for (int animalIndex = 0; animalIndex < _animals.Count; ++animalIndex) {
        Console.WriteLine($"{animalIndex + 1}. {_animals[animalIndex].GetInfo()}");
      }
    }

    public void ShowAnimalByIndex(int animalIndex) {
      if (_animals.Count <= absenceOfAnimals) {
        Console.WriteLine("There are no animals in the zoo");
        return;
      }
      
      if (animalIndex >= absenceOfAnimals && animalIndex < _animals.Count) {
        Console.WriteLine(_animals[animalIndex].GetInfo());
      } else {
        Console.WriteLine("Animal with the specified number not found");
      }
    }

    public void ShowAnimalByName(string animalName) {
      if (_animals.Count <= absenceOfAnimals) {
        Console.WriteLine("There are no animals in the zoo");
        return;
      }
      
      if (string.IsNullOrWhiteSpace(animalName)) {
        Console.WriteLine("Animal name cannot be empty");
        return;
      }

      foreach (var currentAnimal in _animals) {
        if (currentAnimal.Name.Equals(animalName, StringComparison.OrdinalIgnoreCase)) {
          Console.WriteLine(currentAnimal.GetInfo());
          return;
        }
      }

      Console.WriteLine($"Animal with name '{animalName}' not found");
    }
  }
  
  //Главный класс программы
  internal class Program {
    
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

    private const string PositiveAnswer = "yes";
    private const string NegativeAnswer = "no";

    private static void Main() {
      
      AnimalManager manager = AnimalManager.Instance;
      AddTestAnimals(manager);

      while (true) {
        
        DisplayMenu();
        string userChoice = Console.ReadLine();

        if (userChoice == ShowAllCommand) {
          manager.ShowAllAnimals();
        }
        else if (userChoice == FindByIndexCommand) {
          FindAnimalByIndex(manager);
        }
        else if (userChoice == FindByNameCommand) {
          FindAnimalByName(manager);
        }
        else if (userChoice == AddAnimalCommand) {
          AddNewAnimal(manager);
        }
        else if (userChoice == ExitCommand) {
          Console.WriteLine("Program terminated. Goodbye!");
          break;
        } else {
          Console.WriteLine("Invalid input. Please select an action from 1 to 5");
        }
      }
    }

    private static void AddTestAnimals(AnimalManager manager) {
      
      manager.AddAnimal(new Mammal("Barsik", 5, "Forest", "Predator", true));
      manager.AddAnimal(new Bird("Kesha", 2, "Tropics", "Omnivore", 0.3));
      manager.AddAnimal(new Fish("Nemo", 1, "Ocean", "Omnivore", "Salt water"));
    }

    private static void DisplayMenu() {
      
      Console.WriteLine("\n=== ZOO MENU ===");
      Console.WriteLine("1. Show all animals");
      Console.WriteLine("2. Find animal by number");
      Console.WriteLine("3. Find animal by name");
      Console.WriteLine("4. Add new animal");
      Console.WriteLine("5. Exit program");
      Console.Write("Select an action: ");
    }

    private static void FindAnimalByIndex(AnimalManager manager) {
      
      Console.Write("Enter animal number: ");
      if (int.TryParse(Console.ReadLine(), out int animalNumber) && animalNumber > incorrectData) {
        manager.ShowAnimalByIndex(animalNumber - 1);
      } else {
        Console.WriteLine("Please enter a valid positive number");
      }
    }

    private static void FindAnimalByName(AnimalManager manager) {
      Console.Write("Enter animal name: ");
      string searchName = Console.ReadLine();
      manager.ShowAnimalByName(searchName);
    }

    private static void AddNewAnimal(AnimalManager manager) {
      string animalType = SelectAnimalType();
      if (animalType == null) return;

      Animal newAnimal = CreateAnimalByType(animalType);
      if (newAnimal != null) {
        manager.AddAnimal(newAnimal);
      }
    }

    private static string SelectAnimalType() {
      
      Console.WriteLine("\nSelect animal type:");
      Console.WriteLine("1. Mammal");
      Console.WriteLine("2. Bird");
      Console.WriteLine("3. Fish");
      Console.WriteLine("4. Reptile");
      Console.WriteLine("5. Amphibian");
      Console.Write("Your choice: ");

      string animalTypeChoice = Console.ReadLine();
      
      if (animalTypeChoice != MammalType && 
          animalTypeChoice != BirdType && 
          animalTypeChoice != FishType && 
          animalTypeChoice != ReptileType && 
          animalTypeChoice != AmphibianType) {
        
        Console.WriteLine("Invalid animal type");
        return null;
      }

      return animalTypeChoice;
    }

    private static Animal CreateAnimalByType(string animalType) {
      Console.Write("Enter name: ");
      string animalName = Console.ReadLine();

      Console.Write("Enter age: ");
      if (!int.TryParse(Console.ReadLine(), out int animalAge) || animalAge < incorrectData) {
        Console.WriteLine("Invalid age format");
        return null;
      }

      Console.Write("Enter habitat: ");
      string animalHabitat = Console.ReadLine();

      Console.Write("Enter diet type: ");
      string animalDiet = Console.ReadLine();

      if (animalType == MammalType) {
        Console.Write("Has fur? (yes/no): ");
        bool hasFur = Console.ReadLine()?.ToLower() == PositiveAnswer;
        return new Mammal(animalName, animalAge, animalHabitat, animalDiet, hasFur);
      }
      else if (animalType == BirdType) {
        Console.Write("Wingspan (m): ");
        if (!double.TryParse(Console.ReadLine(), out double wingSpan) || wingSpan < incorrectData) {
          Console.WriteLine("Invalid wingspan format");
          return null;
        }
        return new Bird(animalName, animalAge, animalHabitat, animalDiet, wingSpan);
      }
      else if (animalType == FishType) {
        Console.Write("Water type: ");
        string waterType = Console.ReadLine();
        return new Fish(animalName, animalAge, animalHabitat, animalDiet, waterType);
      }
      else if (animalType == ReptileType) {
        Console.Write("Is venomous? (yes/no): ");
        bool isVenomous = Console.ReadLine()?.ToLower() == PositiveAnswer;
        return new Reptile(animalName, animalAge, animalHabitat, animalDiet, isVenomous);
      }
      else if (animalType == AmphibianType) {
        Console.Write("Skin moisture: ");
        string skinMoisture = Console.ReadLine();
        return new Amphibian(animalName, animalAge, animalHabitat, animalDiet, skinMoisture);
      }

      return null;
    }
  }
}
