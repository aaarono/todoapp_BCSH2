using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private User currentUser;
        private Category? selectedCategory;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public TaskViewModel(User user)
        {
            currentUser = user;
            Categories = new ObservableCollection<Category>(CategoryService.GetCategories(currentUser.UserId));
            Tasks = new ObservableCollection<TaskItem>();
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

        private void LoadTasks()
        {
            Tasks.Clear();
            if (selectedCategory != null)
            {
                foreach (var task in TaskService.GetTasks(currentUser.UserId, selectedCategory.CategoryId))
                {
                    Tasks.Add(task);
                }
            }
        }

        public ICommand AddCategoryCommand => new RelayCommand(AddCategory);
        public ICommand AddTaskCommand => new RelayCommand(AddTask);
        public ICommand CompleteTaskCommand => new RelayCommand(CompleteTask);

        private void AddCategory()
        {
            var categoryName = "New Category";
            CategoryService.AddCategory(currentUser.UserId, categoryName);
            Categories.Add(new Category { UserId = currentUser.UserId, CategoryName = categoryName });
        }

        private void AddTask()
        {
            if (selectedCategory != null)
            {
                var taskName = "New Task";
                TaskService.AddTask(currentUser.UserId, selectedCategory.CategoryId, taskName);
                Tasks.Add(new TaskItem { TaskName = taskName, CategoryId = selectedCategory.CategoryId, UserId = currentUser.UserId });
            }
        }

        private void CompleteTask(object taskObj)
        {
            if (taskObj is TaskItem task)
            {
                task.IsCompleted = true;
                TaskService.MarkTaskAsCompleted(task.TaskId);
                Tasks.Remove(task);
            }
        }
    }
}
