using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.Proxy.HttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Proxy.HttpClient
{
    public class ProxyHttpClient : IProxyHttpClient
    {
        private System.Net.Http.HttpClient _httpClient;

        const string hidemiAddress = "https://hidemy.name/ru/api/proxylist.php?out=js&type=5&code=658844074094668";
        const string cianUrl = "https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=flat&p=7&region=1&room1=1&room2=1&type=4";

        public ProxyHttpClient()
        {
            _httpClient = new System.Net.Http.HttpClient(); 
        }


        public async Task<bool> CheckProxyAsync(string host, int port)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    Proxy = new MihaZupan.HttpToSocks5Proxy(host, port)
                };

                _httpClient = new System.Net.Http.HttpClient(handler);

                var result = await _httpClient.GetAsync(cianUrl);

                if (result.IsSuccessStatusCode)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<ProxyDto>> GetProxiesAsync()
        {
            var data = await _httpClient.GetAsync(hidemiAddress);

            var streamData = await data.Content.ReadAsStreamAsync();

            using (var streamReader = new StreamReader(streamData))
            {
                var result = await streamReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<ICollection<ProxyDto>>(result);
            }
        }
    }
}
