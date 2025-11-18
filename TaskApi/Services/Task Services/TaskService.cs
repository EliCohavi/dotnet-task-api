using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Dtos;
using TaskApi.Models;
using TaskApi.Repositories;
using System.Linq;

namespace TaskApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTasksAsync() {
            var entities = await _taskRepository.GetAllAsync();

            return entities.Select(e => new TaskItemDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Completed = e.Completed,
                DueDate = e.DueDate
            });

        }


        public async Task<TaskItemDto> GetTaskByIdAsync(int id)
        {
            var entity = await _taskRepository.GetByIdAsync(id);

            if (entity == null) throw new KeyNotFoundException("Task not found");

            return new TaskItemDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Completed = entity.Completed,
                DueDate = entity.DueDate
            };
        }


        public async Task<TaskItemDto> CreateTaskAsync(CreateTaskDto dto)
        {
            var entity = new TaskItem // Map Dto to Entity
            {
                Title = dto.Title,
                Description = dto.Description,
                Completed = dto.Completed,
                DueDate = dto.DueDate
            };

            var created = await _taskRepository.AddAsync(entity); // Save to repository

            return new TaskItemDto // Map Entity to Dto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                Completed = created.Completed,
                DueDate = created.DueDate
            };
        }


        public async Task<TaskItemDto> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var existing = await _taskRepository.GetByIdAsync(id); // Retrieve existing entity from repository
            if (existing == null) throw new KeyNotFoundException("Task not found");

            // Field updates
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Completed = dto.Completed;
            existing.DueDate = dto.DueDate;

            var updated = await _taskRepository.UpdateAsync(existing); // Save changes to repository

            var updatedDto = new TaskItemDto // Map entity back to Dto
            {
                Id = updated.Id,
                Title = updated.Title,
                Description = updated.Description,
                Completed = updated.Completed,
                DueDate = updated.DueDate
            };

            return updatedDto;
        }

        public async Task<TaskItemDto> PartialUpdateTaskAsync(int id, PatchTaskDto dto)
        {
            var existing = await _taskRepository.GetByIdAsync(id); // Retrieve existing entity from repository
            if (existing == null) throw new KeyNotFoundException("Task not found");

            // Partial field updates
            if (dto.Title != null) existing.Title = dto.Title;
            if (dto.Description != null) existing.Description = dto.Description;
            if (dto.Completed != null) existing.Completed = dto.Completed.Value;
            if (dto.DueDate != null) existing.DueDate = dto.DueDate;

            var updated = await _taskRepository.PartialUpdateAsync(existing); // Save changes to repository

            var updatedDto = new TaskItemDto // Map entity back to Dto
            {
                Id = updated.Id,
                Title = updated.Title,
                Description = updated.Description,
                Completed = updated.Completed,
                DueDate = updated.DueDate
            };

            return updatedDto;
        }

        public async Task<IEnumerable<TaskItemDto>> GetUpcomingTasksAsync()
        {
            var allTasks = await _taskRepository.GetAllAsync();
            var upcomingTasks = allTasks
                .Where(t => t.DueDate != null)
                .OrderBy(t => t.DueDate);


            return upcomingTasks.Select(e => new TaskItemDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Completed = e.Completed,
                DueDate = e.DueDate
            });
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var existing = await _taskRepository.GetByIdAsync(id);
            if (existing == null) return false;
            await _taskRepository.DeleteAsync(id);
            return true;
        }


    }
}