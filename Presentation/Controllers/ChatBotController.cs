using Application.Common.Models;
using Application.UserChatBotMessages.Commands.CreateUserChatBotMessage;
using Application.UserChatBotMessages.Queries;
using Application.UserChatBotMessages.Queries.GetUserChatBotMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/chatBot")]
    [Authorize]
    public class ChatBotController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ChatBotController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserChatBotMessageCommandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CreateUserChatBotMessageCommand request)
        {
            return Ok(await _sender.Send(request));
        } 
        
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<UserChatBotMessageDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetUserChatBotMessagesQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
