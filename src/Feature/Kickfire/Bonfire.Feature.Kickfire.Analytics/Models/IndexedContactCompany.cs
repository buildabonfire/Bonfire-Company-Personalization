
using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;
using Sitecore.ContentSearch.Linq.Common;

namespace Bonfire.Kickfire.Analytics.Models
{
    [PredefinedQuery("type", ComparisonType.Equal, "contact")]
    public class IndexedContactCompany : AbstractIndexable
    {
        public string Type { get; set; }

        [IndexField("contact.IdentificationLevel")]
        public string IdentificationLevel { get; set; }

        [IndexField("contact.Classification")]
        public int Classification { get; set; }

        [IndexField("contact.VisitCount")]
        public int VisitCount { get; set; }

        [IndexField("contact.Value")]
        public int Value { get; set; }

        [IndexField("contact.ContactId")]
        public Guid ContactId { get; set; }

        [IndexField("contact.Identifier")]
        [Obsolete("Use the Identifier property instead.")]
        public string ExternalUser { get; set; }

        [IndexField("contact.Identifier")]
        public string Identifier { get; set; }

        [IndexField("contact.IntegrationLabel")]
        public string IntegrationLabel { get; set; }

        [IndexField("contact.PreferredEmail")]
        public string PreferredEmail { get; set; }

        [IndexField("contact.Emails")]
        public List<string> Emails { get; set; }

        [IndexField("contact.PreferredAddressKey")]
        public string PreferredAddressKey { get; set; }

        [CollapsedIndexField]
        [IndexField("contact.PreferredAddress")]
        public IndexedContactPreferredAddress PreferredAddress { get; set; }

        [IndexField("contact.company")]
        public string Company { get; set; }

        [IndexField("contact.FirstName")]
        public string FirstName { get; set; }

        [IndexField("contact.MiddleName")]
        public string MiddleName { get; set; }

        [IndexField("contact.Surname")]
        public string Surname { get; set; }

        [IndexField("contact.Title")]
        public string Title { get; set; }

        [IndexField("contact.Suffix")]
        public string Suffix { get; set; }

        [IndexField("contact.Nickname")]
        public string Nickname { get; set; }

        [IndexField("contact.BirthDate")]
        public DateTime BirthDate { get; set; }

        [IndexField("contact.Gender")]
        public string Gender { get; set; }

        [IndexField("contact.JobTitle")]
        public string JobTitle { get; set; }

        [IndexField("contact.FullName")]
        public string FullName { get; set; }

        [IndexField("contact.LatestVisitDate")]
        public DateTime LatestVisitDate { get; set; }

        [IndexField("contact.Tags")]
        public string[] Tags { get; set; }
    }
}
