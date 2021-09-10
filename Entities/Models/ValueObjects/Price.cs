using System;
using System.Linq;
using Utils;

namespace Entities.Models.ValueObjects
{
    public class Price : ValueObject
    {
        public int? Value { get; private set; }

        private Price() { }

        public Price(string value)
        {
            if (value.IsEmpty())
                throw new ArgumentException(nameof(value));

            if (int.TryParse(value.Split('/').FirstOrDefault(), out var result))
            {
                Value = result;
            }
        }
    }
}
