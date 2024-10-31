namespace ToDoApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
