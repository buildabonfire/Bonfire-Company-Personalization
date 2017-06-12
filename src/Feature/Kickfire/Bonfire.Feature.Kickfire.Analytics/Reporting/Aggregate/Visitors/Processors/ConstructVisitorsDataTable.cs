using System.Data;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Aggregate.Visitors;
using Sitecore.Cintel.Reporting.Processors;

namespace Bonfire.Feature.Kickfire.Analytics.Reporting.Aggregate.Visitors.Processors
{
    public class ConstructVisitorsDataTable : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            args.ResultTableForView = new DataTable();
            args.ResultTableForView.Columns.Add(Schema.ContactId.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.ContactIdentificationLevel.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.FirstName.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.MiddleName.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.Surname.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.EmailAddress.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.Value.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.VisitCount.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.ValuePerVisit.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitValue.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitDuration.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitRegionDisplayName.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitCountryDisplayName.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitCityDisplayName.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitBusinessName.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitStartDateTime.ToColumn());
            args.ResultTableForView.Columns.Add(Schema.LatestVisitEndDateTime.ToColumn());
            args.ResultTableForView.Columns.Add(new ViewField<string>("VisitIp").ToColumn());
            args.ResultTableForView.Columns.Add(new ViewField<string>("Company").ToColumn());
        }
    }
}
