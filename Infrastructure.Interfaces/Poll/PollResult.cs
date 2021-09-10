using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Poll
{
    public class PollResult
    {
        public string Message { get; }

        public bool IsSuccess { get; }

        public PollResult(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public static PollResult Success() => new PollResult(string.Empty, true);
        public static PollResult Fail(string message) => new PollResult(message, false);
    }

    public class PollResult<T> : PollResult
    {
        public T Data { get; }

        public PollResult(T data, string message, bool isSuccess)
            : base(message, isSuccess)
        {
            Data = data;
        }

        public static PollResult<T> Success(T data) => new PollResult<T>(data, string.Empty, true);
        public static PollResult<T> Fail(string message) => new PollResult<T>(default, message, false);
    }
}
