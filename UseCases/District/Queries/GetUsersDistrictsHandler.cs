using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.District.Dto;

namespace UseCases.District.Queries
{
    public class GetUsersDistrictsHandler : IRequestHandler<GetUsersDistrictsRequest, ICollection<DistinctMenuDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUsersDistrictsHandler(
            IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<DistinctMenuDto>> Handle(GetUsersDistrictsRequest request, CancellationToken cancellationToken)
        {
            var districts = _dbContext.Districts
                .ProjectTo<DistrictDto>(_mapper.ConfigurationProvider)
                .ToArray();

            var userDistricts = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .Where(x => x.ChatId == request.ChatId)
                .Select(x => x.UserContext.Disctricts)
                .FirstOrDefaultAsync();

            var collection = _mapper.Map<DistrictDto[]>(userDistricts);

            var unabledDistricts = from d in districts
                                   join u in collection
                                   on d.Name equals u.Name into ps
                                   from p in ps.DefaultIfEmpty()
                                   where p == null
                                   select d;                

            var list = new List<DistinctMenuDto>();

            foreach (var userDistrict in userDistricts)
            {
                list.Add(new DistinctMenuDto() { Id = userDistrict.Id, Name = userDistrict.Name, IsSelected = true });
            }

            foreach (var unableDistrict in unabledDistricts)
            {
                list.Add(new DistinctMenuDto() { Id = unableDistrict.Id, Name = unableDistrict.Name, IsSelected = false });
            }

            return list.OrderBy(x => x.Id).ToList();
        }
    }
}
