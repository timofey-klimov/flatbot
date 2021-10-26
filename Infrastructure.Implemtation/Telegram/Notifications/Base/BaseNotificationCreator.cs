using Entities.Models;
using Entities.Models.FlatEntities;
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

            var pledge = flat.PriceInfo.Deposit == null ? "Нет" : flat.PriceInfo.Deposit.ToString();
            var comission = flat.PriceInfo.AgentFee == null ? "Нет" : $"{flat.PriceInfo.AgentFee}%";

            //builder.AppendLine("Новая квартира")
            //    .AppendLine($"Цена: {price}")
            //    .AppendLine($"Залог: {pledge}")
            //    .AppendLine($"Комиссия: {comission}")
            //    .AppendLine($"Метро: {flat.Metro}")
            //    .AppendLine($"Адрес:{flat.Address}")
            //    .AppendLine($"Площадь:{flat.FlatArea} кв.м")
            //    .AppendLine($"Этаж: {flat.CurrentFloor}")
            //    .AppendLine($"Этажей в доме: {flat.MaxFloor}")
            //    .AppendLine($"Время до метро: {flat.TimeToMetro} минут {wayToGo}")
            //    .AppendLine($"Ссылка: {flat.CianReference}")
            //    .AppendLine(string.Empty);

            return builder.ToString();
        }
    }
}
