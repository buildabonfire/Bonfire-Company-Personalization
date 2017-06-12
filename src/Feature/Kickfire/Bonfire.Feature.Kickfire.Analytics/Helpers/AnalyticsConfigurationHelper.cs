using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.Kickfire.Analytics.Helpers
{
    internal static class AnalyticsConfigurationHelper
    {
        internal static Item GetConfigurationItem()
        {
            if (!string.IsNullOrEmpty(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ConfigPath")))
            {
                var ConfigItemPath = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ConfigPath");

                var ConfigItem = Sitecore.Context.Database.GetItem(ConfigItemPath);

                if (ConfigItem == null)
                    Log.Info("The configuration item is empty. Did you forget to publish? Configuration item is located at " + ConfigItemPath + ".", new System.Exception("config item null"));
                return ConfigItem;
            }

            return null;
        }

        internal static bool SkipIsp()
        {
            var configItem = GetConfigurationItem();

            if (configItem != null)
            {
                if (configItem.Fields[Constants.IDs.Fields.Configuration.SkipIsp] != null)
                {
                    CheckboxField skipIsp =
                        configItem.Fields[Constants.IDs.Fields.Configuration.SkipIsp];

                    return skipIsp.Checked;
                }
            }

            return true;

        }

        internal static bool OnlyUsa()
        {
            var configItem = GetConfigurationItem();

            if (configItem != null)
            {
                if (configItem.Fields[Constants.IDs.Fields.Configuration.SkipNonUsa] != null)
                {
                    CheckboxField skipUsa =
                        configItem.Fields[Constants.IDs.Fields.Configuration.SkipNonUsa];

                    return skipUsa.Checked;
                }
            }

            return false;

        }
    }
}
