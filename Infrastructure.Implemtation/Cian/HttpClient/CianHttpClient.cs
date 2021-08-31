using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian.HttpClient;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.HttpClient
{
    public class CianHttpClient : ICianHttpClient
    {
        private readonly System.Net.Http.HttpClient _client;
        public CianHttpClient()
        {
            _client = new System.Net.Http.HttpClient();
        }

        public async Task<byte[]> GetExcelFromCian(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new CianHttpClientException($"StatusCode:{response.StatusCode}, Message:{await response.Content.ReadAsStringAsync()}");

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
