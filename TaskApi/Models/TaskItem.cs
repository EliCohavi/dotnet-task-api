namespace TaskApi.Models
{
    // No annotation required for simple entity
    public class TaskItem
    {
        public int Id { get; set; } // EF Core will treat 'Id' as the primary key by convention
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public int? DueDate { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}