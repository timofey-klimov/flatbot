using Entities.Enums;
using Entities.Models.Exceptions;
using Entities.Models.UserAgregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models
{
    public class UserContext : Entity<int>
    {
        public int UserId { get; private set; }

        public decimal MinimumPrice { get; private set; }

        public decimal MaximumPrice { get; private set; }

        public int TimeToMetro { get; private set; }

        public int MinimumFloor { get; private set; }

        public int MinimumFlatArea { get; private set; }

        public int MinimumBuildingYear { get; private set; }

        public ICollection<District> Districts { get; private set; }

        private List<PostedNotification> _postedNotifications;
        public IReadOnlyCollection<PostedNotification> PostedNotifications => _postedNotifications.AsReadOnly();

        private List<UserRoomCount> _roomCounts;
        public IReadOnlyCollection<UserRoomCount> UserRoomCounts => _roomCounts.AsReadOnly();

        public UserState State { get; private set; }

        private UserContext()
        {
            
        }
        public UserContext(decimal maximumPrice, 
            decimal minimumPrice, 
            int timeToMetro, 
            int minimumFloor,
            int minimumFlatArea,
            int minimumBuildingYear)
        {
            MaximumPrice = maximumPrice;
            MinimumPrice = minimumPrice;
            TimeToMetro = timeToMetro;
            MinimumFloor = minimumFloor;
            MinimumFlatArea = minimumFlatArea;
            MinimumBuildingYear = minimumBuildingYear;
            State = new UserState(UserStates.MainMenu);
            _roomCounts = new List<UserRoomCount>() { 1 };
            _postedNotifications = new List<PostedNotification>();
        }

        public void UpdateMaximumPrice(decimal price)
        {
            MaximumPrice = price;
        }

        public void UpdateMinimumPrice(decimal price)
        {
            MinimumPrice = price;
        }

        public void UpdateTimeToMetro(int time)
        {
            TimeToMetro = time;
        }

        public void UpdateMinimumFloor(int number)
        {
            MinimumFloor = number;
        }

        public void UpdateNotifications(IEnumerable<long> cianIds)
        {
            var items = cianIds.Select(x => new PostedNotification(x));
            _postedNotifications.AddRange(items);
        }
        public void UpdateState(UserStates state)
        {
            State.ChangeState(state);
        }

        public void RemoveDistrict(District district)
        {
            if (Districts == null)
                throw new DomainNullRefException(Districts);
            if (district == null)
                throw new ArgumentNullException(nameof(district));

            Districts.Remove(district);
        }

        public void AddDistrict(District district)
        {
            if (Districts == null)
                throw new DomainNullRefException(Districts);
            if (district == null)
                throw new ArgumentNullException(nameof(district));

            if (!Districts.Contains(district))
                Districts.Add(district);
        }

        public void AddRoomsCount(int roomsCount)
        {
            if (_roomCounts == null)
                throw new DomainNullRefException(_roomCounts);

            var item = _roomCounts.FirstOrDefault(x => x.RoomCount == roomsCount);

            if (item == null)
                _roomCounts.Add(roomsCount);
        }

        public void RemoveRoomsCount(int roomsCount)
        {
            if (_roomCounts == null)
                throw new DomainNullRefException(_roomCounts);

            var item = _roomCounts.FirstOrDefault(x => x.RoomCount == roomsCount);

            if (item != null)
                _roomCounts.Remove(item);
        }

        public void UpdateFlatArea(int flatArea)
        {
            if (flatArea <= 0)
                throw new DomainNullRefException(flatArea);

            MinimumFlatArea = flatArea;
        }

        public void UpdateFlatBuildYear(int flatBuildYear)
        {
            if (flatBuildYear <= 0)
                throw new DomainNullRefException(flatBuildYear);

            MinimumBuildingYear = flatBuildYear;
        }
    } 
}
