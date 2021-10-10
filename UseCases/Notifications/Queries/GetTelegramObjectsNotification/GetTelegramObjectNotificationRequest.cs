using Infrastructure.Interfaces.Telegram.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.Notifications.Queries.GetTelegramObjectsNotification
{
    public class GetTelegramObjectNotificationRequest : BaseUserRequest<ICollection<NotificationDto>>
    {
        public GetTelegramObjectNotificationRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
