using System;

namespace Entities.Models
{
    public class Notification : Entity<int>
    {
        public int UserId { get; set; }

        public DateTime NotifyDate { get; set; }

        public int NotifyCount { get; set; }
    }
}
