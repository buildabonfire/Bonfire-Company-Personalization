using Sitecore;

namespace Bonfire.Foundation.Kickfire.CacheService.Cache
{
    public static class CacheBuilder
    {
        public static readonly KickFireCache KickFireCache = new KickFireCache(StringUtil.ParseSizeString("200MB"));
    }

}
