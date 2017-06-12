// Decompiled with JetBrains decompiler
// Type: Sitecore.Cintel.Client.Transformers.Contact.ContactSearchResultTransformer
// Assembly: Sitecore.Cintel.Client, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C49E8D2D-BDEF-4367-944C-6A3D8658501E
// Assembly location: C:\inetpub\wwwroot\bfsite\Website\bin\Sitecore.Cintel.Client.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Bonfire.Feature.Kickfire.Analytics.Models;
using Sitecore.Analytics.Model;
using Sitecore.Cintel.Client;
using Sitecore.Cintel.Client.Transformers;
using Sitecore.Cintel.Commons;
using Sitecore.Cintel.Configuration;
using Sitecore.Cintel.Endpoint.Transfomers;
using Sitecore.Cintel.Search;
using Sitecore.ContentSearch;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.Kickfire.Analytics.Transformers.Contact
{
    public class ContactSearchResultTransformer : IContactSearchResultTransformer
    {
        private readonly TimeConverter _timeConverter;
        private readonly TextConverter _textConverter;
        private static IProviderSearchContext _ctx;

        public ContactSearchResultTransformer()
        {
            _timeConverter = ClientFactory.Instance.GetTimeConverter();
            _textConverter = ClientFactory.Instance.GetTextConverter();
            _ctx = ContentSearchManager.GetIndex(CustomerIntelligenceConfig.ContactSearch.SearchIndexName).CreateSearchContext();
        }

        public ContactSearchResultTransformer(TimeConverter timeConverter, TextConverter textConverter)
        {
            _timeConverter = timeConverter;
            _textConverter = textConverter;
            _ctx = ContentSearchManager.GetIndex(CustomerIntelligenceConfig.ContactSearch.SearchIndexName).CreateSearchContext();
        }

        public object Transform(ResultSet<List<IContactSearchResult>> resultSet)
        {
            Assert.ArgumentNotNull(resultSet, "resultSet");
            if (resultSet.Data.Dataset.Count == 0)
                return resultSet;
            var source = resultSet.Data.Dataset.FirstOrDefault().Value;
            if (source == null)
                return resultSet;
            var nowTime = DateTime.UtcNow;
            var list = source.Select(r => ExtendResult(r, nowTime)).ToList();
            source.Clear();
            source.AddRange(list);
            return resultSet;
        }

        private ContactSearchResultEx2 ExtendResult(IContactSearchResult result, DateTime nowTime)
        {
            var contactSearchResultEx = new ContactSearchResultEx2(result)
            {
                FormattedLatestVisitStartDateTime = _timeConverter.FormatDateTime(result.LatestVisitStartDateTime),
                Recency = _timeConverter.GetRecency(result.LatestVisitStartDateTime, nowTime),
                LatestVisitLocationDisplayName =
                    _textConverter.GetLocation(result.LatestVisitLocationCityDisplayName,
                        result.LatestVisitLocationRegionDisplayName, result.LatestVisitLocationCountryDisplayName),
                EmailAddressExt = _textConverter.GetEmail(result.PreferredEmail)
            };

            var duration = _timeConverter.GetDuration(result.LatestVisitStartDateTime, result.LatestVisitEndDateTime);
            contactSearchResultEx.LatestDuration = duration;

            contactSearchResultEx.Company = GetContactCompany(contactSearchResultEx);

            var strArray = new[]
            {
                result.FirstName,
                result.MiddleName,
                result.Surname
            };

            var fullName = _textConverter.GetFullName((ContactIdentificationLevel)result.IdentificationLevel, strArray);
            contactSearchResultEx.FullName = fullName;
            return contactSearchResultEx;
        }

        private string GetContactCompany(ContactSearchResultEx2 contactSearchResultEx2)
        {
            var company = _ctx.GetQueryable<IndexedCompany>()
                        .Where(iv => iv.ContactId == contactSearchResultEx2.ContactId)
                        .OrderByDescending(iv => iv.Company);

            var firstOrDefault = company.FirstOrDefault();
            if (firstOrDefault != null && firstOrDefault.Company != null) return firstOrDefault.Company;

            return "unknown";
        }
    }
}
