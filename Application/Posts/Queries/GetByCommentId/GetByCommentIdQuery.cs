namespace Application.Posts.Queries.GetByCommentId
{
    public class GetByCommentIdQuery: IRequest<PostDto>
    {
        public int Id { get; set; }
    }
}
