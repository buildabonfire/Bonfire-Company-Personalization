using System;
using System.Collections.Generic;
using Bonfire.Feature.KickfireCore.Helpers;
using Bonfire.Feature.KickfireCore.Models.Facets;
using Bonfire.Feature.KickfireCore.Repository;
using Bonfire.Feature.KickfireCore.Services;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Repositories;
using Bonfire.Foundation.Kickfire.Library.Services;
using Sitecore.Analytics;
using Sitecore.Analytics.Pipelines.CreateVisits;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.KickfireCore.Pipelines.createVisit
{
    public class AdvancedIpData : CreateVisitProcessor
    {
        private readonly ICompanyService _companyService;
        private readonly ISicCodeOverrideRepository _sicCodeOverrideRepository;
        private readonly ISicCodeRepository _sicCodeRepository;
        private readonly ISicCodeGroupRepository _sicCodeGroupRepositor;

        public AdvancedIpData(ISicCodeGroupRepository sicCodeGroupRepository, 
            ICompanyService companyService, 
            ISicCodeOverrideRepository sicCodeOverrideRepository,
            ISicCodeRepository sicCodeRepository)
        {
            _companyService = companyService;
            _sicCodeOverrideRepository = sicCodeOverrideRepository;
            _sicCodeRepository = sicCodeRepository;
            _sicCodeGroupRepositor = sicCodeGroupRepository;
        }

        public override void Process(CreateVisitArgs args)
        {
            //var companyService = DependencyResolver.Current.GetService<ICompanyService>();

            Assert.ArgumentNotNull(args, "args");
            Assert.IsNotNull(args.Session, "args.Session is null");
            Assert.IsNotNull(args.Session.Contact, "args.Session.Contact is null");
            Assert.IsNotNull(_companyService, "Company service can not be null");

            // convert the IP from the tracker
            var clientIp = GetClientIp(args);

            Log.Info("KickFire: ====== Starting Kickfire for " + clientIp + "======", "KickFire");
            Log.Info("KickFire: GEOIP Country IS : " + Tracker.Current.Session.Interaction.GeoData.Country, "KickFire");
            
            // make the call
            try
            {
                // check to see if we only want USA to keep the API hits down
                if (!ShouldProcessNonUsa())
                {
                    Log.Info("KickFire: Not USA, kicking out", "KickFire");
                    return;
                }

                // call the services
                var model = _companyService.GetRootObject(clientIp);

                // Make sure our request is good.
                if (IsRequestValid(model))
                {
                    ProcessValidRequest(model, clientIp);
                }
                else
                {
                    ProcessInvalidRequest(model, clientIp);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"KickFire: KickFire Error {ex.Message}", ex, this);
            }
        }

        private void ProcessValidRequest(KickFireModel.RootObject model, string clientIp)
        {
            // add company data to xDB
            AddCompanyData(model);

            // add company information node
            //AddCompanyInformation(model);

            // add the page event
            //Helpers.Events.PageEvent.RegisterCompanyEvent(model.Data[0].name);

            Log.Info("KickFire: Logged for " + clientIp, "KickFire");

            bool.TryParse(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ProcessSicCode"),
                out var processSicCode);

            if (processSicCode)
                ProcessSicCode(clientIp, model);

            // lets kick off the Engagement Plan
        }

        private void ProcessInvalidRequest(KickFireModel.RootObject model, string clientIp)
        {
            if (model == null)
                Log.Info("KickFire: Model null. IP is " + clientIp, this);

            else
            {
                if (model.Status != "success")
                    Log.Info("KickFire: Model capture not successful. Is is " + clientIp, "KickFire");

                if (model?.Data?.Count == 0)
                    Log.Info("KickFire: No data records. IP is " + clientIp, "KickFire");

                if (model?.Data != null && model?.Data[0].IsIsp != 0)
                    Log.Info("KickFire: Is ISP, skipped. IP is " + clientIp, "KickFire");
            }
        }

        private bool IsRequestValid(KickFireModel.RootObject model)
        {
            return model != null
                   && model.Status == "success"
                   && model.Data.Count > 0
                   && ShouldProcessIsp(model);
        }

        private static string GetClientIp(CreateVisitArgs args)
        {
            var clientIp = args.Session.Interaction.Ip[0] + "."
                  + args.Session.Interaction.Ip[1] + "."
                  + args.Session.Interaction.Ip[2] + "."
                  + args.Session.Interaction.Ip[3];

            if (args.Request.Params["Spoof"] != null)
                clientIp = args.Request.Params["Spoof"];
            return clientIp;
        }

        private static void ProcessProfile(Sitecore.Data.Items.Item profileItem)
        {
            // update the tracker
            var profile = Tracker.Current.Interaction.Profiles[Constants.Profile.IndustryName];
            var scores = new Dictionary<string, double> { { profileItem.Name, 10 } };

            Log.Info("KickFire: Looking up profile " + profile.ProfileName, "KickFire");

            profile.Score(scores);
            profile.UpdatePattern();

            Log.Info("KickFire: ====== ALL IS DONE ======", "KickFire");
        }

        public void ProcessSicCode(string clientIp, KickFireModel.RootObject model )
        {
            // Lets put the user into the right pattern
            if (string.IsNullOrWhiteSpace(model.Data[0].SicCode))
            {
                Log.Info("KickFire: No SicCode from Kickfire API call. IP is " + clientIp, "KickFire");
                return;
            }

            int.TryParse(model.Data[0].SicCode, out var sicId);

            // lets look for an override
            var sicCodeModel = _sicCodeOverrideRepository.GetSicCodeFromOverride(model.Data[0].SicCode);
            if (sicCodeModel == null)
            {
                sicCodeModel = _sicCodeRepository.GetSicCodeById(sicId);
            }

            // get the profile item so we can assign the proper points
            var profileItem = _sicCodeGroupRepositor.GetProfileItemBySicCode(model.Data[0].SicCode);

            //var groupItem = SicCodeGroupRepository.GetSicGroup(model.Data[0].SicCode);

            ProcessProfile(profileItem);

        }

        private static void AddCompanyData(KickFireModel.RootObject model)
        {
            var service = new CompanyConnectService();
            service.UpdateCompanyFacet(HydrateCompanyModel(model));
        }

        private static CompanyFacet HydrateCompanyModel(KickFireModel.RootObject model)
        {
            var data = new CompanyFacet();
            data.Name = model.Data[0].Name;
            data.Cid = model.Data[0].Cid;
            data.Category = model.Data[0].Category2;
            data.Category2 = model.Data[0].Category;
            data.City = model.Data[0].City;
            data.Confidence = model.Data[0].Confidence;
            data.Country = model.Data[0].Country;
            data.CountryShort = model.Data[0].CountryShort;
            data.Employees = model.Data[0].Employees;
            data.Facebook = model.Data[0].Facebook;
            data.IsIsp = model.Data[0].IsIsp;
            data.Latitude = model.Data[0].Latitude;
            data.LinkedIn = model.Data[0].LinkedIn;
            data.Longitude = model.Data[0].Longitude;
            data.Phone = model.Data[0].Phone;
            data.Postal = model.Data[0].Postal;
            data.Region = model.Data[0].Region;
            data.RegionShort = model.Data[0].RegionShort;
            data.Revenue = model.Data[0].Revenue;
            data.SicCode = model.Data[0].SicCode;
            data.NaicsCode = model.Data[0].NaicsCode;
            data.StockSymbol = model.Data[0].StockSymbol;
            data.Street = model.Data[0].Street;
            data.Twitter = model.Data[0].Twitter;
            data.Website = model.Data[0].Website;
            data.IsIsp = model.Data[0].IsIsp;
            data.Confidence = model.Data[0].Confidence;
            return data;
        }

        private bool ShouldProcessNonUsa()
        {
            if (!AnalyticsConfigurationHelper.OnlyUsa()) return true;

            return Tracker.Current.Session.Interaction.GeoData.Country == "United States" 
                   || Tracker.Current.Session.Interaction.GeoData.Country == "US";
        }

        private bool ShouldProcessIsp(KickFireModel.RootObject model)
        {
            return !AnalyticsConfigurationHelper.SkipIsp() ||
                   AnalyticsConfigurationHelper.SkipIsp() && model.Data[0].IsIsp == 0;
        }
    }
}