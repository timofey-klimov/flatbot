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

namespace Infrastructure.Implemtation.Cian
{
    public class CianService : ICianService
    {
        private readonly ICianUrlBuilder _cianUrlBuilder;
        private readonly System.Net.Http.HttpClient _httpClient;
        public CianService(ICianUrlBuilder cianUrlBuilder, IHttpClientFactory httpClient)
        {
            _cianUrlBuilder = cianUrlBuilder;
            _httpClient = httpClient.CreateClient();
        }

        public async Task<int?> GetPagesCount(City city, DealType dealType, Room room)
        {
            try
            {
                var domParser = new HtmlParser();
                var token = CancellationToken.None;

                for (int pageNumber = 0; pageNumber < int.MaxValue; pageNumber += 8)
                {
                    var url = _cianUrlBuilder.BuildCianUrl(city, dealType, room, OperationType.GetFlats, pageNumber);

                    var page = await (await _httpClient.GetAsync(url))
                        .Content
                        .ReadAsStringAsync();

                    IHtmlDocument document = await domParser.ParseDocumentAsync(page, token);

                    var pagination = document.QuerySelectorAll("div")
                            .Where(x => x.GetAttribute("data-name") == "Pagination")
                            .FirstOrDefault();

                    var lastPage = pagination.QuerySelector("ul")
                        .QuerySelectorAll("li")
                        .Where(x => x.QuerySelector("a") != null)
                        .Select(x => x.QuerySelector("a")?.TextContent)
                        .Last();

                    if (lastPage != "..")
                        return int.Parse(lastPage);
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
