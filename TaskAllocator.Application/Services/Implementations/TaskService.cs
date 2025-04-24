using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAllocator.Application.DTOs;
using TaskAllocator.Application.Services.Interfaces;

namespace TaskAllocator.Application.Services.Implementations
{
    public class TaskService : ITaskService
    {
        public Task<TaskDto> AssignTaskAsync(Guid taskId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> AutoAssignTaskAsync(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskDto>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
