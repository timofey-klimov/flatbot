using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Logger;
using MihaZupan;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.HttpClient
{
    public class CianHttpClient : ICianHttpClient
    {
        private System.Net.Http.HttpClient _client;
        private readonly IProxyManager _manager;

        public CianHttpClient(
            IProxyManager proxyManager)
        {
            _client = new System.Net.Http.HttpClient();
            _manager = proxyManager;
        }

        public void CreateClientWithProxy()
        {
            var proxys = _manager.GetProxys();

            var currentProxy = proxys.ElementAt(new Random().Next(0, proxys.Count));

            var handler = new HttpClientHandler()
            {
                Proxy = new HttpToSocks5Proxy(currentProxy.Ip, currentProxy.Port)
            };

            _client = new System.Net.Http.HttpClient(handler);

        }

        public async Task<byte[]> GetExcelFromCianAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(nameof(HttpException));

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<string> GetPageAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(nameof(HttpException));

            return await response.Content.ReadAsStringAsync();

        }
    }
}
