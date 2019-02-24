using System.IO;
using System.Net;
using System.Text;
using Bonfire.Foundation.Kickfire.CacheService.Cache;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Services;
using Newtonsoft.Json;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.KickfireService
{
    public class Company : ICompanyService
    {
        public KickFireModel.RootObject GetRootObject(string clientIp)
        {
            var apiUrl = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ApiUrl");
            Assert.IsNotNullOrEmpty(apiUrl, "Kickfire API url cannot be empty. Please update the settings file.");

            var apiKey = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ApiKey");
            Assert.IsNotNullOrEmpty(apiKey, "Kickfire API key cannot be empty. Please update the settings file.");

            var company = CacheBuilder.KickFireCache.Get(clientIp);

            if (string.IsNullOrWhiteSpace(company))
            {
                company = CallApiService(clientIp, apiKey, apiUrl);
                CacheBuilder.KickFireCache.Set(clientIp, company);
            }
            else
                Log.Debug("KickFire: Got company from cache. " + clientIp, "KickFire");

            var data =  JsonConvert.DeserializeObject<KickFireModel.RootObject>(company);
            data.IsError = false;

            return data;
        }

        private static string CallApiService(string clientIp, string apiKey, string apiUrl)
        {
            string company;
            var apiCall = GetApiUrl(clientIp, apiKey, apiUrl);
            Log.Debug($"KickFire: Calling url {apiCall}", "KickFire");
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

            Log.Debug("KickFire: API call for " + clientIp + "|" + company, "KickFire");
            return company;
        }

        private static string GetApiUrl(string clientIp, string apiKey, string apiUrl)
        {
            var api = $"{apiUrl}?ip={clientIp}&key={apiKey}";

            return api;
        }
    }
}
