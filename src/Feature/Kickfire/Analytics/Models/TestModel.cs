using Sitecore.Analytics;

namespace Bonfire.Feature.Kickfire.Analytics.Models
{
    public class TestModel
    {
        public string TestUrl { get; set; }

        public string TestText
        {
            get
            {
                var profile = Tracker.Current.Interaction.Profiles[Constants.Items.Profiles.Industry.Name];
                
                profile.UpdatePattern();

                return "yup";
            }
        }
    }
}