using Infrastructure.Interfaces.Logger;
using System;

namespace Infrastructure.Implemtation.Logger
{
    /// <summary>
    /// Сделать шину, чтобы не писать логи синхронно
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private Action<string> writeError;
        private Action<string> writeInfo;
        public LoggerService(
            Action<string> writeError,
            Action<string> writeInfo)
        {
            this.writeError = writeError;
            this.writeInfo = writeInfo;
        }

        public void Error(string message)
        {
            writeError(message);
        }

        public void Info(string message)
        {
            writeInfo(message);
        }
    }
}
