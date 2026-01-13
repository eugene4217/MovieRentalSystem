using MovieRentalSystem.DAL;
using MovieRentalSystem.Models;
using System;

namespace MovieRentalSystem.BLL
{
    public class ClientService
    {
        private readonly ClientRepository _repository = new ClientRepository();

        public void AddClient()
        {
            Console.Write("ФИО клиента: ");
            string name = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(name))
            {
                _repository.AddClient(name);
            }
            else
            {
                Console.WriteLine("Ошибка: ФИО не может быть пустым.");
            }
        
        }

        public void ShowClients()
        {
            Console.WriteLine("\n--- СПИСОК КЛИЕНТОВ ---");
            var clients = _repository.GetAllClientsList();
            if (clients.Count == 0)
            {
                Console.WriteLine("Список пуст.");
            }
            else
            {
                foreach (var c in clients)
                {
                    Console.WriteLine($"ID: {c.Id} | ФИО: {c.FullName}");
                }
            }
        }

        public void EditClient()
        {
            Console.Write("ID клиента: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Новое ФИО: ");
            string name = Console.ReadLine();
            _repository.UpdateClient(new Client { Id = id, FullName = name });
        }

        public void DeleteClient()
        {
            Console.Write("ID для удаления: ");
            int id = int.Parse(Console.ReadLine());
            _repository.DeleteClient(id);
        }
    }
}