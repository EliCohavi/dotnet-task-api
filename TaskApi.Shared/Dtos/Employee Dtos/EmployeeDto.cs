using TaskApi.Dtos.Task_Dtos;

namespace TaskApi.Dtos.Employee_Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<TaskSummaryDto>? Tasks { get; set; }
    }
}
