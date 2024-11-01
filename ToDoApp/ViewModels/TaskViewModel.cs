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

        public ICommand AddCategoryCommand => new RelayCommand(AddCategory);
        public ICommand AddTaskCommand => new RelayCommand(AddTask);
        public ICommand CompleteTaskCommand => new RelayCommand<object>(CompleteTask);
        public ICommand EditTaskCommand => new RelayCommand<object>(EditTask);
        public ICommand DeleteTaskCommand => new RelayCommand<object>(DeleteTask);

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
                Tasks.Remove(task);
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
    }
}
