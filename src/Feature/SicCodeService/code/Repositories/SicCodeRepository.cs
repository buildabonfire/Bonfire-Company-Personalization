using System.Linq;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Repositories;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Bonfire.Feature.SicCodeService.Repositories
{
    public class SicCodeRepository : ISicCodeRepository
    {
        public SicCodeModel GetSicCodeById(int id)
        {
            // lets get the SicCode from SQL
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