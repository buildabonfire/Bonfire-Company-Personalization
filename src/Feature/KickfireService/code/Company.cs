using System;
using System.Threading;
using System.Threading.Tasks;
using Bonfire.Foundation.Kickfire.CacheService.Cache;
using Bonfire.Foundation.Kickfire.Library.Extensions;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Repositories;
using Bonfire.Foundation.Kickfire.Library.Services;
using Newtonsoft.Json;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.KickfireService
{
    public class Company : ICompanyService
    {
        public async Task<KickFireModel> GetKickfireModel(string clientIp)
        {
            var apiUrl = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ApiUrl");
            Assert.IsNotNullOrEmpty(apiUrl, "Kickfire API url cannot be empty. Please update the settings file.");

            var apiKey = Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ApiKey");
            Assert.IsNotNullOrEmpty(apiKey, "Kickfire API key cannot be empty. Please update the settings file.");

            var company = GetCompanyFromCache(clientIp);

            if (company != null)
            {
                Log.Debug("KickFire: Got company from cache. " + clientIp, "KickFire");
                return company;
            }

            company = await CallApiService(clientIp, apiKey, apiUrl);
            CacheBuilder.KickFireCache.Set(clientIp, company.Jsonize());

            return company;
        }

        private async Task <KickFireModel> CallApiService(string clientIp, string apiKey, string apiUrl)
        {
            var apiCall = GetApiUrl(clientIp, apiKey, apiUrl);
            Log.Debug($"KickFire: Calling url {apiCall}", "KickFire");

            KickFireModel company = null;
            try
            {
                var ct = new CancellationToken();
                company = await Task.Run(() => ApiSerializationRepository.DeserializeGetRequestAsync<KickFireModel>(apiCall, ct, GetTimeSpan()), ct);

            }
            catch (TaskCanceledException tce)
            {
                Log.Error($"The API call timed out before it could be completed. The time out is {GetTimeSpan().ToString()}", tce, this);
                return null;
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e, this);
                return null;
            }

            
            if (company == null)
            {
                Log.Debug("KickFire: API call for " + clientIp +" return nulled value in response.", "KickFire");
                return null;
            }

            Log.Debug("KickFire: API call for " + clientIp + "|" + company.Jsonize(), "KickFire");
            return company;
        }

        private static string GetApiUrl(string clientIp, string apiKey, string apiUrl)
        {
            var api = $"{apiUrl}?ip={clientIp}&key={apiKey}";

            return api;
        }

        private static KickFireModel GetCompanyFromCache(string clientIp)
        {
            var cache = CacheBuilder.KickFireCache.Get(clientIp);
            if (string.IsNullOrEmpty(cache))
                return null;

            try
            {
                var model = JsonConvert.DeserializeObject<KickFireModel>(cache);
                model.IsError = false;
                return model;
            }
            catch (Exception e)
            {
                Log.Error($"Could not serialize the cache response. Value = {cache}", e, "KickFire");
                return null;
            }
        }

        private TimeSpan GetTimeSpan()
        {
            return new TimeSpan(0, 0, 2);
        }
    }
}
