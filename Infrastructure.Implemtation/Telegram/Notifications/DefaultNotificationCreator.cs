﻿using Entities.Models;
using Infrastructure.Interfaces.Telegram.Base;
using Infrastructure.Interfaces.Telegram.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Telegram.NotificationCreators
{
    public class DefaultNotificationCreator : BaseNotificationCreator, INotificationCreator
    {
        public async Task<ICollection<NotificationDto>> CreateAsync(ICollection<Flat> flats)
        {
            var items = new List<NotificationDto>(flats.Count);

            foreach (var flat in flats)
            {
                var notification = new NotificationDto
                {
                    HasImage = false,
                    Message = CreateNotificationMessage(flat)
                };

                items.Add(notification);
            }

            return items;
        }
    }
}
