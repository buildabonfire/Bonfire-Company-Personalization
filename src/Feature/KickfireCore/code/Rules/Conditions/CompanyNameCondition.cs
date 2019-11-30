using Bonfire.Feature.KickfireCore.Models.Facets;
using Sitecore.Diagnostics;
using Sitecore.Rules;

namespace Bonfire.Feature.KickfireCore.Rules.Conditions
{
    public class CompanyNameCondition<T> : CompanyFacetCondition<T> where T : RuleContext
    {
        public string Value { get; set; }

        protected override (string, string) GetVisitStringValue(CompanyFacet facet)
        {
            return (facet.Name, Value);
        }
    }
}