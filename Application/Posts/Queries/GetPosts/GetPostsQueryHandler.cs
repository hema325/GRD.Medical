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
            var query = _context.Posts
                .OrderByDescending(p => p.PostedOn)
                .Include(p => p.Owner)
                .AsQueryable();

            if(request.OwnerId != null)
                query = query.Where(p => p.OwnerId == request.OwnerId);

            if (request.Before != null)
                query = query.Where(p => p.PostedOn < request.Before);

            var posts = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<PostDto>>(posts);
        }
    }
}
