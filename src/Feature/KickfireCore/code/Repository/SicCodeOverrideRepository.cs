

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
            _db = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName"));
        }

        public Item GetSicCodeFromOverride(string sicCode)
        {
            return GetGroupOverrideParent()?.Children.FirstOrDefault(x => x[Templates.SicCodeOverride.Fields.Code] == sicCode);
        }

        public Item GetGroupOverrideParent()
        {
            var groupSetting = Sitecore.Configuration.Settings.GetAppSetting("Bonfire.Kickfire.Overrides");

            if (_db == null || groupSetting == null || ID.IsID(groupSetting))
                return null;

            return _db.GetItem(ID.Parse(groupSetting));
        }
    }
}