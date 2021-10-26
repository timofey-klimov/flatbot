using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianFlatJsonCreator
    {
        Task<JToken> CreateAsync(string url);
    }
}
