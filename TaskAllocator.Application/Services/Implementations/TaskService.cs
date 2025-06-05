using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskAllocator.Application.DTOs;
using TaskAllocator.Application.Interfaces.Repositories;
using TaskAllocator.Application.Services.Interfaces;
using AutoMapper;

namespace TaskAllocator.Application.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<TaskDto> AssignTaskAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (task == null || user == null)
                throw new KeyNotFoundException("Task or User not found");

            task.AssignedTo = user.Id;
            await _taskRepository.UpdateAsync(task);

            return _mapper.Map<TaskDto>(task);
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

        public Task<IEnumerable<TaskDto>> GetAllTasksAsync()
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
