namespace Application.Articles.Commands.DeleteAritcle
{
    public class DeleteArticleCommand : IRequest
    {
        public int Id { get; set; }
    }
}
