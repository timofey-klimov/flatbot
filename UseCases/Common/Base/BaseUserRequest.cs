using MediatR;

namespace UseCases.User.Base
{
    public abstract class BaseUserRequest : IRequest
    {
        public long ChatId { get; }

        public BaseUserRequest(long chatId)
        {
            ChatId = chatId;
        }
    }

    public abstract class BaseUserRequest<T> : IRequest<T>
    {
        public long ChatId { get; }

        public BaseUserRequest(long chatId)
        {
            ChatId = chatId;
        }
    }
}
