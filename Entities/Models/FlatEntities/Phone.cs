using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class Phone : Entity<int>
    {
        public int FlatId { get; private set; }

        public string Number { get; private set; }

        private Phone() { }

        public Phone(string number)
        {
            Number = number;
        }
    }
}
