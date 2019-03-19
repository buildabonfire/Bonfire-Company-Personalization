using Bonfire.Feature.KickfireCore.Models.Facets;

namespace Bonfire.Feature.KickfireCore.Extensions
{
    public static class ContactExtensions
    {
        public static CompanyFacet Company(this Sitecore.XConnect.Contact c)
        {
            return c.GetFacet<CompanyFacet>(CompanyFacet.DefaultFacetKey);
        }
    }
}