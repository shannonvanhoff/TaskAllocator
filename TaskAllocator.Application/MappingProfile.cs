using AutoMapper;
using TaskAllocator.Application.DTOs;
using TaskAllocator.Domain.Entities;

namespace TaskAllocator.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Task mappings
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<CreateTaskRequest, TaskEntity>();
            CreateMap<UpdateTaskRequest, TaskEntity>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}