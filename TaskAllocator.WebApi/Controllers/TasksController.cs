using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAllocator.Application.DTOs;
using TaskAllocator.Application.Services.Interfaces;

namespace TaskAllocator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _taskService.GetAllTasksAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var result = await _taskService.CreateTaskAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var result = await _taskService.UpdateTaskAsync(id, request);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("{taskId}/assign/{userId}")]
        public async Task<IActionResult> Assign(Guid taskId, Guid userId)
        {
            var result = await _taskService.AssignTaskAsync(taskId, userId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("{taskId}/auto-assign")]
        public async Task<IActionResult> AutoAssign(Guid taskId)
        {
            var result = await _taskService.AutoAssignTaskAsync(taskId);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
