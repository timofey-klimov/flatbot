using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WepApp.Controllers
{
    [Route("/api/bot")]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBotClient telegramBotClient;

        public BotController(ITelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
        }

        [HttpGet]
        public string Check()
        {
            return "Ok";
        }

        [HttpPost]
        public async Task Handle([FromBody]Update update)
        {
            if (update.Message.Text == "/start")
                await telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "hello");

        }
    }
}
