﻿using AngleSharp.Html.Parser;
using Entities.Models;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Infrastructure.Implemtation.Cian.EventHandlers
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
            var htmlParser = new HtmlParser();

            var document = await htmlParser.ParseDocumentAsync(@event.Data);

            var cards = document
                .QuerySelectorAll("article")
                .Where(x => x.GetAttribute("data-name") == "CardComponent");

            if (!cards.Any())
                throw new Exception("Error in html");

            foreach (var card in cards)
            {
                try
                {
                    var linkArea = card.QuerySelectorAll("div")
                        .Where(x => x.GetAttribute("data-name") == "LinkArea")
                        .FirstOrDefault();

                    var cianReference = linkArea
                        .QuerySelector("a")
                        .GetAttribute("href");

                    var cianId = cianReference
                        .Split('/')
                        .ElementAt(5)
                        .ToLong();

                    var titleInfo = card.QuerySelectorAll("div")
                            .Where(x => x.GetAttribute("data-name") == "TitleComponent")
                            .FirstOrDefault()
                            .QuerySelector("span")
                            .QuerySelector("span")
                            .TextContent;


                    if (titleInfo.Split(',').Length < 3)
                    {
                        titleInfo = card.QuerySelectorAll("span")
                            .Where(x => x.GetAttribute("data-mark") == "OfferSubtitle")
                            .FirstOrDefault()
                            .TextContent;
                    }

                    var splitedTitleInfo = titleInfo.Split(',');

                    var roomCount = splitedTitleInfo[0].Split('-')[0].Trim().ToInt();

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

                    var time = distanceInformation?.Split(' ')[0]?.ToNullableInt();

                    var onCar = !distanceInformation?.Contains("пешком");

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

                    var rentalFeeInformation = linkArea
                        ?.QuerySelectorAll("p")
                        ?.Where(x => x.GetAttribute("data-mark") == "PriceInfo")
                        ?.FirstOrDefault()
                        ?.TextContent;


                    var formatPriceInfo = FormatPriceInfo(rentalFeeInformation);

                    var priceInfo = linkArea
                        ?.QuerySelectorAll("span")
                        ?.Where(x => x.GetAttribute("data-mark") == "MainPrice")
                        ?.FirstOrDefault()
                        ?.Children
                        .FirstOrDefault()
                        ?.TextContent;

                    var price = priceInfo?.Replace(" ", "")?.Replace("₽/мес.", "")?.Trim().ToDecimal();

                    var flat = new Flat
                    {
                        CianId = cianId,
                        RoomCount = roomCount,
                        FlatArea = flatArea,
                        CurrentFloor = currentFloor,
                        MaxFloor = maxFloor,
                        Metro = metro,
                        TimeToMetro = time,
                        WayToGo = onCar == true ? Entities.Enums.WayToGo.Car : Entities.Enums.WayToGo.Walk,
                        Address = address,
                        Comission = formatPriceInfo.Commision,
                        Pledge = formatPriceInfo.Pledge,
                        MoreThanYear = formatPriceInfo.MoreThanYear,
                        Price = price,
                        CianReference = cianReference
                    };

                    var entity = await _context.Flats.FirstOrDefaultAsync(x => x.CianId == flat.CianId);

                    if (entity != null)
                    {
                        var entCreateDate = entity.CreateDate;
                        flat.UpdateDate = DateTime.Now;
                        flat.CreateDate = entCreateDate;
                        entity = flat;
                    }
                    else
                    {
                        await _context.Flats.AddAsync(flat);
                        _logger.Info($"Create new flat CianId: {flat.CianId}");
                    }

                    await _context.SaveChangesAsync(default);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
            }
        }

        private (int? Commision, decimal? Pledge, bool? MoreThanYear) FormatPriceInfo(string priceInfo)
        {
            if (priceInfo.IsEmpty())
                return default;

            int? commision = null;
            decimal? pledge = null;

            var splited = priceInfo.Split(',');

            bool? moreThanYear = splited.ElementAtOrDefault(0)?.Contains("года");

            bool? needCommision = !splited.ElementAtOrDefault(2)?.Trim()?.Contains("без комиссии");

            bool? needPlegde = !splited.ElementAtOrDefault(3)?.Trim()?.Contains("без залога");

            if (needCommision == true)
            {
                commision = splited.ElementAtOrDefault(2)?.Trim()?.Split(' ')[1]?.Replace("%", "")?.Trim().ToNullableInt();
            }

            if (needPlegde == true)
            {
                pledge = splited.ElementAtOrDefault(3)?.Replace(" ", "")?.Replace("залог", "")?.Replace("₽", "")?.Trim().ToNullableDecimal();
            }

            return (commision, pledge, moreThanYear);
        }
    }
}