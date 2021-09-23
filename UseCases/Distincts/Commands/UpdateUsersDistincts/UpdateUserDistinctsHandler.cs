using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Distincts.Commands.UpdateUsersDistincts
{
    public class UpdateUserDistinctsHandler : IRequestHandler<UpdateUsersDistinctsRequest>
    {
        public Task<Unit> Handle(UpdateUsersDistinctsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
