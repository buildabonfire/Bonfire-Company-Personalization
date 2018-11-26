using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public class SicCodeGroupGroupRepository : ISicCodeGroupRepository
    {
        private readonly Database _db;
        public SicCodeGroupGroupRepository()
        {
            _db = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName"));
        }

        public Item GetSicGroup(string sicCode)
        {
            var item = GetSicCodeItem(sicCode);
            if (item != null)
                return item;

            return GetDefaultSicCode();
        }

        public Item GetProfileItemBySicCode(string sicCode)
        {
            return GetProfileFromSicCode(GetSicCodeItem(sicCode));
        }





        private Item GetDefaultSicCode()
        {
            var defaultItem = Sitecore.Configuration.Settings.GetAppSetting("Bonfire.Kickfire.DefaultSicCode");

            if (_db == null || defaultItem == null || ID.IsID(defaultItem))
                return null;

            return _db.GetItem(ID.Parse(defaultItem));
        }

        private Item GetSicCodeItem(string sicCode)
        {
            var sicCodeItem = GetSicCodeFromBase(sicCode);

            if (sicCodeItem != null)
                return sicCodeItem;

            return GetDefaultSicCode();
        }

        private Item GetSicCodeFromBase(string sicCode)
        {
            return GetGroupParent()?.Children.FirstOrDefault(x => x[Templates.SiceCode.Fields.Group] == sicCode);
        }

        private Item GetProfileFromOverride(Item sicCodeOverride)
        {
            // get the group code this is overriding and get the profile for that
            var profileItem = (Sitecore.Data.Fields.ReferenceField)sicCodeOverride.Fields[Templates.SicCodeOverride.Fields.Grouping];
            return GetProfileFromSicCode(profileItem.TargetItem);
        }

        private Item GetProfileFromSicCode(Item sicCode)
        {
            var profileItem = (Sitecore.Data.Fields.ReferenceField) sicCode.Fields[Templates.SiceCode.Fields.Profile];
            return profileItem.TargetItem;
        }

        public Item GetGroupParent()
        {
            var groupSetting = Sitecore.Configuration.Settings.GetAppSetting("Bonfire.Kickfire.Grouping");

            if (_db == null || groupSetting == null || ID.IsID(groupSetting))
                return null;

            return _db.GetItem(ID.Parse(groupSetting));
        }
    }
}