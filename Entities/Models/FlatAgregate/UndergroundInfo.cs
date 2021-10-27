using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class UndergroundInfo : Entity<int>
    {
        public int FlatId { get; private set; }

        public string Name { get; private set; }

        public int Time { get; private set; }

        public string Type { get; private set; }

        private UndergroundInfo() { }

        public UndergroundInfo(
            string name,
            int time,
            string type)
        {
            Name = name;
            Time = time;
            Type = type;
        }
    }
}
