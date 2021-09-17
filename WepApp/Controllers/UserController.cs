using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Commands.CreateUser;
using UseCases.User.Commands.SetFlatMinumPrice;
using UseCases.User.Commands.SetMaximumPrice;
using UseCases.User.Commands.SetTimeToMetro;
using UseCases.User.Queries.GetUserProfile;
using WepApp.Dto;

namespace WepApp.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        public IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{chatId}/{price}/minimum-price")]
        public async Task<ApiResponse> SetFlatMinimumPrice(long chatId, decimal price, CancellationToken token)
        {
            await _mediator.Send(new SetFlatMinimumPriceRequest(chatId, price), token);
            return ApiResponse.Success("Created");
        }

        [HttpPost("create/{chatId}/{userName}")]
        public async Task CreateUser(long chatId, string userName, CancellationToken token)
        {
            await _mediator.Send(new CreateUserRequest(chatId, userName), token);
        }

        [HttpPost("{chatId}/{price}/maximum-price")]
        public async Task SetFlatMaximumPrice(long chatId, decimal price, CancellationToken token)
        {
            await _mediator.Send(new SetFlatMaximumPriceRequest(chatId, price), token);
        }

        [HttpPost("{chatId}/{timeToMetro}/set-time-to-metro")]
        public async Task SetTimeToMetro(long chatId, int timeToMetro, CancellationToken token)
        {
            await _mediator.Send(new SetTimeToMetroRequest(chatId, timeToMetro), token);
        }

        [HttpGet("{chatId}/profile")]
        public async Task<ApiResponse<string>> GetUserProfile(long chatId, CancellationToken token)
        {
            var result = await _mediator.Send(new GetUserProfileRequest(chatId), token);

            return ApiResponse<string>.Success(result, string.Empty);
        }
    }
}
