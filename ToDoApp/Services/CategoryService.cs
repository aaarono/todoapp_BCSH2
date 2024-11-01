using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class CategoryService
    {
        private static readonly string connectionString = "Data Source=todoapp.db";

        public static async Task<List<Category>> GetCategoriesAsync(int userId)
        {
            var categories = new List<Category>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT CategoryId, UserId, CategoryName FROM Categories WHERE UserId = @userId";
                command.Parameters.AddWithValue("@userId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
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

        public static async Task AddCategoryAsync(int userId, string categoryName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Categories (UserId, CategoryName) VALUES (@userId, @categoryName)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryName", categoryName);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
