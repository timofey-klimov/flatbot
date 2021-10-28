using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.Notifications.Commands.EnableNotifications
{
    public class EnableNotificationsHandler : IRequestHandler<EnableNotificationsRequest>
    {
        private readonly IDbContext _dbContext;
        public EnableNotificationsHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EnableNotificationsRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.EnableNotifications();

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
