using System;

namespace Infrastructure.Interfaces.Logger
{
    public interface ILoggerService
    {
         void Error(Type errorPlace, string message);

         void Info(Type infoPlace, string message);

         void Debug(Type debugPlace, string message);
    }
}
