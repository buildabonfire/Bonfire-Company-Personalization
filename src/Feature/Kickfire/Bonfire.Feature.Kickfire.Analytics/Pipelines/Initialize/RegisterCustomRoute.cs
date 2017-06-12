using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Initialize
{
    public class InitRoutes : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
    {
        private const string BASE_ROUTE = "apis";
        private const string API_VERSION = "/v1";

        public override void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute(
                "VisitorData", // Route name
                BASE_ROUTE + API_VERSION + "/VisitorDetails",
                new {controller = "Visitor", action = "VisitorDetailsJSON"},
                new[] {"Bonfire.Feature.Kickfire.Analytics.Controllers"});

            RouteTable.Routes.MapRoute(
                "ClearVisitorSession",
                BASE_ROUTE + API_VERSION + "/ClearVisitorSession",
                new { controller = "Visitor", action = "ClearVisitorSession" },
                new[] { "Bonfire.Feature.Kickfire.Analytics.Controllers" });
        }
    }
}
