using Sitecore.Data.Items;

namespace Bonfire.Feature.Kickfire.Analytics.Constants
{
    public static class Items
    {
        public static Item SicParent = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName")).GetItem(IDs.SicParentId);
        public static Item GroupParent = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName")).GetItem(IDs.GroupParentId);

        public static class Profiles
        {
            public static Item Industry = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName")).GetItem(IDs.Profiles.Industry);

            public static class Keys
            {
                public static Item Agriculture = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName")).GetItem(IDs.Profiles.Keys.Industry.Agriculture);

            }
        }
    }
}
