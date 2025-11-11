using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(TaskItem taskItem);
        Task<TaskItem> UpdateTaskAsync(int id, TaskItem taskItem);
        Task<bool> DeleteTaskAsync(int id);
    }
}