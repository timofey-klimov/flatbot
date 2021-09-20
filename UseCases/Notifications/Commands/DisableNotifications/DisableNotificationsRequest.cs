using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.Notifications.Commands.DisableNotifications
{
    public class DisableNotificationsRequest : BaseUserRequest
    {
        public DisableNotificationsRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
