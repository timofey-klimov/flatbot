using Entities.Models;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Telegram;
using Infrastructure.Interfaces.Telegram.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Notifications.Queries.GetTelegramObjectsNotification
{
    public class GetTelegramObjectNotificationHandler : IRequestHandler<GetTelegramObjectNotificationRequest, ICollection<NotificationDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ITelegramNotificationCreator _tgNotifyCreator;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _flatCountManager;

        public GetTelegramObjectNotificationHandler(
            IDbContext dbContext,
            ITelegramNotificationCreator telegramNotification,
            IFilterFlatService filterFlatService,
            IFlatCountInMessageManager flatCountManager
            )
        {
            _dbContext = dbContext;
            _tgNotifyCreator = telegramNotification;
            _filter = filterFlatService;
            _flatCountManager = flatCountManager;
        }

        public async Task<ICollection<NotificationDto>> Handle(GetTelegramObjectNotificationRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                 .Users
                 .Include(x => x.UserContext)
                 .ThenInclude(x => x.Disctricts)
                 .Include(x => x.NotificationContext)
                 .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _flatCountManager.FlatCount, cancellationToken);

            var notifications = await _tgNotifyCreator.CreateObjectsAsync(flats);

            user.NotificationContext.CreateLastNotifyDateNow();
            user.UserContext.AddNotifications(flats.Select(x => x.CianId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return notifications;
        }
    }
}
