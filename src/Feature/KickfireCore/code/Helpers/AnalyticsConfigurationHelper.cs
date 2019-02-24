using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.KickfireCore.Helpers
{
    internal static class AnalyticsConfigurationHelper
    {
        internal static Item GetConfigurationItem()
        {
            if (!string.IsNullOrEmpty(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ConfigPath")))
            {
                var configItemPath = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ConfigPath");

                var configItem = Sitecore.Context.Database.GetItem(configItemPath);

                if (configItem == null)
                    Log.Info("The configuration item is empty. Did you forget to publish? Configuration item is located at " + configItemPath + ".", new System.Exception("config item null"));
                return configItem;
            }

            return null;
        }

        internal static bool SkipIsp()
        {
            var configItem = GetConfigurationItem();

            if (configItem != null)
            {
                if (configItem.Fields[Templates.Configuration.Fields.SkipIsp] != null)
                {
                    CheckboxField skipIsp =
                        configItem.Fields[Templates.Configuration.Fields.SkipIsp];

                    return skipIsp.Checked;
                }
            }

            return false;

        }

        internal static bool OnlyUsa()
        {
            var configItem = GetConfigurationItem();

            if (configItem != null)
            {
                if (configItem.Fields[Templates.Configuration.Fields.SkipNonUsa] != null)
                {
                    CheckboxField skipUsa =
                        configItem.Fields[Templates.Configuration.Fields.SkipNonUsa];

                    return skipUsa.Checked;
                }
            }

            return false;

        }
    }
}