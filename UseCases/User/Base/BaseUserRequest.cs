using MediatR;

namespace UseCases.User.Base
{
    public class BaseUserRequest : IRequest
    {
        public long ChatId { get; }

        public BaseUserRequest(long chatId)
        {
            ChatId = chatId;
        }
    }

    public class BaseUserRequest<T> : IRequest<T>
    {
        public long ChatId { get; }

        public BaseUserRequest(long chatId)
        {
            ChatId = chatId;
        }
    }
}
