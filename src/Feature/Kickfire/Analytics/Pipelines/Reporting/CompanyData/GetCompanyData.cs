using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting.ReportingServerDatasource;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Reporting.CompanyData
{
    public class GetCompanyData : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            var queryExpression = this.CreateQuery().Build();
            var table = base.GetTableFromContactQueryExpression(queryExpression, args.ReportParameters.ContactId, null);
            args.QueryResult = table;
        }

        protected virtual QueryBuilder CreateQuery()
        {
            var builder = new QueryBuilder
            {
                collectionName = "Contacts"
            };
            builder.Fields.Add("_id");
            builder.Fields.Add("CompanyData_Name");
            builder.Fields.Add("CompanyData_Website");
            builder.Fields.Add("CompanyData_Street");
            builder.Fields.Add("CompanyData_City");

            builder.Fields.Add("CompanyData_Cid");
            builder.Fields.Add("CompanyData_RegionShort");
            builder.Fields.Add("CompanyData_Region");
            builder.Fields.Add("CompanyData_Postal");
            builder.Fields.Add("CompanyData_CountryShort");
            builder.Fields.Add("CompanyData_Country");
            builder.Fields.Add("CompanyData_Phone");
            builder.Fields.Add("CompanyData_Employees");
            builder.Fields.Add("CompanyData_Revenue");
            builder.Fields.Add("CompanyData_Category");
            builder.Fields.Add("CompanyData_SicCode");
            builder.Fields.Add("CompanyData_Latitude");
            builder.Fields.Add("CompanyData_Longitude");
            builder.Fields.Add("CompanyData_StockSymbol");
            builder.Fields.Add("CompanyData_Facebook");
            builder.Fields.Add("CompanyData_Twitter");
            builder.Fields.Add("CompanyData_LinkedIn");
            builder.Fields.Add("CompanyData_LinkedInId");
            builder.Fields.Add("CompanyData_IsIsp");
            builder.Fields.Add("CompanyData_Confidence");

            builder.QueryParms.Add("_id", "@contactid");
            return builder;
        }
    }
}
