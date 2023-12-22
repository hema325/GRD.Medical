using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetPosts
{
    internal class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PaginatedList<PostDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Posts
                .OrderByDescending(p=>p.PostedOn)
                .Include(p=>p.Owner)
                .PaginateAsync(request.pageNumber, request.pageSize);

            return _mapper.Map<PaginatedList<PostDto>>(posts);
        }
    }
}
