using Microsoft.Extensions.Caching.Memory;
using Widget.Contracts.Interfaces;

namespace Widget.Services.Cache;

public class WidgetCache<T, TKey> : IWidgetCache<T, TKey>
{
    private readonly MemoryCache _cache = new(new MemoryCacheOptions()
    {
        SizeLimit = 1024
    });

    public async Task<T> GetOrCreateAsync(TKey key, Func<Task<T>> createItem)
    {
        var cacheEntry = _cache.Get<T>(key);
        if (cacheEntry != null) return cacheEntry;

        cacheEntry = await createItem().ConfigureAwait(false);

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSize(1)
            .SetPriority(CacheItemPriority.High)
            .SetSlidingExpiration(TimeSpan.FromSeconds(30))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

        _cache.Set(key, cacheEntry, cacheEntryOptions);

        return cacheEntry;
    }

    public void Remove(object key)
    {
        _cache.Remove(key);
    }
}