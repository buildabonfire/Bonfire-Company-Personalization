

namespace Bonfire.Feature.KickfireCore.Services
{
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using System.Collections.Generic;
    using Models.Facets;
    using Foundation.XConnectService.Repositories;

    public class CompanyConnectService
    {
        private readonly ContactIdentificationRepository _contactIdentificationRepository;

        public CompanyConnectService()
        {
            _contactIdentificationRepository = new ContactIdentificationRepository();
        }

        public void UpdateCompanyFacet(CompanyFacet model)
        {
            var contactReference = _contactIdentificationRepository.GetContactReference();

            using (var client = _contactIdentificationRepository.CreateContext())
            {
                // we can have 1 to many facets
                // PersonalInformation.DefaultFacetKey
                // EmailAddressList.DefaultFacetKey
                // Avatar.DefaultFacetKey
                // PhoneNumberList.DefaultFacetKey
                // AddressList.DefaultFacetKey
                // plus custom ones
                var facets = new List<string> { CompanyFacet.DefaultFacetKey };

                // get the contact
                var contact = client.Get(contactReference, new ContactExpandOptions(facets.ToArray()));

                // pull the facet from the contact (if it exists)
                var facet = contact.GetFacet<CompanyFacet>(CompanyFacet.DefaultFacetKey);

                // if it exists, change it, else make a new one
                if (facet != null)
                {
                    facet = model;

                    // set the facet on the client connection
                    client.SetFacet(contact, CompanyFacet.DefaultFacetKey, facet);
                }
                else
                {
                    // make a new one
                    facet = model;

                    // set the facet on the client connection
                    client.SetFacet(contact, CompanyFacet.DefaultFacetKey, facet);
                }

                // submit the changes to xConnect
                client.Submit();

                // reset the contact
                _contactIdentificationRepository.Manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                Sitecore.Analytics.Tracker.Current.Session.Contact = _contactIdentificationRepository.Manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                
            }
        }

    }
}