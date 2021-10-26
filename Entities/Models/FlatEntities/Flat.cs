using Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models.FlatEntities
{
    public class Flat : AgregateRoot<int>
    {
        public long CianId { get; private set; }

        public DateTime CreateDate { get; private set; }

        public double? TotalArea { get; private set; }

        public double? CellingHeight { get; private set; }

        public int FloorsCount { get; private set; }

        public int CurrentFloor { get; private set; }

        public int RoomsCount { get; private set; }

        public string LeaseTermType { get; private set; }

        public bool? HasFurniture { get; private set; }

        public string Description { get; private set; }

        public string CianUrl { get; private set; }

        public BuildingInfo BuildingInfo { get; private set; }

        public FlatGeo FlatGeo { get; private set; }

        public Address Address { get; private set; }

        public ParkingInfo ParkingInfo { get; private set; }

        public PriceInfo PriceInfo { get; private set; }

        private List<RailwayInfo> _railwayInfos;
        public IReadOnlyCollection<RailwayInfo> RailwayInfos => _railwayInfos.AsReadOnly();


        private List<UndergroundInfo> _undergroundInfos;
        public IReadOnlyCollection<UndergroundInfo> UndergroundInfos => _undergroundInfos.AsReadOnly();


        private List<PhotoInfo> _photoInfos;
        public IReadOnlyCollection<PhotoInfo> PhotoInfos => _photoInfos.AsReadOnly();


        private List<Phone> _phones;
        public IReadOnlyCollection<Phone> Phones => _phones.AsReadOnly();

        private Flat() 
        {

        }

        public Flat(
            long cianId,
            string cianUrl
            )
        {
            CianId = cianId;
            CianUrl = cianUrl;
            _railwayInfos = new List<RailwayInfo>();
            _undergroundInfos = new List<UndergroundInfo>();
            _photoInfos = new List<PhotoInfo>();
            _phones = new List<Phone>();
            CreateDate = DateTime.Now;
        }

        public Flat CreateBasicParameters(
            double? flatArea,
            double? cellingHeight,
            int floorsCount,
            int currentFloor,
            int roomsCount,
            bool? hasFurniture,
            string description,
            string leaseTermType)
        {
            TotalArea = flatArea;
            CellingHeight = cellingHeight;
            FloorsCount = floorsCount;
            CurrentFloor = currentFloor;
            RoomsCount = roomsCount;
            HasFurniture = hasFurniture;
            Description = description;
            LeaseTermType = leaseTermType;
            return this;
        }


        public Flat UpdateBuildingInfo(int? buildingYear, string type)
        {
            BuildingInfo = new BuildingInfo(buildingYear, type);

            return this;
        }

        public Flat UpdateFlatGeo(double latitude, double longitude)
        {
            FlatGeo = new FlatGeo(longitude, latitude);

            return this;
        }

        public Flat UpdateAddress(string city, District okrug, string raion, string street, string house)
        {
            Address = new Address(city, okrug, raion, street, house);
            return this;
        }

        public Flat UpdateParkingInfo(string parkingType, bool? isFree, int? priceMonthly)
        {
            ParkingInfo = new ParkingInfo(parkingType, isFree, priceMonthly);

            return this;
        }

        public Flat UpdateRailWayyInfo(string name, int time, string type)
        {
            var railwayInfo = _railwayInfos.FirstOrDefault(x => x.Type == type);

            if (railwayInfo == null)
            {
                _railwayInfos.Add(new RailwayInfo(name, time, type));
            }

            if (railwayInfo != null && railwayInfo.Time > time)
            {
                _railwayInfos.Remove(railwayInfo);
                _railwayInfos.Add(new RailwayInfo(name, time, type));
            }

            return this;
        }

        public Flat UpdateUndergroundsInfo(string name, int time, string type)
        {
            var undergroundInfo = _undergroundInfos.FirstOrDefault(x => x.Type == type);

            if (undergroundInfo == null)
            {
                _undergroundInfos.Add(new UndergroundInfo(name, time, type));
                return this;
            }

            if (undergroundInfo != null && undergroundInfo.Time > time)
            {
                _undergroundInfos.Remove(undergroundInfo);
                _undergroundInfos.Add(new UndergroundInfo(name, time, type));

            }

            return this;
        }

        public Flat UpdateFlatsPhotos(string url)
        {
            _photoInfos.Add(new PhotoInfo(url));
            return this;
        }

        public Flat UpdatePhones(string phone)
        {
            _phones.Add(new Phone(phone));

            return this;
        }

        public Flat UpdatePriceInfo(int price, int? deposit, int? agentFee)
        {
            PriceInfo = new PriceInfo(price, deposit, agentFee);

            return this;
        }
    }
}
