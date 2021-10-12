using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Infrastructure.Implemtation.Telegram.NotificationCreators
{
    public abstract class BaseNotificationCreator
    {
        protected string CreateNotificationMessage(Flat flat)
        {
            if (flat == null)
                throw new ArgumentException(nameof(flat));

            var builder = new StringBuilder();

            var pledge = flat.Pledge == null ? "Нет" : flat.Pledge.ToString();
            var comission = flat.Comission == null ? "Нет" : $"{flat.Comission}%";
            var price = !flat.Price.HasValue ? "Неизвестно" : $"{flat.Price.To<int>()}";
            var wayToGo = flat.WayToGo == Entities.Enums.WayToGo.Car ? "на транспорте" : "пешком";

            builder.AppendLine("Новая квартира")
                .AppendLine($"Цена: {price}")
                .AppendLine($"Залог: {pledge}")
                .AppendLine($"Комиссия: {comission}")
                .AppendLine($"Метро: {flat.Metro}")
                .AppendLine($"Адрес:{flat.Address}")
                .AppendLine($"Площадь:{flat.FlatArea} кв.м")
                .AppendLine($"Этаж: {flat.CurrentFloor}")
                .AppendLine($"Этажей в доме: {flat.MaxFloor}")
                .AppendLine($"Время до метро: {flat.TimeToMetro} минут {wayToGo}")
                .AppendLine($"Ссылка: {flat.CianReference}")
                .AppendLine(string.Empty);

            return builder.ToString();
        }
    }
}
