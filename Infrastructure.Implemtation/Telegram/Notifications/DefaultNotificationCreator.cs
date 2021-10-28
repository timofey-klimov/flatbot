using Entities.Models;
using Entities.Models.FlatEntities;
using Infrastructure.Interfaces.Telegram.Base;
using Infrastructure.Interfaces.Telegram.Dto;
using Infrastructure.Interfaces.Telegram.HostManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Telegram.NotificationCreators
{
    public class DefaultNotificationCreator : BaseNotificationCreator, INotificationCreator
    {
        public DefaultNotificationCreator(ITelegramClientHostManager hostManager) 
            : base(hostManager)
        {
        }

        public async Task<ICollection<NotificationDto>> CreateAsync(ICollection<Flat> flats, long chatId)
        {
            var items = new List<NotificationDto>(flats.Count);

            foreach (var flat in flats)
            {
                var notification = new NotificationDto
                {
                    HasImage = false,
                    Message = CreateNotificationMessage(flat, chatId)
                };

                items.Add(notification);
            }

            return items;
        }

        public async Task<NotificationDto> CreateAsync(Flat flat, long chatId)
        {
            return new NotificationDto() { Message = CreateNotificationMessage(flat, chatId) };
        }
    }
}
