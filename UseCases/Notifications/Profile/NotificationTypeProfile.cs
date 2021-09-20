using Entities.Enums;
using UseCases.Notifications.Dto;

namespace UseCases.Notifications.Profile
{
    public class NotificationTypeProfile : AutoMapper.Profile
    {
        public NotificationTypeProfile()
        {
            CreateMap<NotificationTypeDto, NotificationType>();
        }
    }
}
