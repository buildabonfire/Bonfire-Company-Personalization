using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.KickfireCore.Repository
{
    public class SicCodeGroupGroupRepository : ISicCodeGroupRepository
    {
        private readonly Database _db;
        public SicCodeGroupGroupRepository()
        {
            _db = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.WebDatabaseName"));
        }

        public Item GetProfileItemBySicCode(string sicCode)
        {
            var profileItem = GetProfileFromOverride(GetSicCodeItemOrDefault(sicCode));

            if (profileItem == null)
                Log.Info("profileItem is null", this); 

            return profileItem;
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
            if (string.IsNullOrEmpty(sicCodePath))
            {
                Log.Info("sicCodePath is null", this);
                return null;
            }

            return DoSearch(Templates.SicCodeOverride.FieldNames.Code, sicCode, sicCodePath, Templates.SicCodeOverride.Id);
        }

        private Item GetProfileFromOverride(Item sicCodeOverride)
        {
            if (sicCodeOverride == null)
            {
                Log.Info("sicCodeOverride is null", this);
                return null;
            }

            // get the group code this is overriding and get the profile for that
            var profileItem = (Sitecore.Data.Fields.ReferenceField)sicCodeOverride.Fields[Templates.SicCodeOverride.Fields.Grouping];
            return profileItem?.TargetItem != null ? GetProfileFromGroup(profileItem.TargetItem) : null;
        }

        private Item GetProfileFromGroup(Item sicCode)
        {
            if (sicCode == null)
            {
                Log.Info("sicCode is null", this);
                return null;
            }

            var profileItem = (Sitecore.Data.Fields.ReferenceField) sicCode.Fields[Templates.SicCode.Fields.Profile];
            return profileItem?.TargetItem;
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

                var result = searchResult?.GetItem();
                if (result == null)
                {
                    Log.Info($"Sic Code search is null. fieldName={fieldName}, value={value}, startPath={startPath}, templateId={templateId.Guid}", this);
                }

                return result;
            }
        }

        private static Item GetSicCodeParent()
        {
            return Sitecore.Context.Database.GetItem(Constants.IDs.SicParentId);
        }
    }
}