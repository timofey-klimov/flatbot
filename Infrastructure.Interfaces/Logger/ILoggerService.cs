namespace Infrastructure.Interfaces.Logger
{
    public interface ILoggerService
    {
         void Error(string message);

         void Info(string message);

         void Debug(string message);
    }
}
