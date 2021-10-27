using Entities.Enums;
using Entities.Models.Base;
using Entities.Models.Exceptions;
using Entities.Models.UserAgregate;
using Newtonsoft.Json;
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

        public ICollection<District> Districts { get; private set; }

        private List<PostedNotification> _postedNotifications;
        public IReadOnlyCollection<PostedNotification> PostedNotifications => _postedNotifications.AsReadOnly();

        private List<UserRoomCount> _roomCounts;
        public IReadOnlyCollection<UserRoomCount> UserRoomCounts => _roomCounts.AsReadOnly();

        public UserState State { get; private set; }

        private UserContext()
        {
            
        }
        public UserContext(decimal maximumPrice, decimal minimumPrice, int timeToMetro, int minimumFloor)
        {
            MaximumPrice = maximumPrice;
            MinimumPrice = minimumPrice;
            TimeToMetro = timeToMetro;
            MinimumFloor = minimumFloor;
            State = new UserState(UserStates.MainMenu);

            _roomCounts = new List<UserRoomCount>() { 1, 2, 3 };
            
            _postedNotifications = new List<PostedNotification>();
        }

        public void ChangeMaximumPrice(decimal price)
        {
            MaximumPrice = price;
        }

        public void ChangeMinimumPrice(decimal price)
        {
            MinimumPrice = price;
        }

        public void ChangeTimeToMetro(int time)
        {
            TimeToMetro = time;
        }

        public void ChangeMinimumFloor(int number)
        {
            MinimumFloor = number;
        }

        public void AddNotifications(IEnumerable<long> cianIds)
        {
            var items = cianIds.Select(x => new PostedNotification(x));
            _postedNotifications.AddRange(items);
        }
        public void ChangeState(UserStates state)
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
    } 
}
