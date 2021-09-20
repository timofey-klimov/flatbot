using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Exceptions;

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
                throw new UserIsNullException("No such user");

            user.EnableNotifications();

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
