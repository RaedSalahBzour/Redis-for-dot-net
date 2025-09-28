using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Redis_DistributedSystemCache.Helper;

public static class RedisCacheHelper
{
    public static async Task SetEntryAsync<T>(this IDistributedCache cache, string key,
    T data, TimeSpan? AbsoluteExpireTime = null, TimeSpan? SlidingExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions();

        options.AbsoluteExpirationRelativeToNow = AbsoluteExpireTime;
        options.SlidingExpiration = SlidingExpireTime;

        var jsonData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(key, jsonData, options);
    }
    public static async Task<T?> GetEntryAsync<T>(this IDistributedCache cache, string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData == null)
        {
            return default(T);
        }
        return JsonSerializer.Deserialize<T>(jsonData);
    }
}

