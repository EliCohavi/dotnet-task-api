namespace TaskApi.Models
{
    public class PatchTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Completed { get; set; }
    }
}
