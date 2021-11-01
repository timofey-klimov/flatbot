using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.UpdateBuildingYear
{
    public class UpdateBuildingYearHandler : IRequestHandler<UpdateBuildingYearRequest>
    {
        private readonly IDbContext _dbContext;
        public UpdateBuildingYearHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateBuildingYearRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId, cancellationToken);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UserContext.UpdateFlatBuildYear(request.BuildingYear);

            await _dbContext.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}
