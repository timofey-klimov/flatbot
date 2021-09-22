using Entities.Enums;
using System;

namespace Entities.Models
{
    public class User : Entity<int>
    {
        public long ChatId { get; private set; }

        public string UserName { get; private set; }

        public UserContext UserContext { get; private set; }

        public NotificationContext NotificationContext { get; private set; }

        private User() { }

        public User (long chatId, string userName)
        {
            ChatId = chatId;
            UserName = userName;
            UserContext = new UserContext(RoomCountContext.One, 40000, 25000, 10);
            NotificationContext = new NotificationContext(NotificationType.Default, true);
        }

        public void ChangeMinimumPrice(decimal price)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.ChangeMinimumPrice(price);
        }

        public void ChangeMaximumPrice(decimal price)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.ChangeMaximumPrice(price);
        }
        public void DisableNotifications()
        {
            if (NotificationContext == null)
                throw new ArgumentNullException(nameof(NotificationContext));

            NotificationContext.Disable();
        }

        public void EnableNotifications()
        {
            if (NotificationContext == null)
                throw new ArgumentNullException(nameof(NotificationContext));

            NotificationContext.Enable();    
        }

        public void ChangeNotificationType(NotificationType notificationType)
        {
            if (NotificationContext == null)
                throw new ArgumentNullException(nameof(NotificationContext));

            NotificationContext.ChangeNotificationType(notificationType);
        }

        public void ChangTimeToMetro(int time)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.ChangeTimeToMetro(time);
        }
    }
}
