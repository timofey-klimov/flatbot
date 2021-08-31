using Infrastructure.Interfaces.FileService;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.FileService
{
    public class LocalFileShare : IFIleShare
    {
        public async Task SaveFileAsync(byte[] bytes, string path)
        {
            using (var fileStream = new FileStream(path,FileMode.CreateNew, FileAccess.ReadWrite))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
