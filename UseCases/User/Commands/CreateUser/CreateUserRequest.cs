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

        public string Name { get;  }

        public string Surname { get;  }

        public CreateUserRequest(long chatId, string userName, string name, string surname)
            : base(chatId)
        {
            UserName = userName;
            Name = name;
            Surname = surname;
        }
    }
}
