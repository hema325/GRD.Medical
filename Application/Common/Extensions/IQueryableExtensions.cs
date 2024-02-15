using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<TDest>> PaginateAsync<TDest>(this IQueryable<TDest> query, int pageNumber, int pageSize) where TDest: class
        {
            var data = await query.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PaginatedList<TDest>(data, count, pageNumber, pageSize);
        }
    }
}
