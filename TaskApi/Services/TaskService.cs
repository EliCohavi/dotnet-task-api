using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Models;
using TaskApi.Repositories;

namespace TaskApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<IEnumerable<TaskItem>> GetAllTasksAsync() => _taskRepository.GetAllAsync();


        public Task<TaskItem> GetTaskByIdAsync(int id) => _taskRepository.GetByIdAsync(id);


        public Task<TaskItem> CreateTaskAsync(TaskItem taskItem) => _taskRepository.AddAsync(taskItem);

        public async Task UpdateTaskAsync(int id, TaskItem taskItem)
        {
            var existing = await _taskRepository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Task not found");
            // Field updates
            existing.Title = taskItem.Title;
            existing.Description = taskItem.Description;
            existing.Completed = taskItem.Completed;
            await _taskRepository.UpdateAsync(existing);
        }

        public Task DeleteTaskAsync(int id) => _taskRepository.DeleteAsync(id);

        public Task<TaskItem> UpdateTaskAsync(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITaskService.DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<TaskItem> ITaskService.UpdateTaskAsync(int id, TaskItem taskItem)
        {
            throw new NotImplementedException();
        }
    }
}