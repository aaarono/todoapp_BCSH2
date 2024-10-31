using System.Collections.Generic;
using System.Data.SQLite;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class CategoryService
    {
        private static readonly string connectionString = "Data Source=todoapp.db";

        public static List<Category> GetCategories(int userId)
        {
            var categories = new List<Category>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Categories WHERE UserId = @userId";
                command.Parameters.AddWithValue("@userId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            CategoryName = reader.GetString(2)
                        });
                    }
                }
            }
            return categories;
        }

        public static void AddCategory(int userId, string categoryName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Categories (UserId, CategoryName) VALUES (@userId, @categoryName)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryName", categoryName);
                command.ExecuteNonQuery();
            }
        }
    }
}
