using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Telegram;
using Infrastructure.Interfaces.Telegram.Dto;
using Infrastructure.Interfaces.Telegram.MessageSender;
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
        private readonly ILoggerService _loggerService;
        public TelegramMessageSender(string baseAddress, ILoggerService loggerService)
        {
            _client = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            _loggerService = loggerService;
        }

        public async Task<TelegramMessageSenderResult> SendMessageAsync(string message, long chatId)
        {
            try
            {
                var url = $"{_client.BaseAddress}/notify/{chatId}/new";

                var request = new { Message = message };

                var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                await _client.PostAsync(url, body);

                return TelegramMessageSenderResult.Ok();
            }
            catch (Exception ex)
            {
                _loggerService.Error(this.GetType(), ex.Message);
                return TelegramMessageSenderResult.Fail();
            }
        }

        public async Task<TelegramMessageSenderResult> SendMessagesAsync(ICollection<NotificationDto> items, long chatId)
        {
            try
            {
                var url = $"{_client.BaseAddress}/notify/{chatId}/new/objects";

                var request = new { Messages = items };

                var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                await _client.PostAsync(url, body);

                return TelegramMessageSenderResult.Ok();
            }
            catch (Exception ex)
            {
                _loggerService.Error(this.GetType(), ex.Message);
                return TelegramMessageSenderResult.Fail();
            }
        }
    }
}
