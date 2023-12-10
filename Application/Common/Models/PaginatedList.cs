namespace Application.Common.Models
{
    public class PaginatedList<TData>
    {
        public IReadOnlyCollection<TData> Data { get; }

        public int TotalCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedList(IReadOnlyCollection<TData> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
