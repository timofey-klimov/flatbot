using Entities.Enums;
using System;

namespace Entities.Models
{
    public class Flat : Entity<int>
    {
        public long CianId { get; set; }

        public int RoomCount { get; set; }

        public double FlatArea { get; set; }

        public int CurrentFloor { get; set; }

        public int MaxFloor { get; set; }

        public string Metro { get; set; }

        public int? TimeToMetro { get; set; }

        public WayToGo? WayToGo { get; set; }

        public string Address { get; set; }

        public int? Comission { get; set; }

        public decimal? Pledge { get; set; }

        public bool? MoreThanYear { get; set; }

        public decimal? Price { get; set; }

        public string CianReference { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }


        public Flat()
        {
            CreateDate = DateTime.Now;
        }
    }
}
