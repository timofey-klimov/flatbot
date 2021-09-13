using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Implemtation.Cian
{
    public class ProxyManager : IProxyManager
    {
        private IDbContext _dbContext;
        private IMapper _mapper;
        public ProxyManager(
            IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IReadOnlyCollection<ProxyDto> GetProxys()
        {
            return _dbContext.Proxies
                .ProjectTo<ProxyDto>(_mapper.ConfigurationProvider)
                .ToArray();

        }
    }
}
