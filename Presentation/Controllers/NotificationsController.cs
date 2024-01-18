using Application.Common.Models;
using Application.Notifications.Commands.MarkAsRead;
using Application.Notifications.Queries;
using Application.Notifications.Queries.GetNotifications;
using Application.Notifications.Queries.GetUnReadNotificationCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/notifications")]
    [Authorize]
    public class NotificationsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public NotificationsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<NotificationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetNotificationsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpGet("unReadNotificationsCount")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnReadNotificationsCount()
        {
            return Ok(await _sender.Send(new GetUnReadNotificationCountQuery()));
        }

        [HttpPost("markAsRead")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> markAsRead(MarkNotificationAsReadCommand request)
        {
            return Ok(await _sender.Send(request));
        }

    }
}
