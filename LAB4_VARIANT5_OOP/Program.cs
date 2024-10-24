using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Основний цикл програми для вибору типу відпочинку
        while (running)
        {
            // Виведення меню вибору типу відпочинку
            Console.WriteLine("Виберіть тип відпочинку:");
            Console.WriteLine("1. Активний відпочинок");
            Console.WriteLine("2. Культурний відпочинок");
            Console.WriteLine("3. Пляжний відпочинок");
            Console.WriteLine("4. Круїз");
            Console.WriteLine("5. Вихід");

            // Отримання вибору користувача
            string choice = Console.ReadLine();

            // Поліморфна змінна для зберігання обраного типу відпочинку
            VacationType vacation = null;

            // Обробка вибору користувача
            switch (choice)
            {
                case "1":
                    vacation = new ActiveVacation(); // Активний відпочинок
                    break;
                case "2":
                    vacation = new CulturalVacation(); // Культурний відпочинок
                    break;
                case "3":
                    vacation = new BeachVacation(); // Пляжний відпочинок
                    break;
                case "4":
                    vacation = new CruiseVacation(); // Круїзний відпочинок
                    break;
                case "5":
                    Console.WriteLine("Вихід з програми...");
                    running = false; // Вихід з програми
                    continue;
                default:
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                    continue;
            }

            // Виклик віртуального методу Plan для відповідного типу відпочинку
            vacation.Plan();

            string dataChoice;
            // Цикл для вибору даних (стандартні або введені користувачем)
            do
            {
                Console.WriteLine("Оберіть варіант використання даних:");
                Console.WriteLine("1. Використати стандартні дані");
                Console.WriteLine("2. Ввести власні дані");

                dataChoice = Console.ReadLine();

                if (dataChoice == "1")
                {
                    // Використання стандартних даних
                    SetDefaultVacationOptions(vacation);
                    vacation.IsDefaultLocation = true;  // Використання стандартних пропозицій
                }
                else if (dataChoice == "2")
                {
                    // Введення користувачем власних даних
                    vacation.Days = GetValidDaysInput();
                    vacation.Location = GetValidLocationInput();
                    vacation.IsDefaultLocation = false; // Не використовуються стандартні пропозиції
                }
                else
                {
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                }
            } while (dataChoice != "1" && dataChoice != "2");

            // Вибір методу розрахунку вартості
            Console.WriteLine("Розрахувати вартість:");
            Console.WriteLine("1. На основі кількості днів");
            Console.WriteLine("2. На основі кількості днів та локації");

            string costChoice = Console.ReadLine();
            decimal cost;

            // Виклик перевантажених методів CalculateCost для розрахунку вартості
            if (costChoice == "1")
            {
                cost = vacation.CalculateCost(vacation.Days); // Розрахунок лише за кількістю днів
            }
            else
            {
                cost = vacation.CalculateCost(vacation.Days, vacation.Location); // Розрахунок за кількістю днів і локацією
            }

            Console.WriteLine($"Вартість відпочинку: {cost} грн.");
        }
    }

    // Метод для стандартних пропозицій 
    static void SetDefaultVacationOptions(VacationType vacation)
    {
        // Словник стандартних пропозицій відпочинку для різних типів
        var defaultOptions = new Dictionary<(Type, string), (int Days, string Location)>
        {
            { (typeof(ActiveVacation), "1"), (8, "Похід в Карпати") },
            { (typeof(ActiveVacation), "2"), (6, "Спелеотуризм") },
            { (typeof(ActiveVacation), "3"), (5, "Велотур по Карпатам") },
            { (typeof(CulturalVacation), "1"), (2, "Концертний тур відомих співаків") },
            { (typeof(CulturalVacation), "2"), (4, "Театральний тур") },
            { (typeof(CulturalVacation), "3"), (6, "Культурний фестиваль") },
            { (typeof(BeachVacation), "1"), (5, "Відпочинок на пляжі") },
            { (typeof(BeachVacation), "2"), (7, "Пляжний відпочинок на Середземному морі") },
            { (typeof(BeachVacation), "3"), (14, "Пляжний відпочинок на екзотичних островах") },
            { (typeof(CruiseVacation), "1"), (10, "Карибський круїз") },
            { (typeof(CruiseVacation), "2"), (90, "Круїз навколо світу") },
            { (typeof(CruiseVacation), "3"), (14, "Круїх по Нілу") }
        };
        // Виведення варіантів стандартних пропозицій для обраного типу відпочинку
        Console.WriteLine("Оберіть одну з стандартних пропозицій:");

        if (vacation is ActiveVacation)
        {
            Console.WriteLine("1. Похід в гори. Тривалість: 8 днів");
            Console.WriteLine("2. Спелеотуризм. Тривалість: 6 днів");
            Console.WriteLine("3. Велотур по Карпатам. Тривалість: 5 днів");
        }
        else if (vacation is CulturalVacation)
        {
            Console.WriteLine("1. Концертний тур відомих співаків. Тривалість: 2 дні");
            Console.WriteLine("2. Театральний тур. Тривалість: 4 дні");
            Console.WriteLine("3. Культурний фестиваль. Тривалість: 6 днів");
        }
        else if (vacation is BeachVacation)
        {
            Console.WriteLine("1. Відпочинок на пляжі. Тривалість: 5 днів");
            Console.WriteLine("2. Пляжний відпочинок на Середземному морі . Тривалість: 7 днів");
            Console.WriteLine("3. Пляжний відпочинок на екзотичних островах. Тривалість: 14 днів");
        }
        else if (vacation is CruiseVacation)
        {
            Console.WriteLine("1. Карибський круїз. Тривалість: 10 днів");
            Console.WriteLine("2. Круїз навколо світу. Тривалість: 90 днів");
            Console.WriteLine("3. Круїх по Нілу. Тривалість 14 днів");
        }
        // Обробка вибору стандартної пропозиції
        string option;
        do
        {
            option = Console.ReadLine();
            if (defaultOptions.TryGetValue((vacation.GetType(), option), out var defaultOption))
            {
                vacation.Days = defaultOption.Days;
                vacation.Location = defaultOption.Location;
            }
            else
            {
                Console.WriteLine("Помилка, спробуйте ще раз.");
            }
        } while (!defaultOptions.ContainsKey((vacation.GetType(), option)));
    }

    // Метод для введення і перевірки кількості днів
    static int GetValidDaysInput()
    {
        int days;
        Console.Write("Введіть кількість днів: ");
        // Перевірка, щоб кількість днів була більше 0
        while (!int.TryParse(Console.ReadLine(), out days) || days <= 0)
        {
            Console.WriteLine("Кількість днів повинна бути більше 0. Спробуйте ще раз.");
        }
        return days;
    }

    // Метод для введення і перевірки локації
    static string GetValidLocationInput()
    {
        Console.Write("Введіть місце відпочинку: ");
        string location;
        while (true)
        {
            location = Console.ReadLine();

            // Якщо рядок не порожній, приймаємо його як введення
            if (!string.IsNullOrEmpty(location))
            {
                break;
            }
            Console.WriteLine("Місце не може бути порожнім. Спробуйте ще раз.");
        }
        return location;
    }
}

