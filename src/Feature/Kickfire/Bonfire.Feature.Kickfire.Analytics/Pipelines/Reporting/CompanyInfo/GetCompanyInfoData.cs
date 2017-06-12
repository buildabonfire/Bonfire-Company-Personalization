using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bonfire.Feature.Kickfire.Analytics.Helpers.Reporting;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Bonfire.Feature.Kickfire.Analytics.Models.Readonly;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Reporting.CompanyInfo
{
    public class GetCompanyInfoData : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            IElementDictionary<IElementCustomerLookup> entries = GetElementDictionary(args);

            IList<KeyValuePair<string, IElementCustomerLookup>> companyInfoList =
                new List<KeyValuePair<string, IElementCustomerLookup>>();

            companyInfoList = (from key in entries.Keys
                select
                    new KeyValuePair<string, IElementCustomerLookup>(key,
                        new ReadonlyElementCustomerLookup(entries[key]))
                into kvp
                orderby kvp.Key
                select kvp).ToList();


            var resultTable = CompanyInfoHelper.CreateCompanyDataTable("CompanyInfo");

            foreach (var companies in companyInfoList)
            {
                DataRow dataRow = resultTable.NewRow();
                resultTable.Rows.Add(CompanyInfoHelper.FillCompanyDataTable(dataRow, companies.Value));
            }

            args.QueryResult = resultTable;
        }

        public IElementDictionary<IElementCustomerLookup> GetElementDictionary(ReportProcessorArgs args)
        {
            IContactCustomerLookups facet =
                (IContactCustomerLookups)
                    Sitecore.Cintel.CustomerIntelligenceManager.ContactService.GetFacet(
                        args.ReportParameters.ContactId, "CompanyInformation");

            return facet.Entries;
        }

    }
}
