﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bonfire.Feature.KickfireCore.Helpers;
using Bonfire.Feature.KickfireCore.Models.Facets;
using Bonfire.Feature.KickfireCore.Repository;
using Bonfire.Feature.KickfireCore.Services;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Services;
using Bonfire.Foundation.XConnectService.Services;
using Sitecore.Analytics;
using Sitecore.Analytics.Pipelines.CreateVisits;
using Sitecore.Data;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.KickfireCore.Pipelines.createVisit
{
    public class AdvancedIpData : CreateVisitProcessor
    {
        private readonly ICompanyService _companyService;
        private readonly IEventTrackerService _eventTrackerService;
        private readonly ISicCodeGroupRepository _sicCodeGroupRepository;

        public AdvancedIpData(ISicCodeGroupRepository sicCodeGroupRepository, ICompanyService companyService, IEventTrackerService eventTrackerService)
        {
            _companyService = companyService;
            _eventTrackerService = eventTrackerService;
            _sicCodeGroupRepository = sicCodeGroupRepository;
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

            Log.Debug("KickFire: ====== Starting Kickfire for " + clientIp + "======", "KickFire");
            Log.Debug("KickFire: GEOIP Country IS : " + Tracker.Current.Session.Interaction.GeoData.Country, "KickFire");
            
            // make the call
            try
            {
                // check to see if we only want USA to keep the API hits down
                if (!ShouldProcessNonUsa())
                {
                    Log.Debug("KickFire: Not USA, kicking out", "KickFire");
                    return;
                }

                var companyModel = GetKickfireModel(clientIp);  

                // Make sure our request is good.
                if (IsRequestValid(companyModel))
                {
                    ProcessValidRequest(companyModel, clientIp);

                    // fire the goals
                    //ProcessGoals(companyModel);
                }
                else
                {
                    ProcessInvalidRequest(companyModel, clientIp);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"KickFire: KickFire Error {ex.Message}", ex, this);
            }
        }

        private void ProcessValidRequest(KickFireModel model, string clientIp)
        {
            // add company data to xDB
            UpdateCompanyDataOnClient(model);

            Log.Debug("KickFire: Logged for " + clientIp, "KickFire");

            bool.TryParse(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ProcessSicCode"),
                out var processSicCode);

            if (processSicCode)
                ProcessSicCode(clientIp, model);

            // lets kick off the Engagement Plan
        }

        private void ProcessInvalidRequest(KickFireModel model, string clientIp)
        {
            if (model == null)
                Log.Debug("KickFire: Model null. IP is " + clientIp, this);

            else
            {
                if (model.Status != "success")
                    Log.Debug("KickFire: Model capture not successful. Is is " + clientIp, "KickFire");

                if (model?.Data?.Count == 0)
                    Log.Debug("KickFire: No data records. IP is " + clientIp, "KickFire");

                if (model?.Data != null && model?.Data[0].IsIsp != 0)
                    Log.Debug("KickFire: Is ISP, skipped. IP is " + clientIp, "KickFire");
            }
        }

        private static bool IsRequestValid(KickFireModel model)
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

            Log.Debug("KickFire: Looking up profile " + profile.ProfileName, "KickFire");

            profile.Score(scores);
            profile.UpdatePattern();

            Log.Debug("KickFire: ====== ALL IS DONE ======", "KickFire"); 
        }

        private void ProcessSicCode(string clientIp, KickFireModel model )
        {
            // Lets put the user into the right pattern
            if (string.IsNullOrWhiteSpace(model.Data[0].SicCode))
            {
                Log.Debug("KickFire: No SicCode from Kickfire API call. IP is " + clientIp, "KickFire");
                return;
            }

            int.TryParse(model.Data[0].SicCode, out var sicId);

            var profileItem = _sicCodeGroupRepository.GetProfileItemBySicCode(model.Data[0].SicCode);

            if (profileItem != null)
                ProcessProfile(profileItem);

        }

        private void ProcessGoals(KickFireModel model)
        {
            var identificationGoal = AnalyticsConfigurationHelper.IdentificationGoal(); 

            if (identificationGoal != null && model.Data[0].Confidence > 70)
            {
                //_eventTrackerService.TrackGoal(identificationGoal.ID.ToGuid());

                //var companyConnectService = new CompanyConnectService();

                //companyConnectService.AddXconnectGoal(identificationGoal.ID.ToGuid(), Tracker.Current.Interaction.UserAgent, new ID("{B418E4F2-1013-4B42-A053-B6D4DCA988BF}").ToGuid());

            }
        }

        private static void UpdateCompanyDataOnClient(KickFireModel model)
        {
            var service = new CompanyConnectService();
            service.UpdateCompanyDataOnClient(HydrateCompanyModel(model));
        }

        private static CompanyFacet HydrateCompanyModel(KickFireModel model)
        {
            var data = new CompanyFacet
            {
                Name = model.Data[0].Name,
                Category = model.Data[0].Category2,
                Category2 = model.Data[0].Category,
                City = model.Data[0].City,
                Confidence = model.Data[0].Confidence,
                Country = model.Data[0].Country,
                CountryShort = model.Data[0].CountryShort,
                Employees = model.Data[0].Employees,
                Facebook = model.Data[0].Facebook,
                IsIsp = model.Data[0].IsIsp,
                Latitude = model.Data[0].Latitude,
                LinkedIn = model.Data[0].LinkedIn,
                Longitude = model.Data[0].Longitude,
                Phone = model.Data[0].Phone,
                Postal = model.Data[0].Postal,
                Region = model.Data[0].Region,
                RegionShort = model.Data[0].RegionShort,
                Revenue = model.Data[0].Revenue,
                SicCode = model.Data[0].SicCode,
                NaicsCode = model.Data[0].NaicsCode,
                NaicsGroup = model.Data[0].NaicsGroup,
                StockSymbol = model.Data[0].StockSymbol,
                Street = model.Data[0].Street,
                Twitter = model.Data[0].Twitter,
                Website = model.Data[0].Website,
                IsWifi = model.Data[0].IsWifi
            };
            data.IsIsp = model.Data[0].IsIsp;
            data.Confidence = model.Data[0].Confidence;
            return data;
        }

        private KickFireModel GetKickfireModel(string ip)
        {
            var sw = new Stopwatch();
            sw.Start();

            // lets use async to call our web service
            Log.Debug("Kickfire: Start API call to KF", "KickFire");

            var model = Task.Run(() => _companyService.GetKickfireModel(ip));
            var companyModel = model.Result;

            sw.Stop();
            Log.Debug($"Kickfire: End API call to KF. Took {sw.ElapsedMilliseconds} ms", "KickFire");

            return companyModel;
        }

        private static bool ShouldProcessNonUsa()
        {
            if (!AnalyticsConfigurationHelper.OnlyUsa()) return true;

            return Tracker.Current.Session.Interaction.GeoData.Country == "United States" 
                   || Tracker.Current.Session.Interaction.GeoData.Country == "US";
        }

        private static bool ShouldProcessIsp(KickFireModel model)
        {
            return !AnalyticsConfigurationHelper.SkipIsp() ||
                   AnalyticsConfigurationHelper.SkipIsp() && model.Data[0].IsIsp == 0;
        }
    }
}