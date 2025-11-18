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
            return await _db.Employees.FindAsync(id);
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

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task AssignTaskAsync(int employeeId, int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTaskAssignmentAsync(int employeeId, int taskId)
        {
            throw new NotImplementedException();
        }
    }
}
