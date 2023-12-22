using Application.Common.Models;

namespace Application.Comments.Queries.GetComments
{
    public class GetCommentsQuery: IRequest<PaginatedList<CommentDto>>
    {
        public int? ReplyTo { get; set; }

        public int PostId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
