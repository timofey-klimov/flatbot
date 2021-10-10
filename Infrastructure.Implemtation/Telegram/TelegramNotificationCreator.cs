using Entities.Models;
using Infrastructure.Interfaces.BitmapManager;
using Infrastructure.Interfaces.Cian.FileService;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Telegram;
using Infrastructure.Interfaces.Telegram.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Infrastructure.Implemtation.Telegram
{
    public class TelegramNotificationCreator : ITelegramNotificationCreator
    {
        private readonly string _tgClientUrl;
        private readonly ICianFileService _fileService;
        private readonly ILoggerService _logger;
        private readonly IImageManager _imageManager;

        public TelegramNotificationCreator(
            IConfiguration configuration, 
            ICianFileService fileService,
            ILoggerService loggerService,
            IImageManager imageManager)
        {
            _tgClientUrl = configuration.GetSection("ClientAppUrl").Value;
            _fileService = fileService;
            _logger = loggerService;
            _imageManager = imageManager;
        }
        public ICollection<string> CreateMessages(ICollection<Flat> flats, int elementsInMessage)
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
                    var price = item.Price.HasValue ? "Неизвестно" : $"{item.Price.To<int>()}";
                    var wayToGo = item.WayToGo == Entities.Enums.WayToGo.Car ? "на транспорте" : "пешком";

                    builder.AppendLine("Новая квартира")
                        .AppendLine($"Цена: {price}")
                        .AppendLine($"Залог: {pledge}")
                        .AppendLine($"Комиссия: {comission}")
                        .AppendLine($"Метро: {item.Metro}")
                        .AppendLine($"Адрес:{item.Address}")
                        .AppendLine($"Площадь:{item.FlatArea} кв.м")
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

        public async Task<ICollection<NotificationDto>> CreateObjectsAsync(ICollection<Flat> flats)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var items = new List<NotificationDto>(flats.Count);
            var tasks = new List<Task>();
            foreach (var flat in flats)
            {
                var task = Task.Run(async () =>
                {
                    var item = new NotificationDto();

                    item.Message = CreateMessage(flat);

                    var images = await _fileService.GetCianFlatImagesAsync(flat);

                    Stream image = default;

                    if (images.Count >= 2)
                        image = _imageManager.GlueImages(images.First(), images.Last());
                    else
                        image = images.FirstOrDefault();

                    item.HasImage = image == null ? false : true;

                    item.Image = image.ToByteArray();

                    items.Add(item);
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            var time = stopwatch.Elapsed.TotalSeconds;

            _logger.Info($"Время выполнения: {time}");

            return items;
        }

        public string CreateMessage(Flat flat)
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
