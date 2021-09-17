using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Exceptions;

namespace UseCases.User.Commands.SetMaximumPrice
{
    public class SetFlatMaximumPriceHandler : IRequestHandler<SetFlatMaximumPriceRequest>
    {
        private IDbContext _dbContext;
        public SetFlatMaximumPriceHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SetFlatMaximumPriceRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .Include(x => x.UserContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException("No such user");

            user.UserContext.MaximumPrice = request.Price;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
