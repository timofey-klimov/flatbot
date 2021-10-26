using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Entities.Models;
using Entities.Models.FlatEntities;
using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Infrastructure.Implemtation.Cian
{
    public class FinderCianFlatsByHtml : IFinderCianFlatsByHtml
    {
        private readonly ILoggerService _logger;

        public FinderCianFlatsByHtml(
            ILoggerService logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<FindedFlatDto>> ExecuteAsync(string html)
        {
            var items = new List<FindedFlatDto>();

            var htmlParser = new HtmlParser();

            var document = await htmlParser.ParseDocumentAsync(html);
            IEnumerable<IElement> cards = GetCards(document);

            if (!cards.Any())
                throw new EmptyPageException();

            foreach (var card in cards)
            {
                try
                {
                    IElement linkArea = GetLinAreaElement(card);
                    string cianReference = GetCianReference(linkArea);
                    long cianId = GetCianId(cianReference);

                    items.Add(new FindedFlatDto(cianId, cianReference));
                }
                catch (Exception ex)
                {
                    _logger.Error(this.GetType(), ex.Message);
                }
            }

            return items;
        }
        
        private IEnumerable<IElement> GetCards(IHtmlDocument document)
        {
            return document
                    .QuerySelectorAll("article")
                    .Where(x => x.GetAttribute("data-name") == "CardComponent");
        }

        private long GetCianId(string cianReference)
        {
            return cianReference
                    .Split('/')
                    .ElementAt(5)
                    .ToLong();
        }

        private string GetCianReference(IElement linkArea)
        {
            return linkArea
                    .QuerySelector("a")
                    .GetAttribute("href");
        }

        private IElement GetLinAreaElement(IElement card)
        {
            return card.QuerySelectorAll("div")
                       .Where(x => x.GetAttribute("data-name") == "LinkArea")
                       .FirstOrDefault();
        }
    }
}
