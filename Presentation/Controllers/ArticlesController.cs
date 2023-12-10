using Application.Articles.Commands.CreateArticle;
using Application.Articles.Commands.DeleteAritcle;
using Application.Articles.Commands.UpdateArticle;
using Application.Articles.Queries;
using Application.Articles.Queries.GetArticleById;
using Application.Articles.Queries.GetArticles;
using Application.Common.Models;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/articles")]
    public class ArticlesController : ApiControllerBase
    {
        private readonly ISender _sender;
        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> CreateAsync([FromForm]CreateArticleCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateArticleCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DeleteAsync([FromForm] DeleteArticleCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute]GetArticleByIdQuery request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ArticleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetArticlesQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
