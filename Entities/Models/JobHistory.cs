using Entities.Enums;
using System;

namespace Entities.Models
{
    public class JobHistory : Entity<int>
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public JobStatus Status { get; set; }

        public string Message { get; set; }

        public DateTime NextFireAt { get; set; }
    }
}
