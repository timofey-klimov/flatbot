using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class Address : Entity<int>
    {
        public int FlatId { get; private set; }

        public string City { get; private set; }

        public District Okrug { get; private set; }

        public string Raion { get; private set; }

        public string Street { get; private set; }

        public string House { get; private set; }

        private Address() { }

        public Address(
            string city,
            District okrug,
            string raion,
            string street,
            string house)
        {
            City = city;
            Raion = raion;
            Street = street;
            House = house;
            Okrug = okrug;
        }

        public override string ToString()
        {
            return $"{City} р-н {Raion} ул {Street} д{House}";
        }
    }
}
