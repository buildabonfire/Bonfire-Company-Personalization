using Bonfire.Feature.KickfireCore.Models.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace Bonfire.Feature.KickfireCore.Rules.Conditions
{
    public abstract class CompanyFacetCondition<T> : StringOperatorCondition<T> where T : RuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull((object)ruleContext, nameof(ruleContext));
            Assert.IsNotNull((object)Tracker.Current, "Tracker.Current is not initialized");

            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            if (xConnectFacet.Facets == null) return false;

            var company = xConnectFacet.Facets[CompanyFacet.DefaultFacetKey] as CompanyFacet;
            if (company == null) return false;

            var stringVal = this.GetVisitStringValue(company);

            return this.Compare(stringVal.Item1, stringVal.Item2);
        }

        protected abstract (string, string) GetVisitStringValue(CompanyFacet company);
    }
}