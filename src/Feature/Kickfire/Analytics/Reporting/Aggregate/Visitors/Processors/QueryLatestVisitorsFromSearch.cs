using System;
using System.Data;
using System.Linq;
using Bonfire.Feature.Kickfire.Analytics.Models;
using Sitecore.Analytics.Model;
using Sitecore.Cintel.Configuration;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Aggregate.Visitors;
using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting.Utility;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.Utilities;

namespace Bonfire.Feature.Kickfire.Analytics.Reporting.Aggregate.Visitors.Processors
{
    public class QueryLatestVisitorsFromSearch : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {

            var ctx = ContentSearchManager.GetIndex(CustomerIntelligenceConfig.ContactSearch.SearchIndexName).CreateSearchContext(SearchSecurityOptions.Default);
            try
            {
                var pageSize = args.ReportParameters.PageSize;
                var collection = ctx.GetQueryable<IndexedContact>()
                    .OrderByDescending(r => r.LatestVisitDate)
                    .Skip((args.ReportParameters.PageNumber - 1) * pageSize)
                    .Take(pageSize);

                args.ResultSet.TotalResultCount = ctx.GetQueryable<IndexedContact>().Count();
                collection.ForEach(sr =>
                {
                    var sr1 = sr;
                    var row = args.ResultTableForView.NewRow();
                    BuildBaseResult(row, sr1);
                    args.ResultTableForView.Rows.Add(row);

                    var visit = ctx.GetQueryable<IndexedVisit>()
                        .Where(iv => iv.ContactId == sr1.ContactId)
                        .OrderByDescending(iv => iv.StartDateTime)
                        .Take(1).FirstOrDefault();

                    if (visit == null)
                        return;

                    PopulateLatestVisit(visit, ref row);

                    var company = ctx.GetQueryable<IndexedCompany>()
                        .Where(iv => iv.ContactId == sr1.ContactId)
                        .OrderByDescending(iv => iv.Company);

                    if (company.Any() && company.FirstOrDefault().Company != null)
                        PopulateCompany(company.FirstOrDefault(), ref row);
                    else
                        row["Company"] = "unknown";
                });
            }
            finally
            {
                if (ctx != null)
                    ctx.Dispose();
            }
        }

        private void BuildBaseResult(DataRow row, IndexedContact ic)
        {
            ContactIdentificationLevel result;
            if (!Enum.TryParse(ic.IdentificationLevel, true, out result))
                result = ContactIdentificationLevel.None;
            row[Schema.ContactIdentificationLevel.Name] = result;
            row[Schema.ContactId.Name] = ic.ContactId;
            row[Schema.FirstName.Name] = ic.FirstName;
            row[Schema.MiddleName.Name] = ic.MiddleName;
            row[Schema.Surname.Name] = ic.Surname;
            row[Schema.EmailAddress.Name] = ic.PreferredEmail;
            row[Schema.Value.Name] = ic.Value;
            row[Schema.VisitCount.Name] = ic.VisitCount;
            row[Schema.ValuePerVisit.Name] = Calculator.GetAverageValue(ic.Value, ic.VisitCount);
        }

        private void PopulateLatestVisit(IndexedVisit visit, ref DataRow row)
        {
            row[Schema.LatestVisitValue.Name] = visit.Value;
            row[Schema.LatestVisitStartDateTime.Name] = visit.StartDateTime;
            row[Schema.LatestVisitEndDateTime.Name] = visit.EndDateTime;
            row[Schema.LatestVisitDuration.Name] = Calculator.GetDuration(visit.StartDateTime, visit.EndDateTime);
            if (visit.WhoIs == null)
                return;
            row[Schema.LatestVisitCityDisplayName.Name] = visit.WhoIs.City;
            row[Schema.LatestVisitCountryDisplayName.Name] = visit.WhoIs.Country;
            row[Schema.LatestVisitRegionDisplayName.Name] = visit.WhoIs.Region;
            row["VisitIp"] = visit.WhoIs.Ip;
        }

        private void PopulateCompany(IndexedCompany company, ref DataRow row)
        {
            row["Company"] = company.Company;
        }
    }
}
