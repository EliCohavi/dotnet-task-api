namespace TaskApi.Dtos
{
    public class PatchTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Completed { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
