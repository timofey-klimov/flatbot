using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Exceptions
{
    public class DomainNullRefException : ExceptionBase
    {
        public DomainNullRefException(object ob)
            : base($"{ob.GetType().Name} is null")
        {

        }
    }
}
