using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.Notifications.Commands.DisableNotifications
{
    public class DisableNotificationsHandler : IRequestHandler<DisableNotificationsRequest>
    {
        private IDbContext _dbContext;

        public DisableNotificationsHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DisableNotificationsRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException(request.ChatId);

            user.DisableNotifications();

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
