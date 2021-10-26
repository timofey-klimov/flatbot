using Entities.Models;
using Infrastructure.Implemtation.Telegram.Factory;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
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
        private readonly INotificationCreatorFactory _creatorFactory;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _flatCountManager;

        public GetTelegramObjectNotificationHandler(
            IDbContext dbContext,
            INotificationCreatorFactory creatorFactory,
            IFilterFlatService filterFlatService,
            IFlatCountInMessageManager flatCountManager
            )
        {
            _dbContext = dbContext;
            _creatorFactory = creatorFactory;
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

            var notificationCreator = _creatorFactory.Create(Infrastructure.Interfaces.Telegram.Model.NotificationCreationType.WithImage);

            var notifications = await notificationCreator.CreateAsync(flats);

            user.NotificationContext.CreateLastNotifyDateNow();
            user.UserContext.AddNotifications(flats.Select(x => x.CianId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return notifications;
        }
    }
}
