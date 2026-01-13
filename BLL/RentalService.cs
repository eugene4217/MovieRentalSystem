using MovieRentalSystem.DAL;
using MovieRentalSystem.Models;
using System;
using System.IO;

namespace MovieRentalSystem.BLL
{
    public class RentalService
    {
        private readonly RentalRepository _repository = new RentalRepository();

        public void RentMovie(int movieId, int clientId, DateTime plannedReturn)
        {
     
            _repository.RentMovie(movieId, clientId, plannedReturn);

 
            string receiptPath = $"Receipt_Rent_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string content = $"""
                === ЧЕК АРЕНДЫ ФИЛЬМА ===
                Дата: {DateTime.Now}
                ID Фильма: {movieId}
                ID Клиента: {clientId}
                Срок возврата: {plannedReturn:yyyy-MM-dd}
                --------------------------
                Спасибо, что выбрали нас!
                """;

            File.WriteAllText(receiptPath, content);
            Console.WriteLine($"\nПрокат оформлен! Чек сохранен в файл: {receiptPath}");
        }

        public void ReturnMovie(int rentalId, DateTime actualReturn)
        {

            _repository.ReturnMovie(rentalId, actualReturn);


            string receiptPath = $"Receipt_Return_{rentalId}_{DateTime.Now:yyyyMMdd}.txt";
            string content = $"""
        === ЧЕК ВОЗВРАТА ФИЛЬМА ===
        ID Проката: {rentalId}
        Дата возврата: {actualReturn:yyyy-MM-dd HH:mm:ss}
        --------------------------
        Статус: Принято
        """;

            File.WriteAllText(receiptPath, content);
            Console.WriteLine($"Фильм по прокату №{rentalId} успешно возвращен.");
            Console.WriteLine($"Чек возврата сохранен: {receiptPath}");
        }

        public void ShowRentals()
        {
            Console.Clear();
            Console.WriteLine("--- СПИСОК АКТИВНЫХ ПРОКАТОВ ---");

           
            var rentals = _repository.GetAllRentals().Where(r => r.ActualReturnDate == null).ToList();

            if (rentals.Count == 0)
            {
                Console.WriteLine("\nНа данный момент активных прокатов нет.");
            }
            else
            {
                foreach (var r in rentals)
                {
                    Console.WriteLine($"ID Проката: {r.Id} | Фильм ID: {r.MovieId} | Клиент ID: {r.ClientId} | Дата: {r.RentDate:dd.MM.yyyy}");
                }
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        }
    }
}