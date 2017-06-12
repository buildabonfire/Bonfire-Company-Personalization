using System;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;
using Sitecore.ContentSearch.Linq.Common;

namespace Bonfire.Feature.Kickfire.Analytics.Models
{
    [PredefinedQuery("type", ComparisonType.Equal, "contact")]
    public class IndexedCompany : AbstractIndexable
    {
        public string Type { get; set; }

        [IndexField("contact.company")]
        public string Company { get; set; }

        [IndexField("visit.StartDateTime")]
        public DateTime StartDateTime { get; set; }

        [IndexField("contact.ContactId")]
        public Guid ContactId { get; set; }

        [CollapsedIndexField]
        public WhoIsResultItem WhoIs { get; set; }

        [CollapsedIndexField]
        public BrowserResultItem Browser { get; set; }

        [IndexField("os")]
        [CollapsedIndexField]
        public OperatingSystemResultItem OperatingSystem { get; set; }

        public IndexedCompany()
        {
            this.OperatingSystem = new OperatingSystemResultItem();
            this.Browser = new BrowserResultItem();
            this.WhoIs = new WhoIsResultItem();
        }
    }
}