using MovieRentalSystem.Models;
using System.Data.SQLite;

namespace MovieRentalSystem.DAL
{
    public class ClientRepository : RepositoryBase
    {
        public List<Client> SearchClients(string term)
        {
            var list = new List<Client>();
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("SELECT * FROM Clients WHERE FullName LIKE @term", conn);
            cmd.Parameters.AddWithValue("@term", $"%{term}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Client
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FullName = reader["FullName"].ToString() ?? ""
                });
            }
            return list;
        }
        public void AddClient(string fullName)
        {
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("INSERT INTO Clients (FullName) VALUES (@name)", conn);
            cmd.Parameters.AddWithValue("@name", fullName);
            cmd.ExecuteNonQuery();
        }

   
        public List<Client> GetAllClientsList()
        {
            var clients = new List<Client>();
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("SELECT * FROM Clients", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FullName = reader["FullName"].ToString() ?? ""
                });
            }
            return clients;
        }
        public void UpdateClient(Client client)
        {
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("UPDATE Clients SET FullName = @name WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@name", client.FullName);
            cmd.Parameters.AddWithValue("@id", client.Id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteClient(int id)
        {
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("DELETE FROM Clients WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}