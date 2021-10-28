using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UseCases.Links.Queries.GetFlatSource;
using UseCases.Links.Queries.GetFlatSource.Dto;
using WepApp.Controllers.Base;
using WepApp.Dto;

namespace WepApp.Controllers
{
    [Route("api/link")]
    public class LinkController : BaseApiController
    {
        public LinkController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet("{flatId}/{chatId}")]
        public async Task<ApiResponse<SourceUrlDto>> GetFlatSource(int flatId, long chatId)
        {
            return Ok(await Mediator.Send(new GetFlatSourceRequest(chatId, flatId)));
        }
    }
}
