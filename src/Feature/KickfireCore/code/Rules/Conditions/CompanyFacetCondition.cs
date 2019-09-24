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
        public string Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull((object)ruleContext, nameof(ruleContext));
            Assert.IsNotNull((object)Tracker.Current, "Tracker.Current is not initialized");

            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            if (xConnectFacet.Facets == null) return false;

            var company = xConnectFacet.Facets[CompanyFacet.DefaultFacetKey] as CompanyFacet;
            if (company == null) return false;

            return this.Compare(this.GetVisitStringValue(company), this.GetValue(ruleContext));
        }

        protected virtual string GetValue(T ruleContext)
        {
            return this.Value ?? string.Empty;
        }

        protected abstract string GetVisitStringValue(CompanyFacet company);
    }
}