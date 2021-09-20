using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Notifications.Dto;

namespace WepApp.Dto.Request
{
    public class SelectNotificationTypeDto
    {
        public long ChatId { get; set; }

        public NotificationTypeDto Type { get; set; }
    }
}
