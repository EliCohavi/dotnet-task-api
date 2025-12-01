using TaskApi.Dtos.Employee_Dtos;

namespace TaskApi.Dtos
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }   // changed to DateTime?
        public List<EmployeeSummaryDto>? Employees { get; set; }
    }
}
