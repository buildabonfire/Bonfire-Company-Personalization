using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public interface ISicCodeOverrideRepository
    {
        Item GetSicCodeFromOverride(string sicCode);
        Item GetGroupOverrideParent();
    }
}