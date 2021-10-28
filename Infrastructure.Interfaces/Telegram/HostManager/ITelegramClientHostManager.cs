using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram.HostManager
{
    public interface ITelegramClientHostManager
    {
        string HostUrl { get; }
    }
}
