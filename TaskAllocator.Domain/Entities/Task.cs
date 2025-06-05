using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocator.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? AssignedTo { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? AssignedUser { get; set; }
        
    }
}
