using Sitecore.Caching;

namespace Bonfire.Foundation.Kickfire.CacheService.Cache
{
    public class KickFireCache : CustomCache
    {
        public KickFireCache(long maxSize): base("BonFire.KickStart", maxSize)
        {

        }

        public string Get(string cacheKey)
        {
            return GetString(cacheKey);
        }

        public void Set(string cacheKey, string value)
        {
            SetString(cacheKey, value);
        }
    }
}
