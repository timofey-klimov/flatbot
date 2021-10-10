using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Implemtation.Cian
{
    public class ProxyManager : IProxyManager
    {
        private IDbContext _dbContext;
        private IMapper _mapper;
        private IMemoryCache _memoryCache;
        private readonly object obj = new object();

        public ProxyManager(
            IDbContext dbContext,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public IReadOnlyCollection<ProxyDto> GetProxys()
        {
            if (_memoryCache.TryGetValue<ProxyDto[]>("proxies", out var result))
                return result;

            ProxyDto[] proxies = default;

            lock (obj)
            {
                proxies = _dbContext.Proxies
                    .ProjectTo<ProxyDto>(_mapper.ConfigurationProvider)
                    .ToArray();
            }

            var memoryCacheEntry = _memoryCache.CreateEntry("proxies");
            memoryCacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            _memoryCache.Set("proxies", proxies);

            return proxies;
        }
    }
}
