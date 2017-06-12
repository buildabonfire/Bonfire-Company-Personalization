using Bonfire.Feature.Kickfire.Analytics.Helpers.Reporting;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Reporting
{
    public class ConstructCompanyDataTable : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            args.ResultTableForView = CompanyInfoHelper.CreateCompanyDataTable("CompanyInfo");
        }
    }
}
