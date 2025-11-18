using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Dtos;
using TaskApi.Models;
using TaskApi.Repositories;
using System.Linq;
using TaskApi.Dtos.Employee_Dtos;

namespace TaskApi.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var entities = await _employeeRepository.GetAllAsync();

            return entities.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
            });
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id);

            if (entity == null) throw new KeyNotFoundException("Employee not found");

            return new EmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name
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

    }
}
