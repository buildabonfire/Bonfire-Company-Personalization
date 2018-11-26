using System.Linq;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Repositories;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Bonfire.Feature.SicCodeService.Repositories
{
    public class SicCodeRepository : ISicCodeRepository
    {
        private readonly Database _db;

        public SicCodeRepository()
        {
            _db = Sitecore.Configuration.Factory.GetDatabase(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.MasterDatabaseName"));
        }

        public SicCodeModel GetSicCodeById(int id)
        {
            using (var context = new Data.SicCodesDataContext())
            {
                var sicCode = context.SicCodes.FirstOrDefault(x => x.SIC == id);

                if (sicCode == null)
                    return null;

                return new SicCodeModel
                {
                    Description = sicCode.Description,
                    SicCode = sicCode.SIC,
                    Grouping = sicCode.Grouping
                };
            }
        }
    }
}