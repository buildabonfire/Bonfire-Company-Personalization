using System;
using Sitecore.Analytics.Model.Framework;

namespace Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries
{
    public interface IElementCustomerLookup : IElement, IValidatable
    {
        string CID { get; set; }
        string name { get; set; }
        string website { get; set; }
        string street { get; set; }
        string city { get; set; }
        string regionShort { get; set; }
        string region { get; set; }
        string postal { get; set; }
        string countryShort { get; set; }
        string country { get; set; }
        string phone { get; set; }
        string employees { get; set; }
        string revenue { get; set; }
        string category { get; set; }
        string sicCode { get; set; }
        string latitude { get; set; }
        string longitude { get; set; }
        string stockSymbol { get; set; }
        string facebook { get; set; }
        string twitter { get; set; }
        string linkedIn { get; set; }
        string linkedInID { get; set; }
        int isISP { get; set; }
        int confidence { get; set; }
        DateTime modified { get; set; }

    }
}
