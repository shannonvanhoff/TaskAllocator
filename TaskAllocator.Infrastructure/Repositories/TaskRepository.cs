using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAllocator.Domain.Entities;
using TaskAllocator.Infrastructure.Data;
using TaskAllocator.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TaskAllocator.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Tasks.Include(t => t.AssignedUser).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (replace with a real logger in production)
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                // Optionally, rethrow or return an empty list
                throw;
                // Or: return Enumerable.Empty<TaskEntity>();
            }
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Tasks
                    .Include(t => t.AssignedUser)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                // Log the exception (replace with a real logger in production)
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                // Return null to indicate not found or error
                return null;
            }
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskEntity task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
