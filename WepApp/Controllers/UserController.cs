using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Commands.CreateUser;
using UseCases.User.Commands.SetFlatMinumPrice;
using UseCases.User.Commands.SetMaximumPrice;
using UseCases.User.Commands.SetTimeToMetro;
using UseCases.User.Commands.SetMinimumFloor;
using UseCases.User.Queries.Dto;
using UseCases.User.Queries.GetUser;
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

        [HttpPut("{chatId}/{price}/minimum-price")]
        public async Task<ApiResponse> SetFlatMinimumPrice(long chatId, decimal price, CancellationToken token)
        {
            await _mediator.Send(new SetFlatMinimumPriceRequest(chatId, price), token);
            return ApiResponse.Success();
        }

        [HttpPost("{chatId}/{userName}/create")]
        public async Task<ApiResponse> CreateUser(long chatId, string userName, CancellationToken token)
        {
            await _mediator.Send(new CreateUserRequest(chatId, userName), token);
            return ApiResponse.Success();
        }

        [HttpPut("{chatId}/{price}/maximum-price")]
        public async Task<ApiResponse> SetFlatMaximumPrice(long chatId, decimal price, CancellationToken token)
        {
            await _mediator.Send(new SetFlatMaximumPriceRequest(chatId, price), token);
            return ApiResponse.Success();
        }

        [HttpPut("{chatId}/{number}/minimum-floor")]
        public async Task<ApiResponse> SetMinimumFloor(long chatId, int number, CancellationToken token)
        {
            await _mediator.Send(new SetMinimumFloorRequest(chatId, number), token);
            return ApiResponse.Success();
        }

        [HttpPost("{chatId}/{timeToMetro}/set-time-to-metro")]
        public async Task<ApiResponse> SetTimeToMetro(long chatId, int timeToMetro, CancellationToken token)
        {
            await _mediator.Send(new SetTimeToMetroRequest(chatId, timeToMetro), token);

            return ApiResponse.Success();
        }

        [HttpGet("{chatId}/profile")]
        public async Task<ApiResponse<string>> GetUserProfile(long chatId, CancellationToken token)
        {
            var result = await _mediator.Send(new GetUserProfileRequest(chatId), token);

            return ApiResponse<string>.Success(result, string.Empty);
        }

        [HttpGet("{chatId}")]
        public async Task<ApiResponse<UserDto>> GetUser(long chatId, CancellationToken token)
        {
            var result = await _mediator.Send(new GetUserRequest(chatId), token);

            return ApiResponse<UserDto>.Success(result);
        }
    }
}
