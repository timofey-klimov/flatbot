using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class ParkingInfo : Entity<int>
    {
        public int FlatId { get; private set; }

        public string ParkingType { get; private set; }

        public bool? IsFree { get; private set; }

        public int? PriceMonthly { get; private set; }

        private ParkingInfo() { }

        public ParkingInfo(
            string parkingType,
            bool? isFree,
            int? priceMonthly)
        {
            ParkingType = parkingType;
            IsFree = isFree;
            PriceMonthly = priceMonthly;
        }
    }
}
