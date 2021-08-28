using Entities.Enums;
using Entities.Models.ValueObjects;

namespace Entities.Models
{
    public class Flat : Entity<int>
    {
        public long CianId { get; private set; }

        public FlatType FlatType { get; private set; }

        public int RoomCount { get; private set; }

        public Metro Metro { get; private set; }

        public Address Address { get; private set; }

        public double RoomArea { get; private set; }

        public int CurrentFloor { get; private set; }

        public int LastFloor { get; private set; }

        public Price Price { get; private set; }

        public string Phone { get; private set; }

        public string Description { get; private set; }
        /// <summary>
        /// Название Жк
        /// </summary>
        public string ResidentialComplexName { get; private set; }

        /// <summary>
        /// Высота потолков
        /// </summary>
        public double? CeilingHeight { get; private set; }

        /// <summary>
        /// Ссылка на Циан
        /// </summary>
        public string Reference { get; private set; }

        public bool ForSale { get; private set; }

        private Flat() { }

        public Flat(
            long cianId,
            FlatType flatType,
            int roomCount,
            Metro metro,
            Address address,
            double roomArea,
            int currentFloor,
            int lastFloor,
            Price price,
            string phone,
            string description,
            string complexName,
            double? cellingHeight,
            bool forSale)
        {
            CianId = cianId;
            FlatType = flatType;
            RoomCount = roomCount;
            Metro = metro;
            Address = address;
            RoomArea = roomArea;
            CurrentFloor = currentFloor;
            LastFloor = lastFloor;
            Price = price;
            Phone = phone;
            Description = description;
            ResidentialComplexName = complexName;
            CeilingHeight = cellingHeight;
            ForSale = forSale;
        }
    }
}
