using MovieRentalSystem.DAL;
using MovieRentalSystem.Models;
using System;
using System.Collections.Generic;

namespace MovieRentalSystem.BLL
{
    public class MovieService
    {
  
        private readonly MovieRepository _repository = new MovieRepository();

        public void AddMovie()
        {
            Console.Write("Название: ");
            string title = Console.ReadLine();
            Console.Write("Цена в день: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                _repository.AddMovie(new Movie { Title = title, PricePerDay = price, IsAvailable = true });
                Console.WriteLine("Сохранено.");
            }
        }

        public void ShowMovies()
        {
            var movies = _repository.GetAllMovies();
            foreach (var m in movies)
                Console.WriteLine($"{m.Id}: {m.Title} - {m.PricePerDay} руб. ({(m.IsAvailable ? "Доступен" : "В прокате")})");
        }

        public void EditMovie()
        {
            Console.Write("ID для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Новое название: ");
                string title = Console.ReadLine();
                Console.Write("Новая цена: ");
                decimal price = decimal.Parse(Console.ReadLine());
                _repository.UpdateMovie(new Movie { Id = id, Title = title, PricePerDay = price });
            }
        }

        public void DeleteMovie()
        {
            Console.Write("ID для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _repository.DeleteMovie(id);
                Console.WriteLine("Удалено.");
            }
        }
    }
}