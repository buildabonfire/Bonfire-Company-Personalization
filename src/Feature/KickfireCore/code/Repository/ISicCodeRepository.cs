using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public interface ISicCodeRepository
    {
        Item GetSicItem(string sicCode);
        Item GetProfileItemBySicCode(string sicCode);
        Item GetGroupParent();
        Item GetGroupOverrideParent();
    }
}