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
            var comments = await _context.Comments
                .Where(c => c.PostId == request.PostId && c.ReplyTo == request.ReplyTo)
                .OrderByDescending(c => c.CommentedOn)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize);

            ResolveImagesUrls(comments);
            return _mapper.Map<PaginatedList<CommentDto>>(comments);
        }

        private string GetFullUrl(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var scheme = _currentHttpRequest.Scheme;
            var host = _currentHttpRequest.Host;
            return $"{scheme}://{host}/{path}";
        }

        private void ResolveImagesUrls(PaginatedList<CommentDto> comments)
        {
            foreach (var data in comments.Data)
            {
                if(data.Media != null)
                    data.Media.Url = GetFullUrl(data.Media.Url);

                data.Owner.ImageUrl = GetFullUrl(data.Owner.ImageUrl);

                foreach (var rply in data.Replies)
                {
                    if(rply.Media!=null)
                        rply.Media.Url = GetFullUrl(rply.Media?.Url);

                    rply.Owner.ImageUrl = GetFullUrl(rply.Owner.ImageUrl);
                }
            }
        }

    }


}
