using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Bus.Exceptions
{
    public class ServiceNotRegistredException : ExceptionBase
    {
        public ServiceNotRegistredException(string message)
            : base(message)
        {

        }
    }
}
