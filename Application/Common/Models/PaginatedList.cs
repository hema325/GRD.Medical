namespace Application.Common.Models
{
    public class PaginatedList<TData>
    {
        public IReadOnlyCollection<TData> Data { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedList(IReadOnlyCollection<TData> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
