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
            return await _context.Tasks.Include(t => t.AssignedUser).ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.Include(t => t.AssignedUser).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(TaskEntity task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(TaskEntity task)
        {
            _context.Tasks.Remove(task);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
