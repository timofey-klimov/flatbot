using UseCases.User.Base;

namespace UseCases.Notifications.Queries.GetTelegramNotification
{
    public class GetTelegramMessagesNotificationRequest : BaseUserRequest<string>
    {
        public GetTelegramMessagesNotificationRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
