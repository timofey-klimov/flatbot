using Infrastructure.Interfaces.Telegram.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Notifications.Commands.DisableNotifications;
using UseCases.Notifications.Commands.EnableNotifications;
using UseCases.Notifications.Commands.SelectNotificationType;
using UseCases.Notifications.Queries.GetTelegramNotification;
using UseCases.Notifications.Queries.GetTelegramObjectsNotification;
using WepApp.Controllers.Base;
using WepApp.Dto;
using WepApp.Dto.Request;

namespace WepApp.Controllers
{
    [Route("api/notify")]
    public class NotificatonController : BaseApiController
    {
        public NotificatonController(IMediator mediator)
            : base(mediator)
        {
            Mediator = mediator;
        }


        [HttpPut("{chatId}/enable")]
        public async Task<ApiResponse> EnableNotifications(long chatId)
        {
            await Mediator.Send(new EnableNotificationsRequest(chatId));
            return Ok();
        }

        [HttpPut("{chatId}/disable")]
        public async Task<ApiResponse> DisableNotifications(long chatId)
        {
            await Mediator.Send(new DisableNotificationsRequest(chatId));

            return Ok();
        }

        [HttpPost("change-type/{chatId}")]

        public async Task<ApiResponse> UpdateNotificationType(long chatId, [FromBody] UpdateNotificationTypeDto dto)
        {
            await Mediator.Send(new SelectNotificationTypeRequest(chatId, dto.Type));

            return Ok();
        }

        [HttpGet("telegram/{chatId}/messages")]
        public async Task<ApiResponse<string>> GetTelegramMessagesNotifications(long chatId)
        {
            var result = await Mediator.Send(new GetTelegramMessagesNotificationRequest(chatId));

            return Ok(result);
        }

        [HttpGet("telegram/{chatId}/objects")]
        public async Task<ApiResponse<ICollection<NotificationDto>>> GetTelegramObjectsNotifications(long chatId)
        {
            var result = await Mediator.Send(new GetTelegramObjectNotificationRequest(chatId));

            return Ok(result);
        }
    }
}
