using System;
using Sitecore.XConnect;

namespace Bonfire.Feature.KickfireCore.Models.Facets
{
    [Serializable]
    [FacetKey(DefaultFacetKey)]
    public class CompanyFacet : Facet
    {
        public const string DefaultFacetKey = "CompanyFacet";
        public string Cid { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string RegionShort { get; set; }
        public string Region { get; set; }
        public string Postal { get; set; }
        public string CountryShort { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Employees { get; set; }
        public string Revenue { get; set; }
        public string Category { get; set; }
        public string Category2 { get; set; }
        public string NaicsCode { get; set; }
        public string SicCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string StockSymbol { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public int IsIsp { get; set; }
        public int Confidence { get; set; }
    }
}