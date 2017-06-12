using Bonfire.Foundation.Kickfire.Library.Model;

namespace Bonfire.Foundation.Kickfire.Library.Repositories
{
    public interface ISicCodeRepository
    {
        SicCodeModel GetSicCodeById(int id);
    }
}