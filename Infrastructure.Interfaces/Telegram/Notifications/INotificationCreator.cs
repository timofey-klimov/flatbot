using Entities.Models;
using Entities.Models.FlatEntities;
using Infrastructure.Interfaces.Telegram.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram.Base
{
    public interface INotificationCreator
    {
        Task<ICollection<NotificationDto>> CreateAsync(ICollection<Flat> flats);

        Task<NotificationDto> CreateAsync(Flat flat);
    }
}
