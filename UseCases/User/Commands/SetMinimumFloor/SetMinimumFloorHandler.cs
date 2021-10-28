using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.SetMinimumFloor
{
    public class SetMinimumFloorHandler : IRequestHandler<SetMinimumFloorRequest>
    {
        private IDbContext _dbContext;
        public SetMinimumFloorHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SetMinimumFloorRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UserContext.ChangeMinimumFloor(request.MinimumFloor);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
