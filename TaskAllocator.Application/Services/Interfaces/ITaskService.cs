using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAllocator.Application.DTOs;

namespace TaskAllocator.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(Guid id);
        Task<TaskDto> CreateTaskAsync(CreateTaskRequest request);
        Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<TaskDto> AssignTaskAsync(Guid taskId, Guid userId);
        Task<TaskDto> AutoAssignTaskAsync(Guid taskId);
    }

}
