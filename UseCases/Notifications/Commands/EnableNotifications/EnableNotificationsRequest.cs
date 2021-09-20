using UseCases.User.Base;

namespace UseCases.Notifications.Commands.EnableNotifications
{
    public class EnableNotificationsRequest : BaseUserRequest
    {
        public EnableNotificationsRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
