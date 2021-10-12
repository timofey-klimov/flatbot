using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.FileManager
{
    public interface ICianFileManager
    {
        Task<byte[]> GetFileAsync(string source);
    }
}
