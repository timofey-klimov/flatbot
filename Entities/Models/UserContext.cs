using Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class UserContext : Entity<int>
    {
        public int UserId { get; private set; }

        public RoomCountContext? RoomCountContext { get; private set; }

        public ICollection<Metro> Metros { get; private set; }

        public decimal MinimumPrice { get; private set; }

        public decimal MaximumPrice { get; private set; }

        public int TimeToMetro { get; private set; }

        public string PostedNotifications { get; private set; }

        public Lazy<List<long>> Notifications { get; private set; }

        private UserContext()
        {
            Notifications = new Lazy<List<long>>(() => JsonConvert.DeserializeObject<List<long>>(PostedNotifications));
        }
        public UserContext(RoomCountContext context, decimal maximumPrice, decimal minimumPrice, int timeToMetro)
        {
            RoomCountContext = context;
            MaximumPrice = maximumPrice;
            MinimumPrice = minimumPrice;
            TimeToMetro = timeToMetro;
            PostedNotifications = JsonConvert.SerializeObject(new List<long>());
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

        public void UpdatePostedNotifications()
        {
            PostedNotifications = JsonConvert.SerializeObject(Notifications.Value);
        }
    } 
}
