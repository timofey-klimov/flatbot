﻿using Infrastructure.Interfaces.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.Events.ExcelDownloaded
{
    public class ExcelDownloadedEvent : IEvent
    {
        public byte[] Data { get; }

        public ExcelDownloadedEvent(byte[] data)
        {
            Data = data;
        }
    }
}
