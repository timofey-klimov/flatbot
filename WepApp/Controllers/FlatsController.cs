using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UseCases.Flats.Queries;
using WepApp.Dto;

namespace WepApp.Controllers
{
    [Route("api/flats")]
    public class FlatsController : ControllerBase
    {
        private IMediator _mediator;

        public FlatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{chatId}")]
        public async Task<ApiResponse<string>> GetFlats(long chatId)
        {
            var result = await _mediator.Send(new GetFlatsRquest(chatId));

            return ApiResponse<string>.Success(result);
        }
    }
}