// Базовий клас для всіх типів відпочинку
class VacationType
{
    public int Days { get; set; } // Властивість кількість днів
    public string Location { get; set; } // Властивість місце відпочинку
    public bool IsDefaultLocation { get; set; } = false;  // позначення чи використано стандартну пропозицію

    // Віртуальний метод для планування відпочинку (взагалі реалізується в похідних класах)
    public virtual void Plan()
    {
        Console.WriteLine("Планування відпочинку...");
    }

    // Перевантажений метод для розрахунку вартості за кількістю днів
    public decimal CalculateCost(int days)
    {
        if (days <= 0)
        {
            Console.WriteLine("Кількість днів повинна бути більше 0.");
            return 0;
        }
        return days * 100; // Сам вигляд розрахунку, мається на увазі що кількість днів множиться на 100
    }

    // Перевантажений метод для розрахунку вартості за кількістю днів і локацією
    public decimal CalculateCost(int days, string location)
    {
        if (days <= 0)
        {
            Console.WriteLine("Кількість днів повинна бути більше 0.");
            return 0;
        }

        if (string.IsNullOrEmpty(location))
        {
            Console.WriteLine("Локація не може бути порожньою.");
            return 0;
        }

        // Розрахунок додаткової вартості за локацію
        decimal additionalCostPercentage = IsDefaultLocation ? 0.50m : 0.75m;// Реалізація розрахунку на основі того що локація це як певний відсоток ( продовження тексту нижче)
        decimal additionalCost = days * 100 * additionalCostPercentage; //якщо стандартна пропозиція то 50% якщо користувач використовує власну локацію то 75%

        return CalculateCost(days) + additionalCost;
    }
}
// Похідні класи для різних типів відпочинку
class ActiveVacation : VacationType
{
    public override void Plan()
    {
        Console.WriteLine("Планування активного відпочинку...");
    }
}

class CulturalVacation : VacationType
{
    public override void Plan()
    {
        Console.WriteLine("Планування культурного відпочинку...");
    }
}

class BeachVacation : VacationType
{
    public override void Plan()
    {
        Console.WriteLine("Планування пляжного відпочинку...");
    }
}
class CruiseVacation : VacationType
{
    public override void Plan()
    {
        Console.WriteLine("Планування круїзного відпочинку...");
    }
}
