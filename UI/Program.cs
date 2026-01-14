using MovieRentalSystem.BLL;
using MovieRentalSystem.DAL;
using MovieRentalSystem.Models;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DatabaseContext.Initialize();

        MovieService movieService = new();
        ClientService clientService = new();
        RentalService rentalService = new();
        ReportService reportService = new();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("""
=== СИСТЕМА ПРОКАТА ФИЛЬМОВ ===
1. Управление фильмами
2. Управление клиентами
3. Управление прокатом
4. Отчёты
0. Выход
""");
            Console.Write("\nВыберите пункт меню: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": MovieMenu(movieService); break;
                case "2": ClientMenu(clientService); break;
                case "3": RentalMenu(rentalService); break;
                case "4": reportService.ShowReports(); Console.ReadKey(); break;
                case "0": return;
                default:
                    Console.WriteLine("Ошибка: Такого пункта нет! Нажмите любую клавишу...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MovieMenu(MovieService service)
    {
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("""
--- УПРАВЛЕНИЕ ФИЛЬМАМИ ---
1. Добавить фильм
2. Показать все
3. Редактировать
4. Удалить
5. Найти фильм
0. Назад
""");
            Console.Write("\nВведите цифру: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "0") break; 

            switch (choice)
            {
                case "1": service.AddMovie(); Console.ReadKey(); break;
                case "2": service.ShowMovies(); Console.ReadKey(); break;
                case "3": service.EditMovie(); Console.ReadKey(); break;
                case "4": service.DeleteMovie(); Console.ReadKey(); break;
                case "5": service.SearchMovie(); Console.ReadKey(); break;
                default:
                    Console.WriteLine("Неверный ввод! Попробуйте еще раз...");
                    Thread.Sleep(1000); 
                    break;
            }
        }
    }

    static void ClientMenu(ClientService service)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("""
--- УПРАВЛЕНИЕ КЛИЕНТАМИ ---
1. Добавить клиента
2. Показать всех
3. Редактировать
4. Удалить
5. Найти клиента
0. Назад
""");
            Console.Write("\nВведите цифру: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "0") break;

            switch (choice)
            {
                case "1": service.AddClient(); Console.ReadKey(); break;
                case "2": service.ShowClients(); Console.ReadKey(); break;
                case "3": service.EditClient(); Console.ReadKey(); break;
                case "4": service.DeleteClient(); Console.ReadKey(); break;
                case "5": service.SearchClient(); Console.ReadKey(); break;
                default:
                    Console.WriteLine("Ошибка ввода! Такого действия не существует.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    static void RentalMenu(RentalService service)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("""
--- УПРАВЛЕНИЕ ПРОКАТОМ ---
1. Оформить прокат
2. Вернуть фильм
3. Показать все прокаты
0. Назад
""");
            Console.Write("\nВведите цифру: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    try {
                        Console.Write("ID фильма: ");
                        int mId = int.Parse(Console.ReadLine()!);
                        Console.Write("ID клиента: ");
                        int cId = int.Parse(Console.ReadLine()!);
                        Console.Write("Дата возврата (гггг-мм-дд): ");
                        DateTime date = DateTime.Parse(Console.ReadLine()!);
                        service.RentMovie(mId, cId, date);
                    } catch { Console.WriteLine("Ошибка: Введены некорректные данные."); }
                    Console.ReadKey();
                    break;
                case "2":
                    try {
                        Console.Write("ID проката: ");
                        int rId = int.Parse(Console.ReadLine()!);
                        service.ReturnMovie(rId, DateTime.Now);
                    } catch { Console.WriteLine("Ошибка: Неверный ID."); }
                    Console.ReadKey();
                    break;
                case "3": service.ShowRentals(); Console.ReadKey(); break;
                default:
                    Console.WriteLine("Неверный выбор. Повторите.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}