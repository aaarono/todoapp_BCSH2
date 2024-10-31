using System.Data.SQLite;

namespace ToDoApp.Services
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=todoapp.db";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Categories (
                    CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER,
                    CategoryName TEXT,
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                );
                CREATE TABLE IF NOT EXISTS Tasks (
                    TaskId INTEGER PRIMARY KEY AUTOINCREMENT,
                    CategoryId INTEGER,
                    UserId INTEGER,
                    TaskName TEXT,
                    IsCompleted BOOLEAN,
                    FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId),
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                );";
                command.ExecuteNonQuery();
            }
        }
    }
}
