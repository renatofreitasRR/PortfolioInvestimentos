using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioInvestimentos.Application.Caching.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5),
                SlidingExpiration = TimeSpan.FromSeconds(5),
            };
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) 
            where T : class
        {
            string? cachedValue = await _cache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedValue))
                return null;

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);

            return value;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
            where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value);

            await _cache.SetStringAsync(key, cacheValue, _options, cancellationToken);
        }

    }
}
