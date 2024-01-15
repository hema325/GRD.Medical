using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetByCommentId
{
    internal class GetByCommentIdQueryHandler : IRequestHandler<GetByCommentIdQuery, PostDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByCommentIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetByCommentIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Comments.Any(c=>c.Id == request.Id));

            if (post == null)
                throw new NotFoundException(nameof(Post));

            return _mapper.Map<PostDto>(post);
        }
    }
}
