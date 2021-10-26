using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class PhotoInfo : Entity<int>
    {
        public int FlatId { get; private set; }

        public string Url { get; private set; }

        private PhotoInfo() { }

        public PhotoInfo(string url)
        {
            Url = url;
        }
    }
}
