using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Dtos;
using TaskApi.Dtos.Employee_Dtos;
using TaskApi.Dtos.Task_Dtos;
using TaskApi.Models;
using TaskApi.Services;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            var dto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Tasks = employee.Tasks?.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title
                }).ToList()
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeCreate(EmployeeCreateDto dto) // Takes Dto, Maps to Model
        {
            EmployeeDto createdEmployeeDto = await _employeeService.CreateEmployeeAsync(dto); // Create task by calling service. Returns Dto
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployeeDto.Id }, createdEmployeeDto);
        }

        //POST /employees/{employeeId}/tasks/{taskId}
        [HttpPost("{employeeId:int}/tasks/{taskId:int}")]
        public async Task<IActionResult> AssignTaskToEmployee(int employeeId, int taskId)
        {
            try
            {
                await _employeeService.AssignAsync(employeeId, taskId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE /employees/{employeeId}/tasks/{taskId}
        [HttpDelete("{employeeId:int}/tasks/{taskId:int}")]
        public async Task<IActionResult> RemoveTaskFromEmployee(int employeeId, int taskId)
        {
            try
            {
                await _employeeService.RemoveAssignmentAsync(employeeId, taskId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto dto)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartialUpdateEmployee(int id, EmployeePatchDto dto)
        {
            try
            {
                await _employeeService.PartialUpdateEmployeeAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("employees-with-overdue-tasks")]
        public async Task<IActionResult> GetEmployeesWithOverdueTasks()
        {
            List<EmployeeDto> employees = await _employeeService.GetEmployeesWithOverdueTasksAsync();
            var dtoList = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Tasks = e.Tasks?.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Completed = t.Completed,
                    DueDate = t.DueDate
                }).ToList()
            });
            return Ok(dtoList);

        }
    }
}
