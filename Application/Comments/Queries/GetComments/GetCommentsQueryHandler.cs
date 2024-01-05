using Application.Common.Extensions;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;

namespace Application.Comments.Queries.GetComments
{
    internal class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, PaginatedList<CommentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentHttpRequest _currentHttpRequest;

        public GetCommentsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentHttpRequest currentHttpRequest)
        {
            _context = context;
            _mapper = mapper;
            _currentHttpRequest = currentHttpRequest;
        }

        public async Task<PaginatedList<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Comments
                .Where(c => c.PostId == request.PostId && c.ReplyTo == request.ReplyTo)
                .AsQueryable();

            if (request.ReplyTo == null)
                query = query.OrderByDescending(c => c.CommentedOn);
            else
                query = query.OrderBy(c => c.CommentedOn);

            if (request.Before != null)
                query = query.Where(c => c.CommentedOn < request.Before);

            var comments = await query.ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize);

            ResolveImagesUrls(comments);
            return _mapper.Map<PaginatedList<CommentDto>>(comments);
        }

        private void ResolveImagesUrls(PaginatedList<CommentDto> comments)
        {
            foreach (var data in comments.Data)
            {
                if(data.Media != null)
                    data.Media.Url = MediaHelpers.GetFullUrl(_currentHttpRequest, data.Media.Url);

                data.Owner.ImageUrl = MediaHelpers.GetFullUrl(_currentHttpRequest, data.Owner.ImageUrl);

                foreach (var rply in data.Replies)
                {
                    if(rply.Media!=null)
                        rply.Media.Url = MediaHelpers.GetFullUrl(_currentHttpRequest, rply.Media?.Url);

                    rply.Owner.ImageUrl = MediaHelpers.GetFullUrl(_currentHttpRequest, rply.Owner.ImageUrl);
                }
            }
        }

    }
}
