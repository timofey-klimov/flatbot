using AngleSharp.Html.Parser;
using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian
{
    public class CianFlatJsonCreator : ICianFlatJsonCreator
    {
        private readonly IParseCianHtmlManager _manager;
        public CianFlatJsonCreator(IParseCianHtmlManager manager)
        {
            _manager = manager;
        }

        public async Task<JToken> CreateAsync(string url)
        {
            var page = await _manager.GetHtmlAsync(url);

            var htmlParser = new HtmlParser();
            var document = await htmlParser.ParseDocumentAsync(page, default);

            var scripts = document
                .QuerySelectorAll("script")
                .Where(x => x.InnerHtml.Contains("window._cianConfig['frontend-offer-card']"))
                .FirstOrDefault();

            if (scripts == null)
                throw new System.Exception("Script was null");

            var json = scripts.InnerHtml;

            var firstChar = json.IndexOf("[{");

            json = json.Remove(0, firstChar - 1).Trim();
            json = json.Remove(json.Length - 1);

            var root = JArray.Parse(json)
                .Children<JObject>()
                .Where(x => x.Value<string>("key") == "defaultState")
                .FirstOrDefault()["value"];

            return root;
        }
    }
}
