using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Bonfire.Foundation.Kickfire.CacheService.Cache;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Services;
using Newtonsoft.Json;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.ExtremeIpService.Services
{
    public class Company : ICompanyService
    {
        public KickFireModel.RootObject GetRootObject(string clientIp)
        {
            var apiUrl = Sitecore.Configuration.Settings.GetSetting("Bonfire.ExtremeIp.ApiUrl");
            Assert.IsNotNullOrEmpty(apiUrl, "Extreme API url cannot be empty. Please update the settings file.");
            
            var company = CacheBuilder.KickFireCache.Get(clientIp);

            if (string.IsNullOrWhiteSpace(company))
            {
                company = CallApiService(clientIp, apiUrl);

                CacheBuilder.KickFireCache.Set(clientIp, company);
            }
            else
                Log.Info("Extreme Ip: Got company from cache. " + clientIp, "ExtremeIp");

            var extremeData =  JsonConvert.DeserializeObject<Models.ExtremeResponse>(company);

            var data = new KickFireModel.RootObject
            {
                data = new List<KickFireModel.Datum>
                {
                    new KickFireModel.Datum
                    {
                        name = extremeData.businessName,
                        website = extremeData.businessWebsite,
                        country = extremeData.country,
                        countryShort = extremeData.countryCode,
                        latitude = extremeData.lat,
                        longitude = extremeData.lon,
                        city = extremeData.city,
                        region = extremeData.region,
                        isISP = string.IsNullOrEmpty(extremeData.businessName) ? 1 : 0
                    }
                }, status = "success"
            };

            return data;
        }

        private static string CallApiService(string clientIp, string apiUrl)
        {
            string company;
            var apiCall = GetApiUrl(clientIp, apiUrl);
            Log.Info($"Extreme Ip: Calling url {apiCall}", "ExtremeIp");
            var webRequest = WebRequest.Create(apiCall);

            using (var response = webRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream(), new UTF8Encoding()))
                {
                    // convert to our model
                    company = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                response.Close();
            }

            Log.Info("Extreme Ip: API call for " + clientIp + "|" + company, "ExtremeIp");
            return company;
        }

        private static string GetApiUrl(string clientIp, string apiUrl)
        {
            var api = $"{apiUrl}{clientIp}";

            return api;
        }
    }
}
