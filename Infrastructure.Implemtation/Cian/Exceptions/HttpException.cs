using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class HttpException : ExceptionBase
    {
        public HttpException(string message)
            : base(message)
        {

        }
    }
}
