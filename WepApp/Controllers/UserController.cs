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
using UseCases.User.Commands.UpdateUserRoomsCount.AddUserRoomsCount;
using UseCases.User.Commands.UpdateUserRoomsCount.RemoveUserRoomsCount;
using System.Collections.Generic;
using UseCases.User.Queries.GetUserRoomsCountMenu;
using UseCases.User.Commands.UpdateFlatArea;
using UseCases.User.Commands.UpdateBuildingYear;

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

        [HttpPost("roomsCount/{chatId}/{roomsCount}/add")]
        public async Task<ApiResponse> AddUserRoomsCount(long chatId, int roomsCount, CancellationToken token)
        {
            var result = await Mediator.Send(new AddUserRoomsCountRequest(chatId, roomsCount), token);

            return Ok();
        }

        [HttpPost("roomsCount/{chatId}/{roomsCount}/remove")]
        public async Task<ApiResponse> RemoveUserRoomsCount(long chatId, int roomsCount, CancellationToken token)
        {
            var result = await Mediator.Send(new RemoveUserRoomsCountRequest(chatId, roomsCount), token);

            return Ok();
        }

        [HttpGet("roomsCount/{chatId}/menu")]
        public async Task<ApiResponse<ICollection<UserRoomsCountMenuDto>>> GetUserRoomsMenu(long chatId, CancellationToken token)
        {
            var result = await Mediator.Send(new GetUserRoomsCountMenuRequest(chatId), token);

            return Ok(result);
        }

        [HttpPost("flatArea/{chatId}/{flatArea}")]
        public async Task<ApiResponse> UpdateFlatArea(long chatId, int flatArea, CancellationToken token)
        {
            var result = await Mediator.Send(new UpdateFlatAreaqRequest(chatId, flatArea), token);

            return Ok();
        }

        [HttpPost("buildingYear/{chatId}/{buildingYear}")]
        public async Task<ApiResponse> UpdateBuildingYear(long chatId, int buildingYear, CancellationToken token)
        {
            var result = await Mediator.Send(new UpdateBuildingYearRequest(chatId, buildingYear), token);

            return Ok();
        }
    }
}
