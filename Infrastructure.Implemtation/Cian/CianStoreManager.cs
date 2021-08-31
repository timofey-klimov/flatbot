using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using System.IO;
using Utils;

namespace Infrastructure.Implemtation.Cian
{
    public class CianStoreManager : ICianStoreManager
    {
        private readonly string _rootFilePath;

        public CianStoreManager(string rootFilePath)
        {
            if (rootFilePath.IsEmpty())
                throw new FilePathIsEmptyException(rootFilePath);

            if (!Directory.Exists(rootFilePath))
                throw new NoSuchDirectoryException(rootFilePath);

            _rootFilePath = rootFilePath;
        }

        public string GetFilesPath()
        {
            return _rootFilePath;
        }
    }
}
