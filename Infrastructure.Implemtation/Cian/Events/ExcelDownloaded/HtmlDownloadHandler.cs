using AngleSharp.Html.Parser;
using Entities.Models;
using Entities.Models.ValueObjects;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Infrastructure.Implemtation.Cian.Events.ExcelDownloaded
{
    public class HtmlDownloadHandler : IEventBusHandler<HtmlDownloadedEvent>
    {
        private readonly ILoggerService _logger;
        private readonly IDbContext _context;

        public HtmlDownloadHandler(
            IDbContext dbContext,
            ILoggerService loggerService)
        {
            _context = dbContext;
            _logger = loggerService;
        }

        public async Task HandleAsync(HtmlDownloadedEvent @event)
        {
            try
            {
                var htmlParser = new HtmlParser();

                var document = await htmlParser.ParseDocumentAsync(@event.Data);

                var cards = document
                    .QuerySelectorAll("article")
                    .Where(x => x.GetAttribute("data-name") == "CardComponent");

                if (!cards.Any())
                    throw new Exception("Error in html");

                foreach (var card in cards)
                {
                    var linkArea = card.QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "LinkArea")
                        .FirstOrDefault();

                    var cianId = linkArea
                        .QuerySelector("a")
                        .GetAttribute("href")
                        .Split('/')
                        .ElementAt(5)
                        .ToLong();

                    var titleInfo = card.QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "TitleComponent")
                        .FirstOrDefault()
                        .QuerySelector("span")
                        .QuerySelector("span")
                        .TextContent;

                    var splitedTitleInfo = titleInfo.Split(',');

                    var flatCount = splitedTitleInfo[0].Split('-')[0].Trim().ToInt();

                    var flatArea = splitedTitleInfo[1].Trim().Split(' ')[0].ToDouble();

                    var floorInfo = splitedTitleInfo[2].Replace("этаж", "").Trim();

                    var currentFloor = floorInfo.Split('/')[0].ToInt();

                    var maxFloor = floorInfo.Split('/')[1].ToInt();

                    var specialGeo = card.QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "SpecialGeo")
                        .FirstOrDefault();

                    var metro = specialGeo
                        .QuerySelector("a")
                        .QuerySelectorAll("div")
                        .Last()
                        .TextContent;

                    var distanceInformation = specialGeo
                        .Children
                        .Last()
                        .TextContent;

                    var time = distanceInformation.Split(' ')[0].ToInt();

                    var onCar = !distanceInformation.Contains("пешком");

                    var addressInfo = card
                        .QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "ContentRow")
                        .FirstOrDefault()
                        .Children
                        .Last()
                        .QuerySelectorAll("a")
                        .Where(x => x.GetAttribute("data-name") == "GeoLabel")
                        .Select(x => x.TextContent)
                        .ToArray();

                    var address = string.Join(',', addressInfo);

                    var price = card
                        .QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "LinkArea")
                        .FirstOrDefault()
                        .QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "ContentRow")
                        .Last()
                        .QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "ContentRow")
                        .FirstOrDefault()
                        .QuerySelectorAll("span")
                        .Last()
                        .TextContent;


                }



            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.GetType().Name} {ex.Message}");
            }
        }
    }
}
