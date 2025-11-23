using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using TaskApi.Services;
using System.Threading.Tasks;
using TaskApi.Dtos;
using System.Linq;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();

            var dtoList = tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Completed = t.Completed,
                DueDate = t.DueDate
            });

            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            
            var dto = new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Completed = task.Completed,
                DueDate = task.DueDate
            };
            return Ok(dto);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingTasks()
        {
            var tasks = await _taskService.GetUpcomingTasksAsync();
            return Ok(tasks);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto) // Takes Dto, Maps to Model
        { 

            var createdTaskDto = await _taskService.CreateTaskAsync(dto); // Create task by calling service. Returns created TaskItem model.

            var responseDto = new TaskItemDto // Maps dto to response dto with id.
            {
                Id = createdTaskDto.Id,
                Title = createdTaskDto.Title,
                Description = createdTaskDto.Description,
                Completed = createdTaskDto.Completed,
                DueDate = createdTaskDto.DueDate
            };

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTaskDto.Id }, responseDto);


        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            try
            {
                await _taskService.UpdateTaskAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartialUpdateTask(int id, PatchTaskDto dto)
        {
            try
            {
                await _taskService.PartialUpdateTaskAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST /tasks/{taskId}/employees/{employeeId}
        [HttpPost("{taskId:int}/employees/{employeeId:int}")]
        public async Task<IActionResult> AssignTaskToEmployee(int taskId, int employeeId)
        {
            try
            {
                await _taskService.AssignAsync(taskId, employeeId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE /tasks/{taskId}/employees/{employeeId}
        [HttpDelete("{taskId:int}/employees/{employeeId:int}")]
        public async Task<IActionResult> RemoveTaskFromEmployee(int taskId, int employeeId)
        {
            try
            {
                await _taskService.RemoveAssignmentAsync(taskId, employeeId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }

    }
}