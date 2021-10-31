using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Framework.Infrastructure.Services
{
    public class CacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan? _relativeExpiration;

        public CacheService(IMemoryCache memoryCache, TimeSpan? relativeExpiration)
        {
            _memoryCache = memoryCache;
            _relativeExpiration = relativeExpiration;
        }

        public async Task<T> SetAsync<T>(string key, Func<ICacheEntry, Task<T>> factory)
        {
            _ = factory ?? throw new ArgumentNullException(nameof(factory));

            using var entry = _memoryCache.CreateEntry(key);
            var entryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);
            entryOptions.AbsoluteExpirationRelativeToNow = _relativeExpiration;
            entry.SetOptions(entryOptions);
            T value = await factory(entry);
            entry.SetValue(value);

            return value;
        }

        public T Set<T>(string key, Func<ICacheEntry, T> factory)
        {
            using var entry = _memoryCache.CreateEntry(key);
            var entryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);
            entryOptions.AbsoluteExpirationRelativeToNow = _relativeExpiration;
            entry.SetOptions(entryOptions);
            T value = factory(entry);
            entry.SetValue(value);

            return value;
        }
    }
}
