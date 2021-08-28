using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities.Models.ValueObjects
{
    public class Address : ValueObject
    {
        public string City { get; private set; }

        public string Street { get; private set; }

        public string HouseNumber { get; private set; }

        private Address() { }

        public Address(string city, string street, string houseNumber)
        {
            if (city.IsEmpty())
                throw new ArgumentException(nameof(city));

            if (street.IsEmpty())
                throw new ArgumentException(nameof(street));

            if (houseNumber.IsEmpty())
                throw new ArgumentException(nameof(houseNumber));

            City = city;
            Street = street;
            HouseNumber = houseNumber;
        }
    }
}
