

using System;
using Bonfire.Foundation.Kickfire.Library.Extensions;

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

        public void UpdateCompanyDataOnClient(CompanyFacet model)
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
                var facet = contact.GetFacet<CompanyFacet>(CompanyFacet.DefaultFacetKey) ;

                // if it exists, change it, else make a new one
                if (facet != null)
                {
                    UpdateFacet(facet, model);

                    // set the facet on the client connection
                    client.SetFacet(contact, CompanyFacet.DefaultFacetKey, facet);
                }
                else
                {
                    // set the facet on the client connection
                    client.SetFacet(contact, CompanyFacet.DefaultFacetKey, model);
                }

                // submit the changes to xConnect
                client.Submit();

                // reset the contact
                _contactIdentificationRepository.Manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                Sitecore.Analytics.Tracker.Current.Session.Contact = _contactIdentificationRepository.Manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                
            }
        }

        public void AddXconnectGoal(Guid goalId, string userAgent, Guid channelId)
        {
            var contactReference = _contactIdentificationRepository.GetContactReference();

            using (var client = _contactIdentificationRepository.CreateContext())
            {
                // get the contact
                var contact = client.Get(contactReference, new ContactExpandOptions());

                var interaction = new Interaction(contact, InteractionInitiator.Brand, channelId, userAgent);

                var goal = new Goal(goalId, DateTime.UtcNow) {EngagementValue = 20};

                interaction.Events.Add(goal);

                //client.AddContact(contact);
                client.AddInteraction(interaction);

                // submit the changes to xConnect
                client.Submit();

                // reset the contact
                _contactIdentificationRepository.Manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
                Sitecore.Analytics.Tracker.Current.Session.Contact = _contactIdentificationRepository.Manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);

            }
        }

        private static void UpdateFacet(CompanyFacet facet, CompanyFacet model)
        {
            facet.Cid = model.Cid;
            facet.Name = model.Name;
            facet.Website = model.Website;
            facet.Street = model.Street;
            facet.City = model.City;
            facet.RegionShort = model.RegionShort;
            facet.Region = model.Region;
            facet.Postal = model.Postal;
            facet.CountryShort = model.CountryShort;
            facet.Country = model.Country;
            facet.Phone = model.Phone;
            facet.Employees = model.Employees;
            facet.Revenue = model.Revenue;
            facet.Category = model.Category;
            facet.Category2 = model.Category2;
            facet.NaicsCode = model.NaicsCode;
            facet.NaicsGroup = model.NaicsGroup;
            facet.SicCode = model.SicCode;
            facet.Latitude = model.Latitude;
            facet.Longitude = model.Longitude;
            facet.StockSymbol = model.StockSymbol;
            facet.Facebook = model.Facebook;
            facet.Twitter = model.Twitter;
            facet.LinkedIn = model.LinkedIn;
            facet.IsIsp = model.IsIsp;
            facet.IsWifi = model.IsWifi;
            facet.Confidence = model.Confidence;
        }

    }
}