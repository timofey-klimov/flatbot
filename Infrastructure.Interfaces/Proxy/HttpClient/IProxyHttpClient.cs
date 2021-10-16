using Infrastructure.Interfaces.Cian.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Proxy.HttpClient
{
    public interface IProxyHttpClient
    {
        Task<bool> CheckProxyAsync(string host, int port);

        Task<ICollection<ProxyDto>> GetProxiesAsync();
    }
}
