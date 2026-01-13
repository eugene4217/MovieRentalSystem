using System.Data.SQLite;

namespace MovieRentalSystem.DAL
{
    public static class DatabaseContext
    {
        public const string ConnectionString = "Data Source=movies.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static void Initialize()
        {
            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Movies (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT,
                    PricePerDay REAL,
                    IsAvailable INTEGER
                );
                CREATE TABLE IF NOT EXISTS Clients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT
                );
                CREATE TABLE IF NOT EXISTS Rentals (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    MovieId INTEGER,
                    ClientId INTEGER,
                    RentDate TEXT,
                    PlannedReturnDate TEXT,
                    ActualReturnDate TEXT,
                    TotalCost REAL,
                    Penalty REAL
                );";
            cmd.ExecuteNonQuery();
        }
    }
}