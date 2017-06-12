using System;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Sitecore.Analytics.Model.Framework;

namespace Bonfire.Feature.Kickfire.Analytics.Models.Generated
{
    [Serializable]
    class CompanyLookupModel : Facet, ICustomerLookup
    {
        
        private const string FieldCid  = "Cid";
        private const string FieldName  = "Name";
        private const string FieldWebsite  = "Website";
        private const string FieldStreet  = "Street";
        private const string FieldCity  = "City";
        private const string FieldRegionShort = "RegionShort";
        private const string FieldRegion  = "Region";
        private const string FieldPostal  = "Postal";
        private const string FieldCountryShort = "CountryShort";
        private const string FieldCountry = "Country";
        private const string FieldPhone  = "Phone";
        private const string FieldEmployees  = "Employees";
        private const string FieldRevenue = "Revenue";
        private const string FieldCategory = "Category";
        private const string FieldSicCode = "SicCode";
        private const string FieldLatitude = "Latitude";
        private const string FieldLongitude = "Longitude";
        private const string FieldStockSymbol = "StockSymbol";
        private const string FieldFacebook = "Facebook";
        private const string FieldTwitter = "Twitter";
        private const string FieldLinkedIn = "LinkedIn";
        private const string FieldLinkedInId = "LinkedInId";
        private const string FieldIsIsp = "IsIsp";
        private const string FieldConfidence = "Confidence";

        public CompanyLookupModel()
        {
            base.EnsureAttribute<string>(FieldCid);
            base.EnsureAttribute<string>(FieldName);
            base.EnsureAttribute<string>(FieldWebsite);
            base.EnsureAttribute<string>(FieldStreet);
            base.EnsureAttribute<string>(FieldCity);
            base.EnsureAttribute<string>(FieldRegionShort);
            base.EnsureAttribute<string>(FieldRegion);
            base.EnsureAttribute<string>(FieldPostal);
            base.EnsureAttribute<string>(FieldCountryShort);
            base.EnsureAttribute<string>(FieldCountry);
            base.EnsureAttribute<string>(FieldPhone);
            base.EnsureAttribute<string>(FieldEmployees);
            base.EnsureAttribute<string>(FieldRevenue);
            base.EnsureAttribute<string>(FieldCategory);
            base.EnsureAttribute<string>(FieldSicCode);
            base.EnsureAttribute<string>(FieldLatitude);
            base.EnsureAttribute<string>(FieldLongitude);
            base.EnsureAttribute<string>(FieldStockSymbol);
            base.EnsureAttribute<string>(FieldFacebook);
            base.EnsureAttribute<string>(FieldTwitter);
            base.EnsureAttribute<string>(FieldLinkedIn);
            base.EnsureAttribute<string>(FieldLinkedInId);
            base.EnsureAttribute<int>(FieldIsIsp);
            base.EnsureAttribute<int>(FieldConfidence);
        }

        public string CID
        {
            get { return base.GetAttribute<string>(FieldCid); }
            set { base.SetAttribute<string>(FieldCid, value); }
        }

        public string name
        {
            get { return base.GetAttribute<string>(FieldName); }
            set { base.SetAttribute<string>(FieldName, value); }
        }

        public string website
        {
            get { return base.GetAttribute<string>(FieldWebsite); }
            set { base.SetAttribute<string>(FieldWebsite, value); }
        }

        public string street
        {
            get { return base.GetAttribute<string>(FieldStreet); }
            set { base.SetAttribute<string>(FieldStreet, value); }
        }

        public string city
        {
            get { return base.GetAttribute<string>(FieldCity); }
            set { base.SetAttribute<string>(FieldCity, value); }
        }

        public string regionShort
        {
            get { return base.GetAttribute<string>(FieldRegionShort); }
            set { base.SetAttribute<string>(FieldRegionShort, value); }
        }

        public string region
        {
            get { return base.GetAttribute<string>(FieldRegion); }
            set { base.SetAttribute<string>(FieldRegion, value); }
        }

        public string postal
        {
            get { return base.GetAttribute<string>(FieldPostal); }
            set { base.SetAttribute<string>(FieldPostal, value); }
        }

        public string countryShort
        {
            get { return base.GetAttribute<string>(FieldCountryShort); }
            set { base.SetAttribute<string>(FieldCountryShort, value); }
        }

        public string country
        {
            get { return base.GetAttribute<string>(FieldCountry); }
            set { base.SetAttribute<string>(FieldCountry, value); }
        }

        public string phone
        {
            get { return base.GetAttribute<string>(FieldPhone); }
            set { base.SetAttribute<string>(FieldPhone, value); }
        }

        public string employees
        {
            get { return base.GetAttribute<string>(FieldEmployees); }
            set { base.SetAttribute<string>(FieldEmployees, value); }
        }

        public string revenue
        {
            get { return base.GetAttribute<string>(FieldRevenue); }
            set { base.SetAttribute<string>(FieldRevenue, value); }
        }

        public string category
        {
            get { return base.GetAttribute<string>(FieldCategory); }
            set { base.SetAttribute<string>(FieldCategory, value); }
        }

        public string sicCode
        {
            get { return base.GetAttribute<string>(FieldSicCode); }
            set { base.SetAttribute<string>(FieldSicCode, value); }
        }

        public string latitude
        {
            get { return base.GetAttribute<string>(FieldLatitude); }
            set { base.SetAttribute<string>(FieldLatitude, value); }
        }

        public string longitude
        {
            get { return base.GetAttribute<string>(FieldLongitude); }
            set { base.SetAttribute<string>(FieldLongitude, value); }
        }

        public string stockSymbol
        {
            get { return base.GetAttribute<string>(FieldStockSymbol); }
            set { base.SetAttribute<string>(FieldStockSymbol, value); }
        }

        public string facebook
        {
            get { return base.GetAttribute<string>(FieldFacebook); }
            set { base.SetAttribute<string>(FieldFacebook, value); }
        }

        public string twitter
        {
            get { return base.GetAttribute<string>(FieldTwitter); }
            set { base.SetAttribute<string>(FieldTwitter, value); }
        }

        public string linkedIn
        {
            get { return base.GetAttribute<string>(FieldLinkedIn); }
            set { base.SetAttribute<string>(FieldLinkedIn, value); }
        }

        public string linkedInID
        {
            get { return base.GetAttribute<string>(FieldLinkedInId); }
            set { base.SetAttribute<string>(FieldLinkedInId, value); }
        }

        public int isISP
        {
            get { return base.GetAttribute<int>(FieldIsIsp); }
            set { base.SetAttribute<int>(FieldIsIsp, value); }
        }

        public int confidence
        {
            get { return base.GetAttribute<int>(FieldConfidence); }
            set { base.SetAttribute<int>(FieldConfidence, value); }
        }
    }
}
