using Application.Common.Models;

namespace Application.Posts.Queries.GetPosts
{
    public class GetPostsQuery: IRequest<PaginatedList<PostDto>>
    {
        public int? OwnerId { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
