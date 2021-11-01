using Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Base
{
    public class EntityNotFoundExceptionBase : ExceptionBase
    {
        public EntityNotFoundExceptionBase(string message) : base(message)
        {
        }
    }
}
