namespace ToDoApp.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
