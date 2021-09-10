using System;
using System.Globalization;
using System.Linq;
using Utils;

namespace Entities.Models.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Value { get; private set; }

        private Price() { }

        public Price(string value)
        {
            if (value.IsEmpty())
                throw new ArgumentException(nameof(value));

            var priceInfo = value.Split('/');

            var priceStr = priceInfo.FirstOrDefault().Replace("руб.", "").Trim();

            if (decimal.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                Value = result;
            }
        }
    }
}
