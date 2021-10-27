using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class FlatGeo : Entity<int>
    {
        public int FlatId { get; private set; }

        public double Longitude { get; private set; }

        public double Latitude { get; private set; }

        private FlatGeo() { }

        public FlatGeo(
            double longitude,
            double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
