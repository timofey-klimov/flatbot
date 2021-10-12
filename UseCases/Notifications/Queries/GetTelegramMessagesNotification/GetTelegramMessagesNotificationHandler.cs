using Infrastructure.Implemtation.Telegram.Factory;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Telegram;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Notifications.Queries.GetTelegramNotification
{
    [Obsolete]
    public class GetTelegramMessagesNotificationHandler : IRequestHandler<GetTelegramMessagesNotificationRequest, string>
    {
        private readonly IDbContext _dbContext;
        private readonly INotificationCreatorFactory _creatorFactory;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _flatCountManager;

        public GetTelegramMessagesNotificationHandler(
            IDbContext dbContext,
            INotificationCreatorFactory creatorFactory,
            IFilterFlatService filter,
            IFlatCountInMessageManager flatCountManger)
        {
            _dbContext = dbContext;
            _creatorFactory = creatorFactory;
            _filter = filter;
            _flatCountManager = flatCountManger;
        }
        public async Task<string> Handle(GetTelegramMessagesNotificationRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _flatCountManager.FlatCount, cancellationToken);

            if (!flats.Any())
            {
                return "Объявлений по твоим фильтрам не найдено";
            }

            var notificationCreator = _creatorFactory.Create(Infrastructure.Interfaces.Telegram.Model.NotificationCreationType.Default);

            var messages = await notificationCreator.CreateAsync(flats);


            user.NotificationContext.CreateLastNotifyDateNow();
            user.UserContext.AddNotifications(flats.Select(x => x.CianId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return messages.FirstOrDefault()?.Message;
        }
    }
}
