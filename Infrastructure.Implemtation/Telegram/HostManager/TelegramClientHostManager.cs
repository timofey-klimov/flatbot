using Infrastructure.Interfaces.Telegram.HostManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Telegram.HostManager
{
    public class TelegramClientHostManager : ITelegramClientHostManager
    {
        public string HostUrl { get; set; }

        public TelegramClientHostManager(string hostUrl)
        {
            HostUrl = hostUrl;
        }
    }
}
