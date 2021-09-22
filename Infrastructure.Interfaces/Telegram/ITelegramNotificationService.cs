using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram
{
    public interface ITelegramNotificationService
    {
        ICollection<string> CreateMany(ICollection<Flat> flats, int elementsInMessage);

        string CreateOne(Flat item);
    }
}
