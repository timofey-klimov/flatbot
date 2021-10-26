using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.Dto
{
    public class FindedFlatDto
    {
        public long CianId { get; }

        public string CianUrl { get; }

        public FindedFlatDto(long cianId, string cianUrl)
        {
            CianId = cianId;
            CianUrl = cianUrl;
        }
    }
}
