using System;

namespace Entities.Models.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public ExceptionBase(string message)
            : base(message)
        {

        }
    }
}
