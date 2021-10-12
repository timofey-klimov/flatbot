using Entities.Models.Exceptions;

namespace WepApp.HostedServices.Exceptions
{
    public class JobManagerNotFoundException : ExceptionBase
    {
        public JobManagerNotFoundException(string message)
            : base(message)
        {

        }
    }
}
