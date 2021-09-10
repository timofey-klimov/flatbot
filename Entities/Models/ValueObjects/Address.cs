using System;
using Utils;

namespace Entities.Models.ValueObjects
{
    public class Address : ValueObject
    {
        public string Value { get; private set; }
        private Address() { }

        public Address(string value)
        {
            if (value.IsEmpty())
                throw new ArgumentException(nameof(value));

            Value = value;
        }
    }
}
