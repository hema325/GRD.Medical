using Application.Common.Models;

namespace Application.Languages.Queries.GetLanguages
{
    public class GetLanguagesQuery: PaginationBase ,IRequest<PaginatedList<LanguageDto>>
    {
    }
}
