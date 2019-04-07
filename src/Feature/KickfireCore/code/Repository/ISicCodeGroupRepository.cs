using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public interface ISicCodeGroupRepository
    {
        Item GetProfileItemBySicCode(string sicCode);
    }
}