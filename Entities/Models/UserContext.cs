using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Models
{
    public class UserContext : Entity<int>
    {
        public int UserId { get; set; }

        public RoomCountContext? RoomCountContext { get; set; }

        public ICollection<Metro> Metros { get; set; }

        public decimal MinimumPrice { get; set; }

        public decimal MaximumPrice { get; set; }

        public int TimeToMetro { get; set; }

        public UserContext()
        {
            RoomCountContext = Enums.RoomCountContext.One;
            MaximumPrice = 40000;
            MinimumPrice = 15000;
            TimeToMetro = 20;
        }
    } 
}
