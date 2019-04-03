using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public class SicCodeGroupGroupRepository : ISicCodeGroupRepository
    {
        private readonly Database _db;
        public SicCodeGroupGroupRepository()
        {
            _db = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.WebDatabaseName"));
        }

        public Item GetSicGroup(string sicCode)
        {
            var item = GetSicCodeItemOrDefault(sicCode);
            if (item != null)
                return item;

            return GetDefaultSicCode();
        }

        public Item GetProfileItemBySicCode(string sicCode)
        {
            return GetProfileFromOverride(GetSicCodeItemOrDefault(sicCode));
        }





        private Item GetDefaultSicCode()
        {
            var defaultItem = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.DefaultSicCode");

            if (_db == null || defaultItem == null || ID.IsID(defaultItem))
                return null;

            return _db.GetItem(ID.Parse(defaultItem));
        }

        private Item GetSicCodeItemOrDefault(string sicCode)
        {
            var sicCodeItem = GetSicCodeItem(sicCode); 

            if (sicCodeItem != null)
                return sicCodeItem;

            return GetDefaultSicCode();
        }

        private Item GetSicCodeItem(string sicCode)
        {
            var sicCodePath = GetSicCodeParent().Paths.FullPath;
            if (string.IsNullOrEmpty(sicCodePath)) return null;

            return DoSearch(Templates.SicCodeOverride.FieldNames.Code, sicCode, sicCodePath, Templates.SicCodeOverride.Id);
            //return GetGroupParent()?.Children.FirstOrDefault(x => x[Templates.SicCode.Fields.Group] == sicCode);
        }

        private Item GetProfileFromOverride(Item sicCodeOverride)
        {
            // get the group code this is overriding and get the profile for that
            var profileItem = (Sitecore.Data.Fields.ReferenceField)sicCodeOverride.Fields[Templates.SicCodeOverride.Fields.Grouping];
            return GetProfileFromGroup(profileItem.TargetItem);
        }

        private Item GetProfileFromGroup(Item sicCode)
        {
            var profileItem = (Sitecore.Data.Fields.ReferenceField) sicCode.Fields[Templates.SicCode.Fields.Profile];
            return profileItem.TargetItem;
        }

        public Item GetGroupParent()
        {
            var groupSetting = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.Grouping");

            if (_db == null || groupSetting == null || !ID.IsID(groupSetting))
                return null;

            return _db.GetItem(ID.Parse(groupSetting));
        }

        protected virtual string GetSearchIndexNameForDatabase() => $"sitecore_{Sitecore.Context.Database}_index";

        private Item DoSearch(string fieldName, string value, string startPath, ID templateId)
        {
            var searchIndexNameForDatabase = this.GetSearchIndexNameForDatabase();
            using (var searchContext = ContentSearchManager.GetIndex(searchIndexNameForDatabase).CreateSearchContext())
            {
                var searchResult = searchContext
                    .GetQueryable<SearchResultItem>()
                    .FirstOrDefault(x => x[fieldName] == value
                                         && x.Path.StartsWith(startPath)
                                         && x.TemplateId == templateId);

                return searchResult?.GetItem();
            }
        }

        private static Item GetSicCodeParent()
        {
            return Sitecore.Context.Database.GetItem(Constants.IDs.SicParentId);
        }
    }
}