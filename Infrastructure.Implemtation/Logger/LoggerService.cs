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
        private Action<string> writeDebug;

        public LoggerService(
            Action<string> writeError,
            Action<string> writeInfo,
            Action<string> writeDebug)
        {
            this.writeError = writeError;
            this.writeInfo = writeInfo;
            this.writeDebug = writeDebug;
        }

        public void Error(Type errorPlace, string message)
        {
            var typedMessage = $"{errorPlace.FullName}: {message}";
            writeError(typedMessage);
        }

        public void Info(Type infoPlace, string message)
        {
            var typedMessage = $"{infoPlace.FullName}: {message}";
            writeInfo(typedMessage);
        }

        public void Debug(Type debugPlace, string message)
        {
            var typedMessage = $"{debugPlace.FullName}: {message}";
            writeDebug(typedMessage);
        }
    }
}
