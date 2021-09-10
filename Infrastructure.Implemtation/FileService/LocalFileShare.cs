using Infrastructure.Interfaces.FileService;
using Infrastructure.Interfaces.Logger;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.FileService
{
    public class LocalFileShare : IFIleShare
    {
        private ILoggerService _logger;
        public LocalFileShare(ILoggerService loggerService)
        {
            _logger = loggerService;
        }

        public async Task SaveFileAsync(byte[] bytes, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                try
                {
                    await fileStream.WriteAsync(bytes, 0, bytes.Length);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
            }
        }
    }
}
