using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.HttpClient
{
    public interface ICianHttpClient
    {
        Task<byte[]> GetExcelFromCian(string url);
    }
}
