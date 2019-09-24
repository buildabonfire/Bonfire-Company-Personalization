using Bonfire.Feature.KickfireCore.Models.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace Bonfire.Feature.KickfireCore.Rules.Conditions
{
    public class FacetPropertyCondition<T> : StringOperatorCondition<T> where T : RuleContext
    {
        public string Value { get; set; }
        public string Property { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull((object)ruleContext, nameof(ruleContext));
            Assert.IsNotNull((object)Tracker.Current, "Tracker.Current is not initialized");

            return ProcessCompanyFacet(ruleContext);
        }

        private bool ProcessCompanyFacet(T ruleContext)
        {
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            if (xConnectFacet.Facets == null) return false;

            var company = xConnectFacet.Facets[CompanyFacet.DefaultFacetKey] as CompanyFacet;
            if (company == null) return false;

            return ProcessFacetProperty(company, ruleContext);
        }

        private bool ProcessFacetProperty(CompanyFacet company, T ruleContext)
        {
            var propertyValue = GetPropValue(company, GetFieldName(Property));
            if (string.IsNullOrEmpty(propertyValue)) return false;

            return this.Compare(propertyValue, this.GetValue(ruleContext));
        }

        protected virtual string GetValue(T ruleContext)
        {
            return this.Value ?? string.Empty;
        }

        public static string GetPropValue(object src, string propName)
        {
            var val = src?.GetType()?.GetProperty(propName)?.GetValue(src, null);
            return val?.ToString() ?? "";
        }

        private string GetFieldName(string id)
        {
            if (!ID.IsID(id)) return string.Empty;

            return Sitecore.Context.Database.GetItem(ID.Parse(id))?[Templates.FieldProperties.Fields.FieldName] ?? string.Empty;
        }
    }
}