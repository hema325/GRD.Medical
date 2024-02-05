using Application.Common.Models;
using Application.Reviews.Commands.CreateReview;
using Application.Reviews.Queries;
using Application.Reviews.Queries.GetReviews;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/reviews")]
    public class ReviewsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ReviewsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HaveRoles(Roles.Patient)]
        public async Task<IActionResult> CreateAsync(CreateReviewCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ReviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetReviewsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
