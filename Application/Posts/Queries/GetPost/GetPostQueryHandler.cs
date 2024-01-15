using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetPost
{
    internal class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == request.Id);

            if (post == null)
                throw new NotFoundException(nameof(Post));

            return _mapper.Map<PostDto>(post);
        }
    }
}
