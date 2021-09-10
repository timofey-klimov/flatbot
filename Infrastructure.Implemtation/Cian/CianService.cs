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

        public string BuildCianUrl(City city, OperationType type, int page)
        {
            return _cianUrlBuilder.BuildCianUrl(city, type, page);
        }

        public async Task<byte[]> GetExcelFromCianAsync(string url)
        {
            return await _pollService.Execute(GetExcelFromCianPoll, url, () =>
            {
                _cianClient = _cianClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });
        }

        public async Task<int> GetPagesCountAsync(City city)
        {
            return await _pollService.Execute(GetPagesCountPolls, city, () =>
            {
                _cianClient = _cianClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });
        }


        private async Task<PollResult<byte[]>> GetExcelFromCianPoll(string url)
        {
            var bytes = await _cianClient.GetExcelFromCianAsync(url);

            return PollResult<byte[]>.Success(bytes);
        }

        private async Task<PollResult<int>> GetPagesCountPolls(City city)
        {
            var domParser = new HtmlParser();
            var token = CancellationToken.None;

            for (int pageNumber = 0; pageNumber < int.MaxValue; pageNumber += 8)
            {
                var url = _cianUrlBuilder.BuildCianUrl(city, OperationType.GetFlats, pageNumber);

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
    }
}
