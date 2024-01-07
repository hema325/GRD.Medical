using Application.Common.Models;
using Application.Notifications.Queries;
using Application.Notifications.Queries.GetNotifications;
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
    }
}
