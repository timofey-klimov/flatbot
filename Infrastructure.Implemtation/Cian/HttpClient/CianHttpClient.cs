using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using MihaZupan;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Utils;

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
            if (url.IsEmpty())
                throw new ArgumentNullException(url);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(nameof(HttpException));

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<string> GetPageAsync(string url)
        {
            var ctsS = new CancellationTokenSource();
            ctsS.CancelAfter(TimeSpan.FromSeconds(10));

            var token = ctsS.Token;
            if (url.IsEmpty())
                throw new ArgumentNullException(url);

            var response = await _client.GetAsync(url, token);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(nameof(HttpException));

            return await response.Content.ReadAsStringAsync();

        }

        public async Task<byte[]> GetFileInBytesAsync(string url)
        {
            if (url.IsEmpty())
                throw new ArgumentNullException(url);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(nameof(HttpException));

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<Stream> GetFileInStreamAsync(string url)
        {
            if (url.IsEmpty())
                throw new ArgumentNullException(url);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpException(nameof(HttpException));

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
