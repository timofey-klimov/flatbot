using System.Threading.Tasks;

namespace Infrastructure.Interfaces.FileService
{
    public interface IFIleShare
    {
        Task SaveFileAsync(byte[] bytes, string path);
    }
}
