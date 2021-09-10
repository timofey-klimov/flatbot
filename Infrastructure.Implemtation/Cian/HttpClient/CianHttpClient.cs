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
        private readonly ILoggerService _logger;
        private readonly IProxyManager _manager;

        public CianHttpClient(
            ILoggerService loggerService,
            IProxyManager proxyManager)
        {
            _logger = loggerService;
            _client = new System.Net.Http.HttpClient();
            _manager = proxyManager;
        }

        public ICianHttpClient CreateClientWithProxy()
        {
            _logger.Info("CreateClientWithProxy");

            var proxys = _manager.GetProxys();

            var currentProxy = proxys.ElementAt(new Random().Next(0, proxys.Count));

            var handler = new HttpClientHandler()
            {
                Proxy = new HttpToSocks5Proxy(currentProxy.Ip, currentProxy.Port)
            };

            _client = new System.Net.Http.HttpClient(handler);

            return this;
        }

        public async Task<byte[]> GetExcelFromCianAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                _logger.Error(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<string> GetPageAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                _logger.Error(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();

        }
    }
}
