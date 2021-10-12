using Infrastructure.Interfaces.Telegram;
using Infrastructure.Interfaces.Telegram.Dto;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Telegram
{
    public class TelegramMessageSender : ITelegramMessageSender
    {
        private readonly System.Net.Http.HttpClient _client;
        public TelegramMessageSender(string baseAddress)
            
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

        public async Task SendMessagesAsync(ICollection<NotificationDto> items, long chatId)
        {
            var url = $"{_client.BaseAddress}/notify/{chatId}/new/objects";

            var request = new { Messages = items };

            var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            await _client.PostAsync(url, body);
        }
    }
}
