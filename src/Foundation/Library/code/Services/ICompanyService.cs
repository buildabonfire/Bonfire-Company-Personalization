using System.Threading.Tasks;
using Bonfire.Foundation.Kickfire.Library.Model;

namespace Bonfire.Foundation.Kickfire.Library.Services
{
    public interface ICompanyService
    {
        Task<KickFireModel> GetKickfireModel(string clientIp);
    }
}
