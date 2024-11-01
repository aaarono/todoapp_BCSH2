using System.Data.SQLite;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class UserService
    {
        private static readonly string connectionString = "Data Source=todoapp.db";

        public static async Task<User?> AuthenticateUserAsync(string? username, string? password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT UserId, Username, Password FROM Users WHERE Username = @username";
                command.Parameters.AddWithValue("@username", username);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var savedPasswordHash = reader.GetString(2);
                        if (SecurityHelper.VerifyPassword(password!, savedPasswordHash))
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<bool> RegisterUserAsync(string username, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                command.Parameters.AddWithValue("@username", username);
                var hashedPassword = SecurityHelper.HashPassword(password);
                command.Parameters.AddWithValue("@password", hashedPassword);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
