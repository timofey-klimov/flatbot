using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.SetFlatMinumPrice
{
    public class SetFlatMinimumPriceRequestHandler : IRequestHandler<SetFlatMinimumPriceRequest>
    {
        private IDbContext _dbContext;
        public SetFlatMinimumPriceRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SetFlatMinimumPriceRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UserContext.ChangeMinimumPrice(request.MinimumPrice);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
