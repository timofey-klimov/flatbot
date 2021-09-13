using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Proxy : Entity<int>
    {
        public string Ip { get; set; }

        public int Port { get; set; }
    }
}
