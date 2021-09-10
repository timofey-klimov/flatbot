using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Logger;
using MihaZupan;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.HttpClient
{
    public class CianHttpClient : ICianHttpClient
    {
        private readonly System.Net.Http.HttpClient _client;
        private readonly ILoggerService _logger;
        public CianHttpClient(ILoggerService loggerService)
        {
            _logger = loggerService;
            _client = new System.Net.Http.HttpClient();
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
