using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.ValueObjects
{
    public class Price : ValueObject
    {
        public int Value { get; private set; }

        private Price() { }

        public Price(int value)
        {
            if (value <= 0)
                throw new ArgumentException(nameof(value));

            Value = value;
        }
    }
}
