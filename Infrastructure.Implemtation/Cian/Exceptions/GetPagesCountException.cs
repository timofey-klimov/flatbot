using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class GetPagesCountException : ExceptionBase
    {
        public GetPagesCountException(string message)
            : base(message)
        {

        }
    }
}
