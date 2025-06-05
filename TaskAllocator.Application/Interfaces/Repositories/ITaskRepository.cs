using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAllocator.Domain.Entities;



namespace TaskAllocator.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task AddAsync(TaskEntity task);
        void Update(TaskEntity task);
        void Delete(TaskEntity task);
        Task SaveAsync();
    }
}
