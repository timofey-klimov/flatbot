using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.District.Exceptions;

namespace UseCases.District.Commands.RemoveUserDistrict
{
    public class RemoveUserDictrictHandler : IRequestHandler<RemoveUserDictrictRequest>
    {
        private readonly IDbContext _dbContext;

        public RemoveUserDictrictHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(RemoveUserDictrictRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Districts)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            var district = await _dbContext.Districts.FirstOrDefaultAsync(x => x.Name == request.DistrictName);

            if (district == null)
                throw new DistrictNotFoundException("No such district");

            user.UserContext.RemoveDistrict(district);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
