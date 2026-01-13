using MovieRentalSystem.DAL;
using System;
using System.IO;

namespace MovieRentalSystem.BLL
{
    public class ReportService
    {
        private readonly RentalRepository _repository = new RentalRepository();

        public void ShowReports()
        {
            var rentals = _repository.GetAllRentals();
            string fileName = $"Report_{DateTime.Now:yyyyMMdd}.txt";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("=== ОТЧЕТ ПО ПРОКАТАМ ===");
                sw.WriteLine($"Дата формирования: {DateTime.Now}");
                sw.WriteLine("--------------------------");

                foreach (var r in rentals)
                {
                    string line = $"ID: {r.Id} | Фильм ID: {r.MovieId} | Клиент ID: {r.ClientId} | Дата: {r.RentDate}";
                    Console.WriteLine(line);
                    sw.WriteLine(line);
                }
            }
            Console.WriteLine($"\nОтчет сохранен в файл: {fileName}");
        }
    }
}