namespace TaskApi.Dtos
{
    public class CreateTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
    }
}
