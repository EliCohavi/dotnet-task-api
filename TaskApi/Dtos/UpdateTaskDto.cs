namespace TaskApi.Dtos
{
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public int DueDate { get; set; }
    }
}
