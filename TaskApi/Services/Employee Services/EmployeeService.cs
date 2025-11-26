using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Dtos;
using TaskApi.Models;
using TaskApi.Repositories;
using System.Linq;
using TaskApi.Dtos.Employee_Dtos;
using TaskApi.Dtos.Task_Dtos;

namespace TaskApi.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;


        public EmployeeService(IEmployeeRepository employeeRepository, ITaskRepository taskRepository)
        {
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var entities = await _employeeRepository.GetAllAsync();

            return entities.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Tasks = e.Tasks?.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title
                }).ToList()
            });
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id);

            if (entity == null) throw new KeyNotFoundException("Employee not found");

            return new EmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Tasks = entity.Tasks?.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title
                }).ToList()
            };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto dto)
        {
            var entity = new Employee
            {
                Name = dto.Name
            };

            var createdEntity = await _employeeRepository.AddAsync(entity);

            return new EmployeeDto
            {
                Id = createdEntity.Id,
                Name = createdEntity.Name
            };

        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto)
        {
            var existing = await _employeeRepository.GetByIdAsync(id); // Retrieve entity from repository
            if (existing == null) throw new KeyNotFoundException("Employee not found");
            // Field Updates
            existing.Name = dto.Name;

            var updated = await _employeeRepository.UpdateAsync(existing); // Save changes to repository    

            var updatedDto = new EmployeeDto // Maps back to DTO
            {
                Id = updated.Id,
                Name = updated.Name
            };

            return updatedDto;
        }

        public async Task<EmployeeDto> PartialUpdateEmployeeAsync(int id, EmployeePatchDto dto)
        {
            var existing = await _employeeRepository.GetByIdAsync(id); // Retrieve entity from repository
            if (existing == null) throw new KeyNotFoundException("Employee not found");
            // Partial Field Updates
            if (dto.Name != null) existing.Name = dto.Name;

            var updated = await _employeeRepository.UpdateAsync(existing); // Save changes to repository

            var updatedDto = new EmployeeDto // Maps back to DTO
            {
                Id = updated.Id,
                Name = updated.Name
            };

            return updatedDto;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var existing = await _employeeRepository.GetByIdAsync(id); // Retrieve entity from repository
            if (existing == null) throw new KeyNotFoundException("Employee not found");

            await _employeeRepository.DeleteAsync(id); // Delete entity from repository
            return true;

        }

        public async Task AssignAsync(int employeeId, int taskId)
        {
            // Optional: verify existence before calling repo
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            await _employeeRepository.AssignTaskAsync(employeeId, taskId);
        }

        public async Task RemoveAssignmentAsync(int employeeId, int taskId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            await _employeeRepository.RemoveTaskAssignmentAsync(employeeId, taskId);
        }

        public async Task<List<EmployeeDto>> GetEmployeesWithOverdueTasksAsync()
        {

            List<Employee> entities = await _employeeRepository.GetEmployeesWithOverdueTasksAsync();

            return entities.Select(e => new EmployeeDto
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
            }).ToList();

        }
    }
}
