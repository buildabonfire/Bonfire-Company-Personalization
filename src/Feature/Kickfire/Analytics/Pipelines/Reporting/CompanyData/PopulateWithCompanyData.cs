using System.Data;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Reporting.CompanyData
{
    public class PopulateWithCompanyData : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            var result = args.QueryResult;
            var table = args.ResultTableForView;
            if (table.Columns.Contains("Name") 
                && result.Rows.Count > 0
                && result.Rows[0]["CompanyData_Name"] != null
                && !string.IsNullOrEmpty(result.Rows[0]["CompanyData_Name"].ToString()))
            {
                foreach (DataRow row in result.AsEnumerable())
                {
                    var targetRow = table.NewRow();

                    var name = row["CompanyData_Name"];
                    if (name != null && !string.IsNullOrEmpty(name.ToString()))
                    {
                        targetRow["Name"] = name;
                    }

                    var website = row["CompanyData_Website"];
                    if (website != null && !string.IsNullOrEmpty(website.ToString()))
                    {
                        targetRow["Website"] = website;
                    }

                    var street = row["CompanyData_Street"];
                    if (street != null && !string.IsNullOrEmpty(street.ToString()))
                    {
                        targetRow["Street"] = street;
                    }

                    var city = row["CompanyData_City"];
                    if (city != null && !string.IsNullOrEmpty(city.ToString()))
                    {
                        targetRow["City"] = city;
                    }

                    var cid = row["CompanyData_Cid"];
                    if (cid != null && !string.IsNullOrEmpty(cid.ToString()))
                    {
                        targetRow["Cid"] = cid;
                    }

                    var regionShort = row["CompanyData_RegionShort"];
                    if (regionShort != null && !string.IsNullOrEmpty(regionShort.ToString()))
                    {
                        targetRow["RegionShort"] = regionShort;
                    }

                    var region = row["CompanyData_Region"];
                    if (region != null && !string.IsNullOrEmpty(region.ToString()))
                    {
                        targetRow["Region"] = region;
                    }

                    var postal = row["CompanyData_Postal"];
                    if (postal != null && !string.IsNullOrEmpty(postal.ToString()))
                    {
                        targetRow["postal"] = postal;
                    }

                    var countryShort = row["CompanyData_CountryShort"];
                    if (countryShort != null && !string.IsNullOrEmpty(countryShort.ToString()))
                    {
                        targetRow["CountryShort"] = countryShort;
                    }

                    var country = row["CompanyData_Country"];
                    if (country != null && !string.IsNullOrEmpty(country.ToString()))
                    {
                        targetRow["Country"] = country;
                    }

                    var phone = row["CompanyData_Phone"];
                    if (phone != null && !string.IsNullOrEmpty(phone.ToString()))
                    {
                        targetRow["Phone"] = phone;
                    }

                    var employees = row["CompanyData_Employees"];
                    if (employees != null && !string.IsNullOrEmpty(employees.ToString()))
                    {
                        targetRow["Employees"] = employees;
                    }

                    var revenue = row["CompanyData_Revenue"];
                    if (revenue != null && !string.IsNullOrEmpty(revenue.ToString()))
                    {
                        targetRow["Revenue"] = revenue;
                    }

                    var category = row["CompanyData_Category"];
                    if (category != null && !string.IsNullOrEmpty(category.ToString()))
                    {
                        targetRow["Category"] = category;
                    }

                    var sicCode = row["CompanyData_SicCode"];
                    if (sicCode != null && !string.IsNullOrEmpty(sicCode.ToString()))
                    {
                        targetRow["SicCode"] = sicCode;
                    }

                    var latitude = row["CompanyData_Latitude"];
                    if (latitude != null && !string.IsNullOrEmpty(latitude.ToString()))
                    {
                        targetRow["Latitude"] = latitude;
                    }

                    var longitude = row["CompanyData_Longitude"];
                    if (longitude != null && !string.IsNullOrEmpty(longitude.ToString()))
                    {
                        targetRow["Longitude"] = longitude;
                    }

                    var stockSymbol = row["CompanyData_StockSymbol"];
                    if (stockSymbol != null && !string.IsNullOrEmpty(stockSymbol.ToString()))
                    {
                        targetRow["StockSymbol"] = stockSymbol;
                    }

                    var facebook = row["CompanyData_Facebook"];
                    if (facebook != null && !string.IsNullOrEmpty(facebook.ToString()))
                    {
                        targetRow["Facebook"] = facebook;
                    }

                    var twitter = row["CompanyData_Twitter"];
                    if (twitter != null && !string.IsNullOrEmpty(twitter.ToString()))
                    {
                        targetRow["Twitter"] = twitter;
                    }

                    var linkedIn = row["CompanyData_LinkedIn"];
                    if (linkedIn != null && !string.IsNullOrEmpty(linkedIn.ToString()))
                    {
                        targetRow["LinkedIn"] = linkedIn;
                    }

                    var linkedInId = row["CompanyData_LinkedInId"];
                    if (linkedInId != null && !string.IsNullOrEmpty(linkedInId.ToString()))
                    {
                        targetRow["LinkedInId"] = linkedInId;
                    }

                    var isIsp = row["CompanyData_IsIsp"];
                    if (isIsp != null && !string.IsNullOrEmpty(isIsp.ToString()))
                    {
                        targetRow["IsIsp"] = isIsp;
                    }

                    var confidence = row["CompanyData_Confidence"];
                    if (confidence != null && !string.IsNullOrEmpty(confidence.ToString()))
                    {
                        targetRow["Confidence"] = confidence;
                    }
                   
                    table.Rows.Add(targetRow);
                }
            }
            args.ResultSet.Data.Dataset[args.ReportParameters.ViewName] = table;
        }
    }
}
