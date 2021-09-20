using UseCases.Notifications.Dto;
using UseCases.User.Base;

namespace UseCases.Notifications.Commands.SelectNotificationType
{
    public class SelectNotificationTypeRequest : BaseUserRequest
    {
        public NotificationTypeDto NotificationType { get; }
        public SelectNotificationTypeRequest(long chatId, NotificationTypeDto notificationType)
            : base(chatId)
        {
            NotificationType = notificationType;
        }
    }
}
