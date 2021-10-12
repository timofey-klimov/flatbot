using Entities.Enums;
using Entities.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

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

        public District District { get; set; }

        public int? Comission { get; set; }

        public decimal? Pledge { get; set; }

        public bool? MoreThanYear { get; set; }

        public decimal? Price { get; set; }

        public string CianReference { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string PdfReference { get; set; }


        public Flat()
        {
            CreateDate = DateTime.Now;
        }


        public override bool Equals(object obj)
        {
            if (obj is Flat flat)
                return this.CianId == flat.CianId;

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + CianId.GetHashCode();
            hash = hash * 23 + Price.GetHashCode();
            hash = hash * 23 + CreateDate.GetHashCode();
            return hash;
        }
    }
}
