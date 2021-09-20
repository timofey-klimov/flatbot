using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UseCases.Notifications.Commands.DisableNotifications;
using UseCases.Notifications.Commands.EnableNotifications;
using UseCases.Notifications.Commands.SelectNotificationType;
using WepApp.Dto;
using WepApp.Dto.Request;

namespace WepApp.Controllers
{
    [Route("api/notify")]
    public class NotificatonController : ControllerBase
    {
        private IMediator _mediator;
        public NotificatonController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPut("{chatId}/enable")]
        public async Task<ApiResponse> EnableNotifications(long chatId)
        {
            await _mediator.Send(new EnableNotificationsRequest(chatId));
            return ApiResponse.Success();
        }

        [HttpPut("{chatId}/disable")]
        public async Task<ApiResponse> DisableNotifications(long chatId)
        {
            await _mediator.Send(new DisableNotificationsRequest(chatId));

            return ApiResponse.Success();
        }

        [HttpPut("default-type")]
        public async Task<ApiResponse> SelectDefaultTypeNotification([FromBody] SelectNotificationTypeDto dto)
        {
            await _mediator.Send(new SelectNotificationTypeRequest(dto.ChatId, dto.Type));

            return ApiResponse.Success();
        }

        [HttpPut("every-day")]
        public async Task<ApiResponse> SelectEveryDayTypeNotification([FromBody] SelectNotificationTypeDto dto)
        {
            await _mediator.Send(new SelectNotificationTypeRequest(dto.ChatId, dto.Type));

            return ApiResponse.Success();
        }

        [HttpPut("every-week")]
        public async Task<ApiResponse> SelectEveryWeekTypeNotification([FromBody] SelectNotificationTypeDto dto)
        {
            await _mediator.Send(new SelectNotificationTypeRequest(dto.ChatId, dto.Type));

            return ApiResponse.Success();
        }
    }
}
