using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class BanIpException : ExceptionBase
    {
        public BanIpException(string message)
            : base(message)
        {

        }
    }
}
