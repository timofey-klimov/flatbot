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

        private NotificationContext() { }

        public NotificationContext(NotificationType notificationType, bool isActive)
        {
            NotificationType = notificationType;
            IsActive = isActive;
        }

        public void ChangeNotificationType(NotificationType notificationType)
        {
            NotificationType = notificationType;
            NextNotify = null;
        }

        public void Enable()
        {
            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
        }

        public void SetNextNotifyDate(int periodInHours)
        {
            if (NextNotify == null)
                NextNotify = DateTime.Now.AddHours(periodInHours);
            else
            {
                NextNotify = NextNotify.Value.AddHours(periodInHours);
            }
        }

        public void CreateLastNotifyDateNow()
        {
            LastNotify = DateTime.Now;
        }
    }
}
