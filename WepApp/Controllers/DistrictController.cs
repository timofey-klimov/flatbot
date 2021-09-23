using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Distincts.Commands.UpdateUsersDistincts;
using UseCases.District.Dto;
using UseCases.District.Queries;
using WepApp.Dto;
using WepApp.Dto.Request;

namespace WepApp.Controllers
{
    [Route("api/districts")]
    public class DistrictController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add/{chatId}")]
        public async Task<ApiResponse> UpdateUsersDistricts(long chatId, [FromBody] DistrictDto distinctDto, CancellationToken token)
        {
            await _mediator.Send(new UpdateUsersDistrictsRequest(chatId, distinctDto.Name), token);

            return ApiResponse.Success();
        }

        [HttpGet("{chatId}")]
        public async Task<ApiResponse<ICollection<DistinctMenuDto>>> GetUsersDistricts(long chatId)
        {
            var result = await _mediator.Send(new GetUsersDistrictsRequest(chatId));

            return ApiResponse<ICollection<DistinctMenuDto>>.Success(result);
        }
    }
}
