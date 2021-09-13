using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Poll;

namespace Infrastructure.Implemtation.Cian
{
    public class CianService : ICianService
    {
        private ICianUrlBuilder _cianUrlBuilder;
        private ICianHttpClient _cianClient;
        private IPollService _pollService;

        public CianService(
            ICianUrlBuilder cianUrlBuilder,
            ICianHttpClient httpCLient,
            IPollService pollService)
        {
            _cianUrlBuilder = cianUrlBuilder;
            _cianClient = httpCLient;
            _pollService = pollService;
        }

        public string BuildCianUrl(City city, int page)
        {
            return _cianUrlBuilder.BuildCianUrl(city, page);
        }

        public async Task<bool> CheckAnnouncement(string url)
        {
            return await _pollService.Execute(CheckAnnouncementPolls, url, () =>
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

        private async Task<PollResult<bool>> CheckAnnouncementPolls(string url)
        {
            var html = await _cianClient.GetPageAsync(url);

            if (html.Contains("<!doctype html>"))
                return PollResult<bool>.Fail("Ban ip");

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
            var html = await _cianClient.GetPageAsync(url);

            if (html.Contains("<!doctype html>"))
                return PollResult<string>.Fail("Ban ip");

            return PollResult<string>.Success(html);
        }

        private async Task<PollResult<int>> GetPagesCountPolls(City city)
        {
            var domParser = new HtmlParser();
            var token = CancellationToken.None;

            for (int pageNumber = 0; pageNumber < int.MaxValue; pageNumber += 8)
            {
                var url = _cianUrlBuilder.BuildCianUrl(city, pageNumber);

                var content = await _cianClient.GetPageAsync(url);

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
