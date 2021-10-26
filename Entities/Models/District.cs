using Entities.Models.Base;
using System.Collections.Generic;

namespace Entities.Models
{
    public class District : AgregateRoot<int>
    {
        public string Name { get; set; }

        public ICollection<UserContext> UserContexts { get; set; }
    }
}
