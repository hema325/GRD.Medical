using Application.Articles.Commands.CreateArticle;
using Application.Articles.Commands.DeleteAritcle;
using Application.Articles.Commands.UpdateArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/article")]
    public class ArticlesController : ApiControllerBase
    {
        private readonly ISender _sender;
        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromForm]CreateArticleCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateArticleCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromForm] DeleteArticleCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

    }
}
