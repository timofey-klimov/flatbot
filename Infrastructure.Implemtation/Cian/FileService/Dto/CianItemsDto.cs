using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.FileService.Dto
{
    public class CianItemsDto
    {
        public byte[] Pdf { get; set; }

        public ICollection<Stream> Images { get; set; }
    }
}
