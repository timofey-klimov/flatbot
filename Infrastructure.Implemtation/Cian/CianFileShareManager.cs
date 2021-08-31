using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.FileService;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian
{
    public class CianFileShareManager : ICianFileShareManager
    {
        private readonly IFIleShare _fileShare;
        private readonly ICianStoreManager _cianStoreManager;
        public CianFileShareManager(IFIleShare fIleShare, ICianStoreManager cianStoreManager)
        {
            _fileShare = fIleShare;
            _cianStoreManager = cianStoreManager;
        }

        public async Task<string> SaveFileAsync(byte[] bytes)
        {
            var filePath = $"{_cianStoreManager.GetFilesPath()}/{Guid.NewGuid()}.xlsx";

            await _fileShare.SaveFileAsync(bytes, filePath);

            return filePath;
        }
    }
}
