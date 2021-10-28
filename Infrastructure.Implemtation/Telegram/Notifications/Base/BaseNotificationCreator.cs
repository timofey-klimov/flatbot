using Entities.Models.FlatEntities;
using Infrastructure.Interfaces.Telegram.HostManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implemtation.Telegram.NotificationCreators
{
    public abstract class BaseNotificationCreator
    {
        private readonly ITelegramClientHostManager _hostManager;
        public BaseNotificationCreator(ITelegramClientHostManager hostManager)
        {
            _hostManager = hostManager;
        }

        protected string CreateNotificationMessage(Flat flat, long chatId)
        {
            if (flat == null)
                throw new ArgumentException(nameof(flat));

            var builder = new StringBuilder();

            foreach (var undergroundInfo in flat.UndergroundInfos)
            {
                switch (undergroundInfo.Type)
                {
                    case "walk":
                        builder.Append($"<strong>м.{undergroundInfo.Name} {undergroundInfo.Time}мин. <i>&#127939;</i></strong>");
                        break;

                    case "transport":
                        builder.Append($"<strong>м.{undergroundInfo.Name} {undergroundInfo.Time}мин. <i>&#128663;</i></strong>");
                        break;

                }
                builder.Append("\n");
            }

            builder.Append($"<i>&#127984</i> {flat.Address}");
            builder.Append("\n");
            builder.Append("\n");

            builder.Append($"<strong>{flat.RoomsCount}-комн. квартира {flat.TotalArea}м, {flat.CurrentFloor}/{flat.FloorsCount}этаж</strong>");
            builder.Append("\n");

            if (flat.CellingHeight != null)
            {
                builder.Append($"<strong>Высота потолков {flat.CellingHeight}м</strong>");
                builder.Append("\n");
            }

            if (flat?.BuildingInfo?.BuildYear != null)
            {
                builder.Append($"Год постройки {flat.BuildingInfo.BuildYear}");
                builder.Append("\n");
            }

            if (flat?.BuildingInfo?.Type != null)
            {
                builder.Append($"Тип здания {GetBuildingTypeName(flat.BuildingInfo.Type)}");
                builder.Append("\n");
            }

            builder.Append("\n");

            builder.Append($"<strong>Цена {flat.PriceInfo.Price} руб.</strong>");
            builder.Append("\n");

            var comission = flat.PriceInfo?.AgentFee == null ? "Нет" : $"{flat.PriceInfo.AgentFee}%";
            builder.Append($"<strong>Комиссия {comission}</strong>");
            builder.Append("\n");

            var deposit = flat.PriceInfo?.Deposit == null ? "Нет" : $"{flat.PriceInfo.Deposit} руб.";
            builder.Append($"<strong>Депозит {deposit}</strong>");
            builder.Append("\n");
            builder.Append("\n");

            builder.Append($"<a href=\"{_hostManager.HostUrl}/link/{flat.Id}/{chatId}\"><i>&#128073;</i>Посмотреть подробнее...</a>");

            builder.Append("\n");
            builder.Append("\n");

            if (flat?.Description != null)
            {
                var description = flat.Description;

                if (flat.Description.Length > 120)
                {
                    description = description.Substring(0, 120) + "...";
                }

                builder.Append($"{description}");
                builder.Append("\n");
            }


            return builder.ToString();
        }

        private string GetBuildingTypeName(string type)
        {
            var dictionary = new Dictionary<string, string>
            {
                { "block", "блочный" },
                { "brick", "кирпичный" },
                { "monolith", "монолитный" },
                { "monolithBrick", "кирпичный  монолит" },
                { "panel", "панельный" }
            };

            return dictionary[type];
        }
    }
}

