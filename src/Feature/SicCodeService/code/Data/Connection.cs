using System.Configuration;

namespace Bonfire.Feature.SicCodeService.Data
{
    public partial class SicCodesDataContext
    {
        public SicCodesDataContext() : base(ConfigurationManager.ConnectionStrings["SicCode"].ConnectionString)
        {
            OnCreated();
        }
    }
}