using Infrastructure.Interfaces.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.Events.ExcelDownloaded
{
    public class HtmlDownloadedEvent : IEvent
    {
        public string Data { get; }

        public HtmlDownloadedEvent(string data)
        {
            Data = data;
        }
    }
}
