using Bonfire.Foundation.Kickfire.Library.Model;
using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public interface ISicCodeOverrideRepository
    {
        SicCodeModel GetSicCodeFromOverride(string sicCode);
        Item GetGroupOverrideParent();
        Item GetSicCodeItemFromOverride(string sicCode);
    }
}