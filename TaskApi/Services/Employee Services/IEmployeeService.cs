using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Dtos.Employee_Dtos;
using TaskApi.Models;


namespace TaskApi.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto dto);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto);
        Task<EmployeeDto> PartialUpdateEmployeeAsync(int id, EmployeePatchDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task AssignAsync(int employeeId, int taskId);
        Task RemoveAssignmentAsync(int employeeId, int taskId);
        Task<List<EmployeeDto>> GetEmployeesWithOverdueTasksAsync();
    }
}
