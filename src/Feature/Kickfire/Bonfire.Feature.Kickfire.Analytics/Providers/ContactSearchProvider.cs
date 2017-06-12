using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Analytics.Model;
using Sitecore.Cintel;
using Sitecore.Cintel.Commons;
using Sitecore.Cintel.Configuration;
using Sitecore.Cintel.Reporting.Utility;
using Sitecore.Cintel.Search;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;
using Sitecore.ContentSearch.Linq;

namespace Bonfire.Feature.Kickfire.Analytics.Providers
{
    public class ContactSearchProvider : IContactSearchProvider
    {
        public ResultSet<List<IContactSearchResult>> Find(ContactSearchParameters parameters)
        {
            var resultSet = new ResultSet<List<IContactSearchResult>>(parameters.PageNumber, parameters.PageSize);
            var ctx = ContentSearchManager.GetIndex(CustomerIntelligenceConfig.ContactSearch.SearchIndexName).CreateSearchContext();
            try
            {
                var searchResults = QueryIndex(ctx, parameters);
                var list1 = searchResults.Hits.Select(h => h.Document).ToList();
                resultSet.TotalResultCount = searchResults.TotalSearchResults;
                var list2 = list1.Select(sr =>
                {
                    var contact = BuildBaseResult(sr);
                    var visit = ctx.GetQueryable<IndexedVisit>().Where(iv => iv.ContactId == contact.ContactId).OrderByDescending(iv => iv.StartDateTime).Take(1).FirstOrDefault();
                    if (visit != null)
                        PopulateLatestVisit(visit, ref contact);
                    return contact;
                }).OrderBy(c => c.FirstName).ThenBy(c => c.LatestVisitStartDateTime).ToList();
                resultSet.Data.Dataset.Add("ContactSearchResults", list2);
            }
            finally
            {
                if (ctx != null)
                    ctx.Dispose();
            }
            return resultSet;
        }

        private SearchResults<IndexedContact> QueryIndex(IProviderSearchContext ctx, ContactSearchParameters parameters)
        {
            var queryable = ctx.GetQueryable<IndexedContact>();
            var text = parameters.Match;
            if (string.IsNullOrEmpty(text.Trim()) || text == "*")
                return queryable.Page(parameters.PageNumber - 1, parameters.PageSize).GetResults();
            var wildcard = "*" + text + "*";
            var slop = 10;
            var source = queryable.Where(q => q.FullName.MatchWildcard(wildcard) || q.Emails.MatchWildcard(wildcard) || q["contact.company"].MatchWildcard(wildcard));
            if (!source.Any())
                source = queryable.Where(q => q.FullName.Like(text, slop) || q.Emails.Like(text, slop) || q["contact.company"].Like(text, slop));
            return source.Page(parameters.PageNumber - 1, parameters.PageSize).GetResults();
        }

        private IContactSearchResult BuildBaseResult(IndexedContact indexedContact)
        {
            ContactIdentificationLevel result;
            if (!Enum.TryParse(indexedContact.IdentificationLevel, true, out result))
                result = ContactIdentificationLevel.None;
            return new ContactSearchResult()
            {
                IdentificationLevel = (int)result,
                ContactId = indexedContact.ContactId,
                FirstName = indexedContact.FirstName,
                MiddleName = indexedContact.MiddleName,
                Surname = indexedContact.Surname,
                PreferredEmail = indexedContact.PreferredEmail,
                JobTitle = indexedContact.JobTitle,
                Value = indexedContact.Value,
                VisitCount = indexedContact.VisitCount
            };
        }

        private void PopulateLatestVisit(IndexedVisit visit, ref IContactSearchResult contact)
        {
            contact.LatestVisitId = visit.InteractionId;
            contact.LatestVisitStartDateTime = visit.StartDateTime;
            contact.LatestVisitEndDateTime = visit.EndDateTime;
            contact.LatestVisitPageViewCount = visit.VisitPageCount;
            contact.LatestVisitValue = visit.Value;
            contact.ValuePerVisit = Calculator.GetAverageValue(contact.Value, contact.VisitCount);
            if (visit.WhoIs == null)
                return;
            contact.LatestVisitLocationCityDisplayName = visit.WhoIs.City;
            contact.LatestVisitLocationCountryDisplayName = visit.WhoIs.Country;
            contact.LatestVisitLocationRegionDisplayName = visit.WhoIs.Region;
            contact.LatestVisitLocationId = visit.LocationId;
        }
    }
}
