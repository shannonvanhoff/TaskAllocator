using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocator.Application.DTOs
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }
}
