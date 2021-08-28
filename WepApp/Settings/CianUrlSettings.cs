using System.Collections.Generic;

namespace WepApp.Settings
{
    public class CianUrlSettings
    {
        public ICollection<Map> Maps { get; set; }
    }


    public class Map
    {
        public string City { get; set; }

        public string Url { get; set; }

        public int Region { get; set; }
    }
}
