using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using TaskApi.Services;
using System.Threading.Tasks;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var taskItem = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Completed = dto.Completed
            };

            var createdTask = await _taskService.CreateTaskAsync(taskItem);

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem taskItem)
        {
            try
            {
                await _taskService.UpdateTaskAsync(id, taskItem);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartialUpdateTask(int id, TaskItem taskItem)
        {
            try
            {
                await _taskService.PartialUpdateTaskAsync(id, taskItem);
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