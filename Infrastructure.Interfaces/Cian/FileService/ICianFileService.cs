using Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.FileService
{
    public interface ICianFileService
    {
        Task<byte[]> GetCianPdfAsync(Flat flat);

        Task<ICollection<Stream>> GetCianFlatImagesAsync(Flat flat);
    }
}
