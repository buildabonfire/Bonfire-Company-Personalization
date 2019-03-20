using Bonfire.Feature.KickfireCore.Models.Facets;
using Sitecore.Rules;

namespace Bonfire.Feature.KickfireCore.Rules.Conditions
{
    public class CompanyNameCondition<T> : CompanyFacetCondition<T> where T : RuleContext
    {
        protected override string GetVisitStringValue(CompanyFacet facet)
        {
            return facet.Name;
        }
    }
}