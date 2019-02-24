

using Bonfire.Foundation.Kickfire.Library.Model;

namespace Bonfire.Feature.KickfireCore.Repository
{
    using System.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public class SicCodeOverrideRepository : ISicCodeOverrideRepository
    {
        private readonly Database _db;

        public SicCodeOverrideRepository()
        {
            _db = Sitecore.Configuration.Factory.GetDatabase(
                Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName"));
        }

        public SicCodeModel GetSicCodeFromOverride(string sicCode)
        {
            var sicItem = GetGroupOverrideParent()?.Children
                .FirstOrDefault(x => x[Templates.SicCodeOverride.Fields.Code] == sicCode);

            return sicItem == null ? null : CreateSicCodeModel(sicItem);
        }

        public Item GetSicCodeItemFromOverride(string sicCode)
        {
            var sicItem = GetGroupOverrideParent()?.Children
                .FirstOrDefault(x => x[Templates.SicCodeOverride.Fields.Code] == sicCode);

            return sicItem;
        }

        public Item GetGroupOverrideParent()
        {
            var groupSetting = Sitecore.Configuration.Settings.GetAppSetting("Bonfire.Kickfire.Overrides");

            if (_db == null || groupSetting == null || ID.IsID(groupSetting))
                return null;

            return _db.GetItem(ID.Parse(groupSetting));
        }

        private SicCodeModel CreateSicCodeModel(Item item)
        {
            int.TryParse(item[Templates.SicCodeOverride.Fields.Code], out var sicId);

            return new SicCodeModel
            {
                Description = item[Templates.SicCodeOverride.Fields.Description],
                Grouping = item[Templates.SicCodeOverride.Fields.Grouping],
                SicCode = sicId
            };
        }
    }
}