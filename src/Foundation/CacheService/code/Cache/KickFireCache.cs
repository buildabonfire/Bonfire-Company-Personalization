using Sitecore.Caching;

namespace Bonfire.Foundation.Kickfire.CacheService.Cache
{
    public class KickFireCache : CustomCache
    {
        static readonly object CacheLock = new object();

        public KickFireCache(long maxSize): base("BonFire.KickStart", maxSize)
        {
        }

        public object Get(string cacheKey)
        {
            return !InnerCache.ContainsKey(cacheKey) ? null : InnerCache.GetValue(cacheKey);
        }

        public void Set(string cacheKey, object value)
        {
            lock (CacheLock)
            {
                InnerCache.Add(cacheKey, value);
            }
        }
    }
}
