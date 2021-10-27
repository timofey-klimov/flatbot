using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.District.Exceptions;

namespace UseCases.Distincts.Commands.UpdateUsersDistincts
{
    public class AddUserDistrictHandler : IRequestHandler<AddUserDistrictRequest>
    {
        private IDbContext _dbContext;

        public AddUserDistrictHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddUserDistrictRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Districts)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException(request.ChatId);

            var district = await _dbContext.Districts.FirstOrDefaultAsync(x => x.Name == request.DistrictName);

            if (district == null)
                throw new DistrictNotFoundException("No such district");

            user.UserContext.AddDistrict(district);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
