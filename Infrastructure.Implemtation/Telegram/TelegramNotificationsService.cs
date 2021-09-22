using Entities.Models;
using Infrastructure.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Implemtation.Telegram
{
    public class TelegramNotificationsService : ITelegramNotificationService
    {
        public ICollection<string> CreateMany(ICollection<Flat> flats, int elementsInMessage)
        {
            if (!flats.Any())
                throw new ArgumentException(nameof(flats));

            if (elementsInMessage <= 0)
                throw new ArgumentException(nameof(elementsInMessage));

            var notifications = new List<string>(flats.Count);

            var chunks = (int)Math.Round((double)flats.Count / elementsInMessage);

            if (chunks == 0)
                chunks = 1;

            for (int i = 0; i < chunks; i++)
            {
                var builder = new StringBuilder();
                var flatsToSend = flats.Skip(i * elementsInMessage).Take(elementsInMessage);

                foreach (var item in flatsToSend)
                {
                    var pledge = item.Pledge == null ? "Нет" : item.Pledge.ToString();
                    var comission = item.Comission == null ? "Нет" : $"{item.Comission}%";
                    var wayToGo = item.WayToGo == Entities.Enums.WayToGo.Car ? "на транспорте" : "пешком";

                    builder.AppendLine("Новая квартира")
                        .AppendLine($"Цена {item.Price}")
                        .AppendLine($"Залог {pledge}")
                        .AppendLine($"Комиссия {comission}")
                        .AppendLine($"Метро: {item.Metro}")
                        .AppendLine($"Адрес:{item.Address}")
                        .AppendLine($"Этаж: {item.CurrentFloor}")
                        .AppendLine($"Этажей в доме: {item.MaxFloor}")
                        .AppendLine($"Время до метро: {item.TimeToMetro} минут {wayToGo}")
                        .AppendLine($"Ссылка: {item.CianReference}")
                        .AppendLine(string.Empty);
                }

                notifications.Add(builder.ToString());
            }

            return notifications;
        }

        public string CreateOne(Flat flat)
        {
            if (flat == null)
                throw new ArgumentException(nameof(flat));

            var pledge = flat.Pledge == null ? "Нет" : flat.Pledge.ToString();
            var comission = flat.Comission == null ? "Нет" : flat.Comission.ToString();

            var builder = new StringBuilder();

            builder.AppendLine("Новая квартира")
                .AppendLine($"Цена {flat.Price}")
                .AppendLine($"Залог {pledge}")
                .AppendLine($"Комиссия {comission}%")
                .AppendLine($"Метро: {flat.Metro}")
                .AppendLine($"Адрес:{flat.Address}")
                .AppendLine($"Этаж: {flat.CurrentFloor}")
                .AppendLine($"Этажей в доме: {flat.MaxFloor}")
                .AppendLine($"Ссылка: {flat.CianReference}")
                .AppendLine(string.Empty);

            return builder.ToString();
        }
    }
}
