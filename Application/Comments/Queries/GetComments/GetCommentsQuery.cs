using Application.Common.Models;

namespace Application.Comments.Queries.GetComments
{
    public class GetCommentsQuery: PaginationBase, IRequest<PaginatedList<CommentDto>>
    {
        public int? ReplyTo { get; set; }
        public int PostId { get; set; }
        public DateTime? Before { get; set; }
    }
}
