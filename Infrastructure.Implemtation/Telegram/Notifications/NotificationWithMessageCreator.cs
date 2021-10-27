﻿using Entities.Models;
using Entities.Models.FlatEntities;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Telegram.Base;
using Infrastructure.Interfaces.Telegram.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Telegram.NotificationCreators
{
    public class NotificationWithMessageCreator : BaseNotificationCreator, INotificationCreator
    {
        private readonly IDbContext _dbContext;
        private readonly ILoggerService _logger;

        public NotificationWithMessageCreator(
            IDbContext dbContext, 
            ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _logger = loggerService;
        }

        public async Task<ICollection<NotificationDto>> CreateAsync(ICollection<Flat> flats)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var items = new List<NotificationDto>(flats.Count);

            foreach (var flat in flats)
            {
                //var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.CiandId == flat.CianId);

                //if (image == null || image.Data == null)
                //{
                //    items.Add(new NotificationDto() { HasImage = false, Message = CreateNotificationMessage(flat) });
                //}
                //else
                //{
                //    items.Add(new NotificationDto() { HasImage = true, Message = CreateNotificationMessage(flat), Image = image.Data });
                //}
            }

            stopWatch.Stop();
            var time = stopWatch.Elapsed.TotalSeconds;

            _logger.Info(this.GetType(), $"Время выполнения {nameof(CreateAsync)}: {time}");

            return items;
        }

        public Task<NotificationDto> CreateAsync(Flat flat)
        {
            throw new System.NotImplementedException();
        }
    }
}
