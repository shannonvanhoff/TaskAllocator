using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskAllocator.Application.Interfaces.Repositories;
using TaskAllocator.Application.Mappings;
using TaskAllocator.Application.Services.Implementations;
using TaskAllocator.Application.Services.Interfaces;
using TaskAllocator.Infrastructure.Data;
using TaskAllocator.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskAllocator API", Version = "v1" });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TaskAllocator.Infrastructure")));

// Dependency Injection
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskAllocator API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
