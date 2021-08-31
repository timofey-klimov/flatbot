using Entities.Models.Exceptions;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class FilePathIsEmptyException : ExceptionBase
    {
        public FilePathIsEmptyException(string message)
            : base(message)
        {

        }
    }
}
