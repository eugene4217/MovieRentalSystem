using System.Data.SQLite;

namespace MovieRentalSystem.DAL
{
    public abstract class RepositoryBase
    {
        protected SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection("Data Source=movies.db;Version=3;");
            connection.Open();
            return connection;
        }
    }
}