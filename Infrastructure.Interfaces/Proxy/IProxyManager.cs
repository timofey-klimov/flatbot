using Infrastructure.Interfaces.Cian.Dto;
using System.Collections.Generic;

namespace Infrastructure.Interfaces.Cian
{
    public interface IProxyManager
    {
        IReadOnlyCollection<ProxyDto> GetProxys();
    }
}
