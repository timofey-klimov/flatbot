using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Distincts.Commands.UpdateUsersDistincts;
using WepApp.Dto;
using WepApp.Dto.Request;

namespace WepApp.Controllers
{
    [Route("api/dictinct")]
    public class DistinctController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DistinctController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add/{chatId}")]
        public async Task<ApiResponse> UpdateUsersDistincts(long chatId, [FromBody] DistinctDto distinctDto, CancellationToken token)
        {
            await _mediator.Send(new UpdateUsersDistinctsRequest(chatId, distinctDto.Name), token);

            return ApiResponse.Success();
        }
    }
}
