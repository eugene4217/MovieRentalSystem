using System.Data.SQLite;
using MovieRentalSystem.Models;
using System.Collections.Generic;
using System;

namespace MovieRentalSystem.DAL
{
    public class MovieRepository : RepositoryBase
    {
        public void AddMovie(Movie movie)
        {
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("INSERT INTO Movies (Title, PricePerDay, IsAvailable) VALUES (@title, @price, @avail)", conn);
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@price", movie.PricePerDay);
            cmd.Parameters.AddWithValue("@avail", movie.IsAvailable ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("SELECT * FROM Movies", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movies.Add(new Movie
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Title = reader["Title"].ToString(),
                    PricePerDay = Convert.ToDecimal(reader["PricePerDay"]),
                    IsAvailable = Convert.ToInt32(reader["IsAvailable"]) == 1
                });
            }
            return movies;
        }

        public void UpdateMovie(Movie movie)
        {
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("UPDATE Movies SET Title = @title, PricePerDay = @price WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@price", movie.PricePerDay);
            cmd.Parameters.AddWithValue("@id", movie.Id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteMovie(int id)
        {
            using var conn = GetConnection();
            using var cmd = new SQLiteCommand("DELETE FROM Movies WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}