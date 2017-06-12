using Sitecore.Analytics.Model.Framework;

namespace Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries
{
    public interface IContactCustomerLookups : IFacet, IElement, IValidatable
    {
        IElementDictionary<IElementCustomerLookup> Entries { get; }  
    }
}
