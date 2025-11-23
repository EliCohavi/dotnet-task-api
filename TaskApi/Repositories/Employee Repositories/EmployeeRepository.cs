using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskApi.Repositories.Employee_Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _db.Employees
                .Include(e => e.Tasks) // Include tasks when retrieving an employee
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> PartialUpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return false;

            _db.Entry(employee).Collection(e => e.Tasks).Load(); // Load related tasks
            employee.Tasks?.Clear(); // Clear task assignments
            return true;
        }

        public async Task AssignTaskAsync(int employeeId, int taskId)
        {
            // Retrieve the employee and task from the database including their navigation collections
            var employee = await _db.Employees
                .Include(e => e.Tasks)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            var task = await _db.TaskItems.FindAsync(taskId);

            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            // Prevents duplicate assignments
            if (!employee.Tasks.Any(t => t.Id == taskId))
                employee.Tasks.Add(task);

            await _db.SaveChangesAsync();
        }

        public async Task RemoveTaskAssignmentAsync(int employeeId, int taskId)
        {
            var employee = await _db.Employees
                .Include(e => e.Tasks)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            _db.Employees.Any(e => e.Name!.Contains("Mike"));

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            var taskItem = await _db.TaskItems.FindAsync(taskId);

            if (taskItem == null)
                throw new KeyNotFoundException("Task not assigned to this employee.");

            if (taskItem.Employees.Any(e => e.Id == employeeId))
                taskItem.Employees.Remove(employee);

            await _db.SaveChangesAsync();
        }
    }
}
