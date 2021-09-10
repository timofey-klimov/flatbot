using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using Infrastructure.Implemtation.Cian.Exceptions;
using System.Net.Http;
using MihaZupan;
using Infrastructure.Interfaces.Cian.HttpClient;

namespace Infrastructure.Implemtation.Cian
{
    public class CianService : ICianService
    {
        private readonly ICianUrlBuilder _cianUrlBuilder;
        private readonly ICianHttpClient _cianClient;

        public CianService(
            ICianUrlBuilder cianUrlBuilder,
            ICianHttpClient httpCLient)
        {
            _cianUrlBuilder = cianUrlBuilder;
            _cianClient = httpCLient;
        }

        public async Task<int?> GetPagesCount(City city)
        {
            try
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
                        throw new BanIpException("Ban IP");

                    var lastPage = pagination
                        ?.QuerySelector("ul")
                        ?.QuerySelectorAll("li")
                        ?.Where(x => x.QuerySelector("a") != null)
                        ?.Select(x => x.QuerySelector("a")?.TextContent)
                        ?.Last();

                    if (lastPage != "..")
                        return int.Parse(lastPage);

                    await Task.Delay(2000);
                }

                return default;
            }
            catch (Exception ex)
            {
                throw new GetPagesCountException(ex.Message);
            }
        }
    }
}
