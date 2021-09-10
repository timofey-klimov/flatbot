using Entities.Enums;
using Entities.Models.ValueObjects;

namespace Entities.Models
{
    public class Flat : Entity<int>
    {
        public long CianId { get; private set; }

        public int RoomCount { get; private set; }

        public Metro Metro { get; private set; }

        public Address Address { get; private set; }

        public double RoomArea { get; private set; }

        public int CurrentFloor { get; private set; }

        public int LastFloor { get; private set; }

        public Price Price { get; private set; }

        public string Phone { get; private set; }

        public double? CeilingHeight { get; private set; }

        /// <summary>
        /// Ссылка на Циан
        /// </summary>
        public string Reference { get; private set; }

        private Flat() { }

        public Flat(
            long cianId,
            int roomCount,
            Metro metro,
            Address address,
            double roomArea,
            int currentFloor,
            int lastFloor,
            Price price,
            string phone,
            double? cellingHeight,
            string reference)
        {
            CianId = cianId;
            RoomCount = roomCount;
            Metro = metro;
            Address = address;
            RoomArea = roomArea;
            CurrentFloor = currentFloor;
            LastFloor = lastFloor;
            Price = price;
            Phone = phone;
            CeilingHeight = cellingHeight;
            Reference = reference;
        }
    }
}
