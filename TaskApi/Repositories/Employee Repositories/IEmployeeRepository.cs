using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> PartialUpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task AssignTaskAsync(int employeeId, int taskId);
        Task RemoveTaskAssignmentAsync(int employeeId, int taskId);

    }
}
