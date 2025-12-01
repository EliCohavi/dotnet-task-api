using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TaskApi.Dtos.Task_Dtos;
using TaskApi.Dtos.Employee_Dtos;

namespace TaskApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _db.TaskItems
                .Include(t => t.Employees)
                .ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _db.TaskItems
                .Include(t => t.Employees) // Include employees when retrieving a task
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> AddAsync(TaskItem taskItem)
        {
            _db.TaskItems.Add(taskItem);
            await _db.SaveChangesAsync();
            return taskItem;
        }

        public async Task<TaskItem> UpdateAsync(TaskItem taskItem)
        {
            _db.TaskItems.Update(taskItem);
            await _db.SaveChangesAsync();
            return taskItem;
        }

        public async Task<TaskItem> PartialUpdateAsync(TaskItem taskItem)
        {
            _db.TaskItems.Update(taskItem);
            await _db.SaveChangesAsync();
            return taskItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var taskItem = await _db.TaskItems.FindAsync(id);
            if (taskItem == null) return false;

            _db.TaskItems.Remove(taskItem);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task AssignEmployeeAsync(int taskId, int employeeId)
        {
            var taskItem = await _db.TaskItems
                .Include(t => t.Employees)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (taskItem == null)
                throw new KeyNotFoundException("Task not found.");

            var employee = await _db.Employees.FindAsync(employeeId);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            // Only add if not already assigned
            if (!taskItem.Employees.Any(e => e.Id == employeeId))
                taskItem.Employees.Add(employee);

            await _db.SaveChangesAsync();
        }

        public async Task RemoveEmployeeAssignmentAsync(int taskId, int employeeId)
        {
            var taskItem = await _db.TaskItems
                .Include(t => t.Employees)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (taskItem == null)
                throw new KeyNotFoundException("Task not found");

            var employee = await _db.Employees.FindAsync(employeeId);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            if (employee.Tasks.Any(t => t.Id == taskId))
                employee.Tasks.Remove(taskItem);

            await _db.SaveChangesAsync();
        }

        public async Task<List<TaskItem>> GetOverdueTasksAsync()
        {
            var now = DateTime.UtcNow;
            // If you want date-only comparison: use now.Date and compare against t.DueDate.Value.Date
            var overdueTasks = await _db.TaskItems
                .Where(t => t.DueDate.HasValue && t.DueDate.Value < now && !t.Completed)
                .Include(t => t.Employees)
                .ToListAsync();
            return overdueTasks;
        }
    }
}