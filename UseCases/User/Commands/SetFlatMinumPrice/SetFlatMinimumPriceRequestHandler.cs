using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            user.UserContext.MinimumPrice = request.MinimumPrice;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
