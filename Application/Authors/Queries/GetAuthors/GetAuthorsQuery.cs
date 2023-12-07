using Application.Common.Models;

namespace Application.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery: IRequest<PaginatedList<AuthorDto>>
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
