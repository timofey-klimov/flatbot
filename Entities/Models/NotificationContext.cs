using Entities.Enums;
using System;

namespace Entities.Models
{
    public class NotificationContext : Entity<int>
    {
        public int UserId { get; set; }

        public NotificationType NotificationType { get; private set; }

        public DateTime? LastNotify { get; private set; }

        public DateTime? NextNotify { get; private set; }

        public bool IsActive { get; private set; }



        public NotificationContext(NotificationType notificationType, bool isActive)
        {
            NotificationType = notificationType;
            IsActive = isActive;
        }

        public void ChangeNotificationType(NotificationType notificationType)
        {
            NotificationType = notificationType;
        }

        public void Enable()
        {
            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
        }

        public void ChangeLastNotifyDate(DateTime dateTime)
        {
            LastNotify = dateTime;
        }
    }
}
