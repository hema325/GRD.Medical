using Application.Common.Models;

namespace Application.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery: PaginationBase, IRequest<PaginatedList<AuthorDto>>
    {
        public string? Name { get; set; }
    }
}
