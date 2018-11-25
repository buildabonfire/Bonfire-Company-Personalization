namespace Bonfire.Foundation.Kickfire.Library.Interfaces
{
    public interface ICustomerLookup
    {
        string Cid { get; set; }
        string Name { get; set; }
        string Website { get; set; }
        string Street { get; set; }
        string City { get; set; }
        string RegionShort { get; set; }
        string Region { get; set; }
        string Postal { get; set; }
        string CountryShort { get; set; }
        string Country { get; set; }
        string Phone { get; set; }
        string Employees { get; set; }
        string Revenue { get; set; }
        string Category { get; set; }
        string SicCode { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        string StockSymbol { get; set; }
        string Facebook { get; set; }
        string Twitter { get; set; }
        string LinkedIn { get; set; }
        string LinkedInId { get; set; }
        int IsIsp { get; set; }
        int Confidence { get; set; }
    }
}
