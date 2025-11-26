namespace TaskApi.Dtos.Task_Dtos
{
    public class TaskSummaryDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Completed { get; set; }
        public int? DueDate { get; set; }

    }
}
