using System.Collections.Generic;
using System.Linq;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Pipelines.ContactIndexableLoadFields;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.CompanyIndexableLoadFields
{
    class LoadFields : ContactIndexableLoadFieldsProcessor
    {
        protected override IEnumerable<IIndexableDataField> GetFields(ContactIndexableLoadFieldsPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            var contact = args.Contact;
            Assert.IsNotNull(contact, "contact");

            //Log.Info("Starting company field aggregator",this);
            var list = new List<IIndexableDataField>();

            var value = contact.Facets.FirstOrDefault(kvp => kvp.Value is ICustomerLookup).Value;

            if (value != null)
            {
                var companyInfo = (ICustomerLookup) value;

                list = new List<IIndexableDataField>
                {
                    new IndexableDataField<string>("contact.company", companyInfo.name)
                };
            }

            return list;
        }
    }
}
