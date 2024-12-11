using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private User currentUser;
        private Category? selectedCategory;

        // Новое свойство для приветствия
        public string GreetingMessage => $"Hello, {currentUser.Username}!";

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<TaskItem> Tasks { get; set; } = new ObservableCollection<TaskItem>();

        public TaskViewModel(User user)
        {
            currentUser = user;
            LoadCategories();
        }

        public Category? SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
                LoadTasks();
            }
        }

        // Команды для категорий
        public ICommand AddCategoryCommand => new RelayCommand(AddCategory);
        public ICommand EditCategoryCommand => new RelayCommand(EditCategory, () => SelectedCategory != null);
        public ICommand DeleteCategoryCommand => new RelayCommand(DeleteCategory, () => SelectedCategory != null);

        // Команды для задач
        public ICommand AddTaskCommand => new RelayCommand(AddTask);
        public ICommand CompleteTaskCommand => new RelayCommand<object>(CompleteTask);
        public ICommand EditTaskCommand => new RelayCommand<object>(EditTask);
        public ICommand DeleteTaskCommand => new RelayCommand<object>(DeleteTask);

        // Дополнительный функционал - Logout
        public ICommand LogoutCommand => new RelayCommand(Logout);

        private async void LoadCategories()
        {
            Categories.Clear();
            var categories = await CategoryService.GetCategoriesAsync(currentUser.UserId);
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        private async void LoadTasks()
        {
            Tasks.Clear();
            if (selectedCategory != null)
            {
                var tasks = await TaskService.GetTasksAsync(currentUser.UserId, selectedCategory.CategoryId);
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
        }

        private async void AddCategory()
        {
            var dialog = new InputDialog("Enter category name:");
            if (dialog.ShowDialog() == true)
            {
                var categoryName = dialog.ResponseText;
                await CategoryService.AddCategoryAsync(currentUser.UserId, categoryName);
                Categories.Add(new Category { UserId = currentUser.UserId, CategoryName = categoryName });
            }
        }

        private async void EditCategory()
        {
            if (SelectedCategory != null)
            {
                var dialog = new InputDialog("Edit category name:")
                {
                    ResponseText = SelectedCategory.CategoryName
                };
                if (dialog.ShowDialog() == true)
                {
                    var newName = dialog.ResponseText;
                    await CategoryService.UpdateCategoryAsync(SelectedCategory.CategoryId, newName);
                    SelectedCategory.CategoryName = newName;
                    // Обновим список категорий вручную, если нужно:
                    // LoadCategories(); или просто OnPropertyChanged("Categories")
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }

        private async void DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this category and all its tasks?",
                                             "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await CategoryService.DeleteCategoryAsync(SelectedCategory.CategoryId);
                    Categories.Remove(SelectedCategory);
                    SelectedCategory = null;
                    Tasks.Clear();
                }
            }
        }

        private async void AddTask()
        {
            if (selectedCategory != null)
            {
                var dialog = new InputDialog("Enter task name:");
                if (dialog.ShowDialog() == true)
                {
                    var taskName = dialog.ResponseText;
                    await TaskService.AddTaskAsync(currentUser.UserId, selectedCategory.CategoryId, taskName);
                    Tasks.Add(new TaskItem { TaskName = taskName, CategoryId = selectedCategory.CategoryId, UserId = currentUser.UserId });
                }
            }
            else
            {
                MessageBox.Show("Please select a category first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void CompleteTask(object taskObj)
        {
            if (taskObj is TaskItem task)
            {
                task.IsCompleted = true;
                await TaskService.MarkTaskAsCompletedAsync(task.TaskId);
                // Обновляем задачи, если нужно
                OnPropertyChanged(nameof(Tasks));
            }
        }

        private async void EditTask(object taskObj)
        {
            if (taskObj is TaskItem task)
            {
                var dialog = new InputDialog("Edit task name:")
                {
                    ResponseText = task.TaskName
                };
                if (dialog.ShowDialog() == true)
                {
                    task.TaskName = dialog.ResponseText;
                    await TaskService.UpdateTaskAsync(task);
                    LoadTasks();
                }
            }
        }

        private async void DeleteTask(object taskObj)
        {
            if (taskObj is TaskItem task)
            {
                await TaskService.DeleteTaskAsync(task.TaskId);
                Tasks.Remove(task);
            }
        }

        private void Logout()
        {
            // Закрываем текущее окно (MainWindow) и открываем LoginWindow
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = loginWindow;
        }
    }
}
