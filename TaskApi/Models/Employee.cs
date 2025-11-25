namespace TaskApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; } = new List<TaskItem>();
    }
}
