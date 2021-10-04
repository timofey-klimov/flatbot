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

namespace UseCases.District.Queries.GetDistricts
{
    public class GetDistrictsHandler : IRequestHandler<GetDistrictsRequest, ICollection<DistrictDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDistrictsHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<DistrictDto>> Handle(GetDistrictsRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.Districts
                        .ProjectTo<DistrictDto>(_mapper.ConfigurationProvider)
                        .ToArrayAsync();
        }
    }
}
