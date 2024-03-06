using Microsoft.Extensions.Caching.Distributed;

namespace PortfolioInvestimentos.Domain.Api.Models
{
    public static class CacheOptions
    {
        public static DistributedCacheEntryOptions DefaultExpiration => new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
        };
    }
}
