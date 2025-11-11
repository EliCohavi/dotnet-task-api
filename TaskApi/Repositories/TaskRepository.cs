using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _db.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _db.TaskItems.FindAsync(id);
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

        public async Task<bool> DeleteAsync(int id)
        {
            var taskItem = await _db.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return false;
            }

            _db.TaskItems.Remove(taskItem);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}