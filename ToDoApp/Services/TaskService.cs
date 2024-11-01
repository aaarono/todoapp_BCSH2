using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class TaskService
    {
        private static readonly string connectionString = "Data Source=todoapp.db";

        public static async Task<List<TaskItem>> GetTasksAsync(int userId, int categoryId)
        {
            var tasks = new List<TaskItem>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TaskId, CategoryId, UserId, TaskName, IsCompleted FROM Tasks WHERE UserId = @userId AND CategoryId = @categoryId";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", categoryId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tasks.Add(new TaskItem
                        {
                            TaskId = reader.GetInt32(0),
                            CategoryId = reader.GetInt32(1),
                            UserId = reader.GetInt32(2),
                            TaskName = reader.GetString(3),
                            IsCompleted = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return tasks;
        }

        public static async Task AddTaskAsync(int userId, int categoryId, string taskName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Tasks (UserId, CategoryId, TaskName, IsCompleted) VALUES (@userId, @categoryId, @taskName, 0)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@taskName", taskName);
                await command.ExecuteNonQueryAsync();
            }
        }

        public static async Task MarkTaskAsCompletedAsync(int taskId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Tasks SET IsCompleted = 1 WHERE TaskId = @taskId";
                command.Parameters.AddWithValue("@taskId", taskId);
                await command.ExecuteNonQueryAsync();
            }
        }

        public static async Task UpdateTaskAsync(TaskItem task)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Tasks SET TaskName = @taskName, IsCompleted = @isCompleted WHERE TaskId = @taskId";
                command.Parameters.AddWithValue("@taskName", task.TaskName);
                command.Parameters.AddWithValue("@isCompleted", task.IsCompleted);
                command.Parameters.AddWithValue("@taskId", task.TaskId);
                await command.ExecuteNonQueryAsync();
            }
        }

        public static async Task DeleteTaskAsync(int taskId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Tasks WHERE TaskId = @taskId";
                command.Parameters.AddWithValue("@taskId", taskId);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
