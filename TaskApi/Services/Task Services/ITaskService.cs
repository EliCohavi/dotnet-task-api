using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Dtos;
using TaskApi.Models;

namespace TaskApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllTasksAsync();
        Task<TaskItemDto> GetTaskByIdAsync(int id);
        Task<TaskItemDto> CreateTaskAsync(CreateTaskDto dto);
        Task<TaskItemDto> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<TaskItemDto> PartialUpdateTaskAsync(int id, PatchTaskDto dto);
        Task<IEnumerable<TaskItemDto>> GetUpcomingTasksAsync();
        Task<bool> DeleteTaskAsync(int id);
    }
}