using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Poll;
using System;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces.Logger;
using System.Collections.Generic;

namespace Infrastructure.Implemtation.Cian
{
    public class ParseCianManager : IParseCianManager
    {
        private readonly ICianHttpClient _cianClient;
        private readonly IPollService _pollService;
        private readonly ILoggerService _logger;
        private readonly ICianUrlBuilder _cianUrlBuilder;

        public ParseCianManager(
            ICianHttpClient httpCLient,
            IPollService pollService,
            ILoggerService logger,
            ICianUrlBuilder cianUrlBuilder)
        {
            _cianClient = httpCLient;
            _pollService = pollService;
            _logger = logger;
            _cianUrlBuilder = cianUrlBuilder;
        }

        public async Task<bool> CheckAnnouncement(string url)
        {
            return await _pollService.Execute(CheckAnnouncementPolls, url, () =>
            {
                _cianClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });
        }

        public async Task<string> GetCianImageSourceAsync(string url)
        {
            return await _pollService.Execute(GetCianImagesPoll, url, () =>
            {
                _cianClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });
        }

        public async Task<string> GetHtmlAsync(string url)
        {
            return await _pollService.Execute(GetHtmlPoll, url, () =>
            {
                _cianClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });
        }

        public async Task<int> GetPagesCountAsync(City city)
        {
            return await _pollService.Execute(GetPagesCountPolls, city, () =>
            {
                _cianClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });
        }

        #region polls

        private async Task<PollResult<string>> GetCianImagesPoll(string url)
        {
            string html = await GetHtmlAsync(url);

            var document = await new HtmlParser().ParseDocumentAsync(html);

            var images = document
                .QuerySelectorAll("div")
                .Where(x => x.GetAttribute("data-name") == "PrintPhoto")
                .Select(x => x.Children.First().GetAttribute("src"))
                .FirstOrDefault();

            return PollResult<string>.Success(images);
        }

        private async Task<PollResult<bool>> CheckAnnouncementPolls(string url)
        {
            string html = string.Empty;
            try
            {
                html = await _cianClient.GetPageAsync(url);

                if (html.Contains("<!doctype html>"))
                    return PollResult<bool>.Fail("Ban ip");
            }
            catch (Exception ex)
            {
                return PollResult<bool>.Fail(ex.Message);
            }

            var document = await new HtmlParser().ParseDocumentAsync(html);

            var element = document
                .QuerySelectorAll("div")
                .Where(x => x.GetAttribute("data-name") == "OfferUnpublished")
                .FirstOrDefault();

            if (element == null)
                return PollResult<bool>.Success(false);

            return PollResult<bool>.Success(true);

        }

        private async Task<PollResult<string>> GetHtmlPoll(string url)
        {
            string html = string.Empty;
            
            try
            {
                 html = await _cianClient.GetPageAsync(url);

                if (html.Contains("<!doctype html>"))
                    return PollResult<string>.Fail("Ban ip");
            }
            catch(Exception ex)
            {
                return PollResult<string>.Fail(ex.Message);
            }


            return PollResult<string>.Success(html);
        }

        private async Task<PollResult<int>> GetPagesCountPolls(City city)
        {
            var domParser = new HtmlParser();
            var token = CancellationToken.None;

            for (int pageNumber = 0; pageNumber < int.MaxValue; pageNumber += 8)
            {
                var url = _cianUrlBuilder.BuildCianUrl(city, pageNumber);

                string content = string.Empty;

                try
                {
                    content = await _cianClient.GetPageAsync(url);
                }
                catch(Exception ex)
                {
                    return PollResult<int>.Fail(ex.Message);
                }


                IHtmlDocument document = await domParser.ParseDocumentAsync(content, token);

                var pagination = document
                    ?.QuerySelectorAll("div")
                    ?.Where(x => x.GetAttribute("data-name") == "Pagination")
                    ?.FirstOrDefault();

                if (pagination == null)
                    return PollResult<int>.Fail("Ban Ip");

                var lastPage = pagination
                    ?.QuerySelector("ul")
                    ?.QuerySelectorAll("li")
                    ?.Where(x => x.QuerySelector("a") != null)
                    ?.Select(x => x.QuerySelector("a")?.TextContent)
                    ?.Last();

                if (lastPage != "..")
                    return PollResult<int>.Success(int.Parse(lastPage));

                await Task.Delay(3000);
            }

            return default;
        }

        #endregion
    }
}
