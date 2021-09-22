using UseCases.User.Base;

namespace UseCases.Notifications.Queries.GetTelegramNotification
{
    public class GetTelegramNotificationRequest : BaseUserRequest<string>
    {
        public GetTelegramNotificationRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
