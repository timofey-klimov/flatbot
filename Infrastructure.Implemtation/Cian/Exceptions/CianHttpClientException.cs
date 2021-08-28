using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class CianHttpClientException : ExceptionBase
    {
        public CianHttpClientException(string message)
            : base(message)
        {

        }
    }
}
