using System;
using System.Linq;
using Utils;
namespace Entities.Models.ValueObjects
{
    public class Metro : ValueObject
    {
        public string Name { get; private set; }

        public int? TimeToGoInMinutes { get; private set; }

        public bool OnCar { get; private set; }


        private Metro() { }

        public Metro(string name)
        {
            if (name.IsEmpty())
                throw new ArgumentException(nameof(name));

            var objs = name.Split('(');

            var metroName = objs.ElementAtOrDefault(0);

            var info = objs.ElementAtOrDefault(1);

            if (info.Contains("пешком"))
            {
                OnCar = false;
            }
            else
            {
                OnCar = true;
            }

            Name = metroName;

            if (int.TryParse(info.Split(" ").FirstOrDefault(), out var result))
                TimeToGoInMinutes = result;
        }
    }
}
