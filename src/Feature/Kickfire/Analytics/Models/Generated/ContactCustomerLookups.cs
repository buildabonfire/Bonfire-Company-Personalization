using System;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Sitecore.Analytics.Model.Framework;

namespace Bonfire.Feature.Kickfire.Analytics.Models.Generated
{
    [Serializable]
    internal class ContactCustomerLookups: Facet, IContactCustomerLookups, IFacet, IElement, IValidatable
	{

		private const string ENTRIES = "Entries";

		public IElementDictionary<IElementCustomerLookup> Entries
		{
			get
			{
				return base.GetDictionary<IElementCustomerLookup>("Entries");
			}
		}

        public ContactCustomerLookups()
		{
			base.EnsureDictionary<IElementCustomerLookup>("Entries");
		}
	}
}
