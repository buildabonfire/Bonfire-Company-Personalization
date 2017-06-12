using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Reporting.CompanyInfo
{
    public class PopulateWithCompanyData : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            // if we are looking for history, we want a few things
            // 1. We want to skip the latest record since it is in the "Current Company"
            // 2. If there is only 1 record, skip it.

            // if we are looking for current, we only want the latest record.
            bool history = false;

            if (args.ReportParameters.AdditionalParameters.Any(x => x.Key == "type" && x.Value.ToString() == "history"))
                history = true;

            //if (args.ReportParameters.AdditionalParameters["type"] != null
            //    && args.ReportParameters.AdditionalParameters["type"].ToString() == "history")
            //    history = true;
            
            
            var result = args.QueryResult;
            var table = args.ResultTableForView;

            if (table.Columns.Contains("Name")
                && ((history && result.Rows.Count > 1) || !history))
            {
                IEnumerable<DataRow> orderedRows = result.AsEnumerable()
                    .OrderByDescending(r => r.Field<DateTime>("Modified"));
                
                int i = 0;
                foreach (DataRow row in orderedRows)
                {
                    // if we want the current, only send bak the 1st record. The latest.
                    if ((!history && i == 0) || (history && i > 0))
                    {
                        var targetRow = table.NewRow();

 #region Add Data
		                       var name = row["Name"];
                        if (name != null && !string.IsNullOrEmpty(name.ToString()))
                        {
                            targetRow["Name"] = name;
                        }

                        var website = row["Website"];
                        if (website != null && !string.IsNullOrEmpty(website.ToString()))
                        {
                            targetRow["Website"] = website;
                        }

                        var street = row["Street"];
                        if (street != null && !string.IsNullOrEmpty(street.ToString()))
                        {
                            targetRow["Street"] = street;
                        }

                        var city = row["City"];
                        if (city != null && !string.IsNullOrEmpty(city.ToString()))
                        {
                            targetRow["City"] = city;
                        }

                        var cid = row["Cid"];
                        if (cid != null && !string.IsNullOrEmpty(cid.ToString()))
                        {
                            targetRow["Cid"] = cid;
                        }

                        var regionShort = row["RegionShort"];
                        if (regionShort != null && !string.IsNullOrEmpty(regionShort.ToString()))
                        {
                            targetRow["RegionShort"] = regionShort;
                        }

                        var region = row["Region"];
                        if (region != null && !string.IsNullOrEmpty(region.ToString()))
                        {
                            targetRow["Region"] = region;
                        }

                        var postal = row["postal"];
                        if (postal != null && !string.IsNullOrEmpty(postal.ToString()))
                        {
                            targetRow["postal"] = postal;
                        }

                        var countryShort = row["CountryShort"];
                        if (countryShort != null && !string.IsNullOrEmpty(countryShort.ToString()))
                        {
                            targetRow["CountryShort"] = countryShort;
                        }

                        var country = row["Country"];
                        if (country != null && !string.IsNullOrEmpty(country.ToString()))
                        {
                            targetRow["Country"] = country;
                        }

                        var phone = row["Phone"];
                        if (phone != null && !string.IsNullOrEmpty(phone.ToString()))
                        {
                            targetRow["Phone"] = phone;
                        }

                        var employees = row["Employees"];
                        if (employees != null && !string.IsNullOrEmpty(employees.ToString()))
                        {
                            targetRow["Employees"] = employees;
                        }

                        var revenue = row["Revenue"];
                        if (revenue != null && !string.IsNullOrEmpty(revenue.ToString()))
                        {
                            targetRow["Revenue"] = revenue;
                        }

                        var category = row["Category"];
                        if (category != null && !string.IsNullOrEmpty(category.ToString()))
                        {
                            targetRow["Category"] = category;
                        }

                        var sicCode = row["SicCode"];
                        if (sicCode != null && !string.IsNullOrEmpty(sicCode.ToString()))
                        {
                            targetRow["SicCode"] = sicCode;
                        }

                        var latitude = row["Latitude"];
                        if (latitude != null && !string.IsNullOrEmpty(latitude.ToString()))
                        {
                            targetRow["Latitude"] = latitude;
                        }

                        var longitude = row["Longitude"];
                        if (longitude != null && !string.IsNullOrEmpty(longitude.ToString()))
                        {
                            targetRow["Longitude"] = longitude;
                        }

                        var stockSymbol = row["StockSymbol"];
                        if (stockSymbol != null && !string.IsNullOrEmpty(stockSymbol.ToString()))
                        {
                            targetRow["StockSymbol"] = stockSymbol;
                        }

                        var facebook = row["Facebook"];
                        if (facebook != null && !string.IsNullOrEmpty(facebook.ToString()))
                        {
                            targetRow["Facebook"] = facebook;
                        }

                        var twitter = row["Twitter"];
                        if (twitter != null && !string.IsNullOrEmpty(twitter.ToString()))
                        {
                            targetRow["Twitter"] = twitter;
                        }

                        var linkedIn = row["LinkedIn"];
                        if (linkedIn != null && !string.IsNullOrEmpty(linkedIn.ToString()))
                        {
                            targetRow["LinkedIn"] = linkedIn;
                        }

                        var linkedInId = row["LinkedInId"];
                        if (linkedInId != null && !string.IsNullOrEmpty(linkedInId.ToString()))
                        {
                            targetRow["LinkedInId"] = linkedInId;
                        }

                        var isIsp = row["IsIsp"];
                        if (isIsp != null && !string.IsNullOrEmpty(isIsp.ToString()))
                        {
                            targetRow["IsIsp"] = isIsp;
                        }

                        var confidence = row["Confidence"];
                        if (confidence != null && !string.IsNullOrEmpty(confidence.ToString()))
                        {
                            targetRow["Confidence"] = confidence;
                        } 
	#endregion

                        table.Rows.Add(targetRow);
                    }

                    i++;
                }
            }
            args.ResultSet.Data.Dataset[args.ReportParameters.ViewName] = table;
            args.ResultSet.TotalResultCount = table.Rows.Count;
        }
    }
}
