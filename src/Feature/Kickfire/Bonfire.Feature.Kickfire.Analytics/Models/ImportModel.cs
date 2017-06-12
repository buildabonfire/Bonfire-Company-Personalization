using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json.Linq;
using Sitecore.Analytics;

namespace Bonfire.Feature.Kickfire.Analytics.Models
{
    public class ImportModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }

        public string JsonData 
        {
            get
            {
                var profile = Tracker.Current;
                
                JObject json = JObject.Parse("{\"SortAs\": \"SGML\",\"GlossTerm\": \"Standard Generalized Markup Language\"}");
                string formatted = json.ToString();
                return formatted;
            }
            
        }
    }
}
