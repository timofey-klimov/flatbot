using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class BuildingInfo : Entity<int>
    {
        public int FlatId { get; private set; }

        public int? BuildYear { get; private set; }

        public string Type { get; private set; }

        private BuildingInfo() { }

        public BuildingInfo(int? buildYear, string type)
        {
            BuildYear = buildYear;
            Type = type;
        }
    }
}
