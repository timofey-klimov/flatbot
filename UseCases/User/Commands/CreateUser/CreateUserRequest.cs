using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserRequest : BaseUserRequest
    {
        public string UserName { get; }

        public CreateUserRequest(long chatId, string userName)
            : base(chatId)
        {
            UserName = userName;
        }
    }
}
