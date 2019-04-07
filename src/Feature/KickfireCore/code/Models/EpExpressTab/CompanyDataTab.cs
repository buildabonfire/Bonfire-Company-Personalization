namespace Bonfire.Feature.KickfireCore.Models.EpExpressTab
{
    using System;
    using Facets;
    using EPExpressTab.Data;
    using EPExpressTab.Repositories;

    public class CompanyDataTab : EpExpressViewModel
    {
        public override string Heading => "Company Data";
        public override string TabLabel => "Company Data";
        public override object GetModel(Guid contactId)
        {
            var contact = EPRepository.GetContact(contactId, CompanyFacet.DefaultFacetKey);

            return contact.GetFacet<CompanyFacet>();
        }
        public override string GetFullViewPath(object model)
        {
            return "/sitecore modules/Kickfire/Views/CompanyData.cshtml";
        }
    }
}