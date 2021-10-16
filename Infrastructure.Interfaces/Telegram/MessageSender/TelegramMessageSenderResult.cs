using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram.MessageSender
{
    public class TelegramMessageSenderResult
    {
        public bool Success { get; }

        public TelegramMessageSenderResult(bool success)
        {
            Success = success;
        }

        public static TelegramMessageSenderResult Ok() => new TelegramMessageSenderResult(true);

        public static TelegramMessageSenderResult Fail() => new TelegramMessageSenderResult(false);
    }
}
