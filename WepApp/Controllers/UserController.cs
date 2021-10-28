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
using WepApp.Dto.Request;
using UseCases.User.Commands.ChangeUserState;
using WepApp.Controllers.Base;
using UseCases.User.Queries.GetUserState;
using UseCases.User.Dto;

namespace WepApp.Controllers
{
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator)
            : base(mediator)
        {
            Mediator = mediator;
        }

        [HttpPut("{chatId}/{price}/minimum-price")]
        public async Task<ApiResponse> SetFlatMinimumPrice(long chatId, decimal price, CancellationToken token)
        {
            await Mediator.Send(new SetFlatMinimumPriceRequest(chatId, price), token);
            return Ok();
        }

        [HttpPost("{chatId}/create")]
        public async Task<ApiResponse> CreateUser(long chatId, [FromBody] CreateUserDto dto, CancellationToken token)
        {
            await Mediator.Send(new CreateUserRequest(chatId, dto.Username, dto.Name, dto.Surname), token);
            return Ok();
        }

        [HttpPut("{chatId}/{price}/maximum-price")]
        public async Task<ApiResponse> SetFlatMaximumPrice(long chatId, decimal price, CancellationToken token)
        {
            await Mediator.Send(new SetFlatMaximumPriceRequest(chatId, price), token);
            return Ok();
        }

        [HttpPut("{chatId}/{number}/minimum-floor")]
        public async Task<ApiResponse> SetMinimumFloor(long chatId, int number, CancellationToken token)
        {
            await Mediator.Send(new SetMinimumFloorRequest(chatId, number), token);
            return Ok();
        }

        [HttpPost("{chatId}/{timeToMetro}/set-time-to-metro")]
        public async Task<ApiResponse> SetTimeToMetro(long chatId, int timeToMetro, CancellationToken token)
        {
            await Mediator.Send(new SetTimeToMetroRequest(chatId, timeToMetro), token);

            return Ok();
        }

        [HttpGet("{chatId}/profile")]
        public async Task<ApiResponse<string>> GetUserProfile(long chatId, CancellationToken token)
        {
            var result = await Mediator.Send(new GetUserProfileRequest(chatId), token);

            return Ok(result);
        }

        [HttpGet("{chatId}")]
        public async Task<ApiResponse<UserDto>> GetUser(long chatId, CancellationToken token)
        {
            var result = await Mediator.Send(new GetUserRequest(chatId), token);

            return Ok(result);
        }

        [HttpPost("state/{chatId}/change")]
        public async Task<ApiResponse> ChangeState([FromBody] UserStateDto userStateDto, long chatId, CancellationToken token)
        {
            var result = await Mediator.Send(new ChangeUserStateRequest(chatId, userStateDto.UserState), token);

            return Ok();
        }

        [HttpGet("state/{chatId}/get")]
        public async Task<ApiResponse<UserStates>> GetUserState(long chatId, CancellationToken token)
        {
            var result = await Mediator.Send(new GetUserStateRequest(chatId), token);
            return Ok(result);
        }
    }
}
