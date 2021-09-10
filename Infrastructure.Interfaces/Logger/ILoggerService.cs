namespace Infrastructure.Interfaces.Logger
{
    public interface ILoggerService
    {
        public void Error(string message);

        public void Info(string message);

        public void Debug(string message);
    }
}
