using Entities.Enums;
using Entities.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models
{
    public class UserContext : JsonPropertyEntity
    {
        public int UserId { get; private set; }

        public RoomCountContext? RoomCountContext { get; private set; }

        public decimal MinimumPrice { get; private set; }

        public decimal MaximumPrice { get; private set; }

        public int TimeToMetro { get; private set; }

        public int MinimumFloor { get; private set; }

        public string PostedNotifications { get; private set; }

        public ICollection<District> Disctricts { get; private set; }

        public UserState State { get; private set; }

        /// <summary>
        /// Не использовать методы List для изменения коллекции.
        /// Использовать внутренние методы класса NotificationContext
        /// </summary>
        public Lazy<List<long>> NotificationsList { get; private set; }

        private UserContext()
        {
            NotificationsList = new Lazy<List<long>>(() => JsonConvert.DeserializeObject<List<long>>(PostedNotifications));
        }
        public UserContext(RoomCountContext context, decimal maximumPrice, decimal minimumPrice, int timeToMetro, int minimumFloor)
        {
            RoomCountContext = context;
            MaximumPrice = maximumPrice;
            MinimumPrice = minimumPrice;
            TimeToMetro = timeToMetro;
            MinimumFloor = minimumFloor;
            PostedNotifications = JsonConvert.SerializeObject(new List<long>());
            State = new UserState(UserStates.MainMenu);
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
            if (NotificationsList.Value == null)
                throw new ArgumentNullException(nameof(NotificationsList));

            NotificationsList.Value.AddRange(cianIds);
        }
        public void ChangeState(UserStates state)
        {
            State.ChangeState(state);
        }

        public override void UpdateJsonEntity()
        {
            if (NotificationsList != null)
                PostedNotifications = JsonConvert.SerializeObject(NotificationsList.Value);
        }
    } 
}
