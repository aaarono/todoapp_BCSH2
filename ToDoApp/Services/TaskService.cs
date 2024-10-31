using System.Collections.Generic;
using System.Data.SQLite;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class TaskService
    {
        private static readonly string connectionString = "Data Source=todoapp.db";

        public static List<TaskItem> GetTasks(int userId, int categoryId)
        {
            var tasks = new List<TaskItem>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tasks WHERE UserId = @userId AND CategoryId = @categoryId";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", categoryId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

        public static void AddTask(int userId, int categoryId, string taskName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Tasks (UserId, CategoryId, TaskName, IsCompleted) VALUES (@userId, @categoryId, @taskName, 0)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@taskName", taskName);
                command.ExecuteNonQuery();
            }
        }

        public static void MarkTaskAsCompleted(int taskId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Tasks SET IsCompleted = 1 WHERE TaskId = @taskId";
                command.Parameters.AddWithValue("@taskId", taskId);
                command.ExecuteNonQuery();
            }
        }
    }
}
