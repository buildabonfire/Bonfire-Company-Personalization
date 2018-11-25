using System.Collections.Generic;
using System.Globalization;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using DateTime = System.DateTime;

namespace Bonfire.Foundation.XConnectService.Repositories
{
    public class GdprRepository 
    {
        private readonly ContactIdentificationRepository _contactIdentificationRepository;

        public GdprRepository()
        {
            _contactIdentificationRepository = new ContactIdentificationRepository();
        }

        public bool ForgetUser()
        {
            var id = _contactIdentificationRepository.GetContactId();
            if (id == null)
            {
                return false;
            }

            var contactReference = new IdentifiedContactReference(id.Source, id.Identifier);

            using (var client = _contactIdentificationRepository.CreateContext())
            {
                var contact = client.Get(contactReference, new ContactExpandOptions());
                if (contact != null)
                {
                    client.ExecuteRightToBeForgotten(contact);
                    client.Submit();
                }
            }

            return false;
        }

        public void FakeUserInfo()
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
                var facets = new List<string> { PersonalInformation.DefaultFacetKey };

                // get the contact
                var contact = client.Get(contactReference, new ContactExpandOptions(facets.ToArray()));

                // pull the facet from the contact (if it exists)
                var facet = contact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);

                // if it exists, change it, else make a new one
                if (facet != null)
                {
                    facet.FirstName = $"Myrtle-{DateTime.Now.Date.ToString(CultureInfo.InvariantCulture)}";
                    facet.LastName = $"McSitecore-{DateTime.Now.Date.ToString(CultureInfo.InvariantCulture)}";

                    // set the facet on the client connection
                    client.SetFacet(contact, PersonalInformation.DefaultFacetKey, facet);
                }
                else
                {
                    // make a new one
                    var personalInfoFacet = new PersonalInformation()
                    {
                        FirstName = "Myrtle",
                        LastName = "McSitecore"
                    };

                    // set the facet on the client connection
                    client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfoFacet);
                }
                
                if (contact != null)
                {
                    // submit the changes to xConnect
                    client.Submit();

                    // reset the contact
                    _contactIdentificationRepository.Manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                    Sitecore.Analytics.Tracker.Current.Session.Contact = _contactIdentificationRepository.Manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                }
            }
        }


    }
}