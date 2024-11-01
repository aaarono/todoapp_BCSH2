using System.Configuration;
using System.Data;
using System.Windows;
using ToDoApp.Services;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeComponent();
            await Task.Run(() => DatabaseHelper.InitializeDatabase());
        }
    }

}
