using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string? username = string.Empty;
        private string? password = string.Empty;
        private User? currentUser;

        public string? Username
        {
            get => username;
            set { username = value; OnPropertyChanged(); }
        }

        public string? Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand => new RelayCommand(Login);
        public ICommand RegisterCommand => new RelayCommand(Register);

        private void Login()
        {
            currentUser = UserService.AuthenticateUser(username, password);
            if (currentUser != null)
            {
                var mainWindow = new MainWindow { DataContext = new TaskViewModel(currentUser) };
                mainWindow.Show();
            }
            else
            {
                // TODO ShowError
            }
        }

        private void Register()
        {
            var success = UserService.RegisterUser(username!, password!);
            if (success)
            {
                // TODO Registration success
            }
            else
            {
                // TODO ShowError
            }
        }
    }
}
