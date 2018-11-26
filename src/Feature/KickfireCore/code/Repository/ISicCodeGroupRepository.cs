using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public interface ISicCodeGroupRepository
    {
        Item GetSicGroup(string sicCode);
        Item GetProfileItemBySicCode(string sicCode);
    }
}