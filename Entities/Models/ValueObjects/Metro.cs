using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
namespace Entities.Models.ValueObjects
{
    public class Metro : ValueObject
    {
        public string Name { get; private set; }

        public int? TimeToGo { get; private set; }

        public bool OnCar { get; private set; }


        private Metro() { }

        public Metro(string name, int? timeToGo, bool onCar)
        {
            if (name.IsEmpty())
                throw new ArgumentException(nameof(name));

            if (timeToGo <= 0)
                throw new ArgumentException(nameof(timeToGo));

            Name = name;
            TimeToGo = timeToGo;
            OnCar = onCar;
        }
    }
}
