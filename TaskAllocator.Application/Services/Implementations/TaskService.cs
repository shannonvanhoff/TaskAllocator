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
using TaskAllocator.Domain.Entities;

namespace TaskAllocator.Application.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

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

        public async Task<TaskDto> AutoAssignTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            var users = await _userRepository.GetAllAsync();

            if (task == null || !users.Any())
                throw new Exception("Task or Users not found");

            // Simple auto-assign: random
            var randomUser = users.OrderBy(u => Guid.NewGuid()).First();
            task.AssignedTo = randomUser.Id;

            await _taskRepository.UpdateAsync(task);

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
        {
            var task = _mapper.Map<TaskEntity>(request);
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;

            await _taskRepository.AddAsync(task);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return false;

            await _taskRepository.DeleteAsync(task);
            return true;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<TaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception (replace with a real logger in production)
                Console.WriteLine($"Error in GetAllTasksAsync: {ex.Message}");
                // Optionally, rethrow or return an empty list
                throw;
                // Or: return Enumerable.Empty<TaskDto>();
            }
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            _mapper.Map(request, task);
            await _taskRepository.UpdateAsync(task);

            return _mapper.Map<TaskDto>(task);
        }
    }
}
