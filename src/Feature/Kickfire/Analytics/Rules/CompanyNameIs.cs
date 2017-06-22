using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace Bonfire.Feature.Kickfire.Analytics.Rules
{
    [UsedImplicitly]
    class CompanyNameIs<T> : StringOperatorCondition<T>
    where T : RuleContext
    {
        public string Value
        {
            get;
            set;
        }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
            Assert.IsNotNull(Tracker.Current.Session, "Tracker.Current.Session is not initialized");
            Assert.IsNotNull(Tracker.Current.Session.Interaction, "Tracker.Current.Session.Interaction is not initialized");

            var value = this.GetValue(ruleContext);
            var companyName = GetCompanyName();

            return base.Compare(companyName, value);
        }

        protected virtual string GetValue(T ruleContext)
        {
            return this.Value ?? string.Empty;
        }

        protected string GetCompanyName()
        {
            var data = Tracker.Current.Contact.GetFacet<ICustomerLookup>("CompanyData");

            return data?.name ?? string.Empty;
        }
    }
}
