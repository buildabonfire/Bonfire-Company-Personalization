using Bonfire.Foundation.Kickfire.Library.Model;

namespace Bonfire.Foundation.Kickfire.Library.Services
{
    public interface ICompanyService
    {
        KickFireModel.RootObject GetRootObject(string clientIp);
    }
}
