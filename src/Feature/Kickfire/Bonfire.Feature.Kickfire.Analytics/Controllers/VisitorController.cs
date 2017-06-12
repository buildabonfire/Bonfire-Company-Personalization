using System.Web.Mvc;
using Bonfire.Feature.Kickfire.Analytics.Dto;
using Bonfire.Feature.Kickfire.Analytics.Models;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.Kickfire.Analytics.Controllers
{
    public class VisitorController : Controller
    {
        
        [HttpGet]
        public JsonResult VisitorDetailsJson()
        {

            var test = Sitecore.Context.Item;
            var vi = new VisitorInformation();
            var trackerDto2 = vi.GetTrackerDto();

            return Json(trackerDto2, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ClearVisitorSession()
        {
            Session.Abandon();

            Log.Info("Closing xDB session", this);

            var alert = new Alerts {Message = "Complete"};

            return Json(alert, JsonRequestBehavior.AllowGet);
        }
    }
}
