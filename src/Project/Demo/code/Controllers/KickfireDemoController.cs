using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using System.Web.Mvc;
using Bonfire.Feature.KickfireCore.Models.Facets;

namespace KickfireDemo.Controllers
{
    public class KickfireDemoController : Controller
    {
        public ActionResult KickfireInfo()
        {
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            var company = new CompanyFacet();

            if (xConnectFacet?.Facets != null)
            {
                company = xConnectFacet.Facets[CompanyFacet.DefaultFacetKey] as CompanyFacet;
            }

            return this.View(company);
        }

    }
}