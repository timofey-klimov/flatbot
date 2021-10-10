using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Base
{
    public abstract class JsonPropertyEntity : Entity<int>
    {
        public abstract void UpdateJsonEntity();
    }
}
