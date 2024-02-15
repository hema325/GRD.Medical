using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Common.Extensions
{
    public static class IDistributedCacheExtensions
    {
        public static async Task SetAsync<TType>(this IDistributedCache cache,string key, TType obj, DistributedCacheEntryOptions? options = null) where TType : class
        {
            var jsonStr = JsonSerializer.Serialize(obj);
            
            if (options != null)
                await cache.SetStringAsync(key, jsonStr, options);

            await cache.SetStringAsync(key, jsonStr);
        }

        public static async Task<TType?> GetAsync<TType>(this IDistributedCache cache, string key) where TType : class
        {
            var jsonStr = await cache.GetStringAsync(key);

            if (jsonStr != null)
                return JsonSerializer.Deserialize<TType>(jsonStr);

            return null;
        }
    }
}
