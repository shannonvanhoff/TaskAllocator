using Microsoft.EntityFrameworkCore;
using TaskAllocator.Application.Services.Interfaces;
using TaskAllocator.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore; 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TaskAllocator.Infrastructure")));

builder.Services.AddScoped<ITaskService, TaskAllocator.Application.Services.Implementations.TaskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.  

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
