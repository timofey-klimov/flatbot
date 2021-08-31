using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class NoSuchDirectoryException : ExceptionBase
    {
        public NoSuchDirectoryException(string message)
            : base(message)
        {

        }
    }
}
