using Infrastructure.Interfaces.Cian.HttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.HttpClient
{
    public class TelegramHttpClient : IClientMessageSender
    {
        private readonly System.Net.Http.HttpClient _client;
        public TelegramHttpClient(string baseAddress)
            
        {
            _client = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task SendMessageAsync(string message, long chatId)
        {
            var url = $"{_client.BaseAddress}/notify/{chatId}/new";

            var request = new { Message = message };

            var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            await _client.PostAsync(url, body);
        }
    }
}
