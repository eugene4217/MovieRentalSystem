using System.Data.SQLite;
using System.Collections.Generic;
using MovieRentalSystem.Models;
using System;

namespace MovieRentalSystem.DAL
{
    public class RentalRepository : RepositoryBase
    {
    
        public void RentMovie(int mId, int cId, DateTime date)
        {
            using var conn = GetConnection(); 
            using var cmd = new SQLiteCommand("INSERT INTO Rentals (MovieId, ClientId, RentDate) VALUES (@m, @c, @d)", conn);
            cmd.Parameters.AddWithValue("@m", mId);
            cmd.Parameters.AddWithValue("@c", cId);
            cmd.Parameters.AddWithValue("@d", date);
            cmd.ExecuteNonQuery();
        }

        public List<Rental> GetAllRentals()
        {
            var list = new List<Rental>();
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("SELECT * FROM Rentals WHERE ActualReturnDate IS NULL", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Rental
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    MovieId = Convert.ToInt32(reader["MovieId"]),
                    ClientId = Convert.ToInt32(reader["ClientId"]),
                    RentDate = Convert.ToDateTime(reader["RentDate"])
                });
            }
            return list;
        }
        public void ReturnMovie(int rentalId, DateTime actualReturn)
        {
            using var conn = GetConnection();

            string getMovieIdSql = "SELECT MovieId FROM Rentals WHERE Id = @id";
            int movieId;
            using (var cmdGet = new SQLiteCommand(getMovieIdSql, conn))
            {
                cmdGet.Parameters.AddWithValue("@id", rentalId);
                var result = cmdGet.ExecuteScalar();
                if (result == null) throw new Exception("Прокат не найден");
                movieId = Convert.ToInt32(result);
            }

          
            using var cmdRental = new SQLiteCommand("UPDATE Rentals SET ActualReturnDate = @date WHERE Id = @id", conn);
            cmdRental.Parameters.AddWithValue("@date", actualReturn.ToString("yyyy-MM-dd HH:mm:ss"));
            cmdRental.Parameters.AddWithValue("@id", rentalId);
            cmdRental.ExecuteNonQuery();
            using var cmdMovie = new SQLiteCommand("UPDATE Movies SET IsAvailable = 1 WHERE Id = @mId", conn);
            cmdMovie.Parameters.AddWithValue("@mId", movieId);
            cmdMovie.ExecuteNonQuery();
        }
    }
}