using System;

namespace Entities.Models
{
    public class TimedJobManager : SheduleJobManager
    {
        public DateTime SheduleRunTime { get; set; }
    }
}
