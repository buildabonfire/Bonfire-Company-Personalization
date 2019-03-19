using Bonfire.Feature.KickfireCore.Models.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Data;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace Bonfire.Feature.KickfireCore.Rules.Conditions
{
    public class EmployeeCountCondition<T> : WhenCondition<T> where T : RuleContext
    {
        public string EmployeeCount { get; set; }

        protected override bool Execute(T ruleContext)
        {
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            var company = xConnectFacet.Facets[CompanyFacet.DefaultFacetKey] as CompanyFacet;

            if (company == null) return false;

            return company.Employees == GetEmployeeCountRange(EmployeeCount);
        }

        private string GetEmployeeCountRange(string id)
        {
            if (!ID.IsID(id)) return string.Empty;

            return Sitecore.Context.Database.GetItem(ID.Parse(id))?[Templates.Employees.Fields.Range] ?? string.Empty;
        }
    }
}