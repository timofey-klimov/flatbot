using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian
{
    public class ProxyManager : IProxyManager
    {
        private readonly ICollection<Proxy> _proxies;
        public ProxyManager(ICollection<Proxy> proxies)
        {
            if (!proxies.Any())
                throw new ArgumentException();
            _proxies = proxies;
        }

        public ICollection<Proxy> GetProxys()
        {
            return _proxies;
        }
    }
}
