using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAllocator.Domain.Entities;
//using Task = TaskAllocator.Domain.Entities.TaskEntity;
using TaskStatus = TaskAllocator.Domain.Entities.TaskStatus;

namespace TaskAllocator.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use static GUIDs for consistent FK references
            var user1Id = Guid.Parse("73acd2f4-6684-4e85-b686-af12d61db68f");
            var user2Id = Guid.Parse("38eb34d8-d240-4201-ad0f-359e5b819f09");
            var task1Id = Guid.Parse("7c900723-66f2-4ad2-9fb7-271387e496eb");
            var task2Id = Guid.Parse("54deab85-df2c-481d-a6f7-eb0239e64aa3");

            // 👤 Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = user1Id,
                    FullName = "Alice Johnson",
                    Email = "alice@example.com"
                },
                new User
                {
                    Id = user2Id,
                    FullName = "Bob Smith",
                    Email = "bob@example.com"
                }
            );

            // ✅ Seed Tasks
            modelBuilder.Entity<Task>().HasData(
                new TaskEntity
                {
                    Id = task1Id,
                    Title = "Fix frontend bugs",
                    Description = "Resolve UI alignment issues",
                    Status = TaskStatus.Pending,
                    CreatedAt = new DateTime(2025, 4, 25, 9, 48, 58, DateTimeKind.Utc),
                    AssignedTo = user1Id
                },
                new TaskEntity
                {
                    Id = task2Id,
                    Title = "Set up database backup",
                    Description = "Configure automated MySQL backups",
                    Status = TaskStatus.Pending,
                    CreatedAt = new DateTime(2025, 4, 25, 9, 48, 58, DateTimeKind.Utc),
                    AssignedTo = user2Id
                }
            );
        }


    }
}
