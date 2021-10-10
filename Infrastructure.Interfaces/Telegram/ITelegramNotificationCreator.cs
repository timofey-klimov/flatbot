using Entities.Models;
using Infrastructure.Interfaces.Telegram.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram
{
    public interface ITelegramNotificationCreator
    {
        ICollection<string> CreateMessages(ICollection<Flat> flats, int elementsInMessage);

        string CreateMessage(Flat item);

        Task<ICollection<NotificationDto>> CreateObjectsAsync(ICollection<Flat> flats);
    }
}
