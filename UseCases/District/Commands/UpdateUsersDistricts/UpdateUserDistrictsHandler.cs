using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.District.Exceptions;
using UseCases.User.Exceptions;

namespace UseCases.Distincts.Commands.UpdateUsersDistincts
{
    public class UpdateUserDistrictsHandler : IRequestHandler<UpdateUsersDistrictsRequest>
    {
        private IDbContext _dbContext;

        public UpdateUserDistrictsHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Unit> Handle(UpdateUsersDistrictsRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException("No such user");

            var district = await _dbContext.Districts.FirstOrDefaultAsync(x => x.Name == request.DistrictName);

            if (district == null)
                throw new DistrictNotFoundException("No such district");

            user.UserContext.Disctricts.Add(district);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
