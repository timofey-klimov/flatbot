using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.HttpClient
{
    public interface ICianHttpClient
    {
        [Obsolete("Используй GetFileAsync")]
        Task<byte[]> GetExcelFromCianAsync(string url);

        Task<string> GetPageAsync(string url);

        void CreateClientWithProxy();

        Task<byte[]> GetFileInBytesAsync(string url);

        Task<Stream> GetFileInStreamAsync(string url);
    }
}
