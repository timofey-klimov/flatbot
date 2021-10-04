using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Distincts.Commands.UpdateUsersDistincts;
using UseCases.District.Commands.RemoveUserDistrict;
using UseCases.District.Dto;
using UseCases.District.Queries;
using UseCases.District.Queries.GetDistricts;
using WepApp.Controllers.Base;
using WepApp.Dto;
using WepApp.Dto.Request;

namespace WepApp.Controllers
{
    [Route("api/districts")]
    public class DistrictController : BaseApiController
    {
        private readonly IMediator _mediator;

        public DistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add/{chatId}")]
        public async Task<ApiResponse> AddUserDistrict(long chatId, [FromBody] DistrictDto districtDto, CancellationToken token)
        {
            await _mediator.Send(new AddUserDistrictRequest(chatId, districtDto.Name), token);

            return Ok();
        }

        [HttpPost("remove/{chatId}")]
        public async Task<ApiResponse> RemoveUserDistrict(long chatId, [FromBody] DistrictDto districtDto, CancellationToken token)
        {
            await _mediator.Send(new RemoveUserDictrictRequest(chatId, districtDto.Name), token);

            return Ok();
        }

        [HttpGet("{chatId}")]
        public async Task<ApiResponse<ICollection<DistrictMenuDto>>> GetUsersDistricts(long chatId)
        {
            var result = await _mediator.Send(new GetUsersDistrictsRequest(chatId));

            return Ok(result);
        }

        [HttpGet]
        public async Task<ApiResponse<ICollection<DistrictDto>>> GetDistricts()
        {
            var result = await _mediator.Send(new GetDistrictsRequest());

            return Ok(result);
        }
    }
}
