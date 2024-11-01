using System;
using System.Security.Cryptography;

namespace ToDoApp.Services
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            // Генерируем соль
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Хешируем пароль с солью
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Комбинируем соль и хеш
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Преобразуем в строку для хранения в базе данных
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string savedPasswordHash)
        {
            // Получаем байты из сохраненного хеша
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Извлекаем соль
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Хешируем введенный пароль с той же солью
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Сравниваем результаты
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
