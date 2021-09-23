using Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models
{
    public class UserContext : Entity<int>
    {
        public int UserId { get; private set; }

        public RoomCountContext? RoomCountContext { get; private set; }

        public decimal MinimumPrice { get; private set; }

        public decimal MaximumPrice { get; private set; }

        public int TimeToMetro { get; private set; }

        public string PostedNotifications { get; private set; }

        public ICollection<District> Disctricts { get; private set; }

        /// <summary>
        /// Не использовать методы List для изменения коллекции.
        /// Использовать внутренние методы класса NotificationContext
        /// </summary>
        public Lazy<List<long>> NotificationsList { get; private set; }

        private UserContext()
        {
            NotificationsList = new Lazy<List<long>>(() => JsonConvert.DeserializeObject<List<long>>(PostedNotifications));
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
        public void AddNotifications(IEnumerable<long> cianIds)
        {
            if (NotificationsList.Value == null)
                throw new ArgumentNullException(nameof(NotificationsList));

            NotificationsList.Value.AddRange(cianIds);

            UpdatePostedNotifications();
        }

        private void UpdatePostedNotifications()
        {
            PostedNotifications = JsonConvert.SerializeObject(NotificationsList.Value);
        }

    } 
}
