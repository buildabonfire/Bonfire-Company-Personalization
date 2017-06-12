using System;
using System.Data;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Sitecore.Cintel.Reporting;

namespace Bonfire.Feature.Kickfire.Analytics.Helpers.Reporting
{
    public static class CompanyInfoHelper
    {
        internal static DataTable CreateCompanyDataTable(string name)
        {
            //var resultTable = new DataTable("CompanyInfo");

            var resultTable = new DataTable(name);
            resultTable.Columns.Add(new ViewField<string>("Name").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Website").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Street").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("City").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Cid").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("RegionShort").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Region").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Postal").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("CountryShort").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Country").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Phone").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Employees").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Revenue").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Category").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("SicCode").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Latitude").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Longitude").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("StockSymbol").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Facebook").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Twitter").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("LinkedIn").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("LinkedInId").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("IsIsp").ToColumn());
            resultTable.Columns.Add(new ViewField<string>("Confidence").ToColumn());
            resultTable.Columns.Add(new ViewField<DateTime>("Modified").ToColumn());

            return resultTable;
        }

        internal static DataRow FillCompanyDataTable(DataRow dataRow, IElementCustomerLookup elementCustomerLookup)
        {

            dataRow["Name"] = elementCustomerLookup.name;
            dataRow["Website"] = elementCustomerLookup.website;
            dataRow["Street"] = elementCustomerLookup.street;
            dataRow["City"] = elementCustomerLookup.city;
            dataRow["Cid"] = elementCustomerLookup.CID;
            dataRow["RegionShort"] = elementCustomerLookup.regionShort;
            dataRow["Region"] = elementCustomerLookup.region;
            dataRow["Postal"] = elementCustomerLookup.postal;
            dataRow["CountryShort"] = elementCustomerLookup.countryShort;
            dataRow["Country"] = elementCustomerLookup.country;
            dataRow["Phone"] = elementCustomerLookup.phone;
            dataRow["Employees"] = elementCustomerLookup.employees;
            dataRow["Revenue"] = elementCustomerLookup.revenue;
            dataRow["Category"] = elementCustomerLookup.category;
            dataRow["SicCode"] = elementCustomerLookup.sicCode;
            dataRow["Latitude"] = elementCustomerLookup.latitude;
            dataRow["Longitude"] = elementCustomerLookup.longitude;
            dataRow["StockSymbol"] = elementCustomerLookup.stockSymbol;
            dataRow["Facebook"] = elementCustomerLookup.facebook;
            dataRow["Twitter"] = elementCustomerLookup.twitter;
            dataRow["LinkedIn"] = elementCustomerLookup.linkedIn;
            dataRow["LinkedInId"] = elementCustomerLookup.linkedInID;
            dataRow["IsIsp"] = elementCustomerLookup.isISP;
            dataRow["Confidence"] = elementCustomerLookup.confidence;
            dataRow["Modified"] = elementCustomerLookup.modified;

            return dataRow;
        }
    }
}
