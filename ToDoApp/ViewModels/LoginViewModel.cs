using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string? username = string.Empty;
        private User? currentUser;

        public string? Username
        {
            get => username;
            set { username = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand => new RelayCommand<object>(Login);
        public ICommand RegisterCommand => new RelayCommand<object>(Register);

        private async void Login(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                var password = passwordBox.Password;
                currentUser = await UserService.AuthenticateUserAsync(username, password);
                passwordBox.Clear();

                if (currentUser != null)
                {
                    var mainWindow = new MainWindow { DataContext = new TaskViewModel(currentUser) };
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void Register(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                var password = passwordBox.Password;
                var success = await UserService.RegisterUserAsync(username!, password);
                passwordBox.Clear();

                if (success)
                {
                    MessageBox.Show("Registration successful. You can now log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Registration failed. Username might be taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
