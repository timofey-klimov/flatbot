using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.HttpClient
{
    public interface ICianHttpClient
    {
        Task<byte[]> GetExcelFromCianAsync(string url);

        Task<string> GetPageAsync(string url);
    }
}
