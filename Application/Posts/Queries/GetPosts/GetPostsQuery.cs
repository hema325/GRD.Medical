using Application.Common.Models;

namespace Application.Posts.Queries.GetPosts
{
    public class GetPostsQuery: PaginationBase, IRequest<PaginatedList<PostDto>>
    {
        public int? OwnerId { get; set; }
        public DateTime? Before { get; set; }
    }
}
