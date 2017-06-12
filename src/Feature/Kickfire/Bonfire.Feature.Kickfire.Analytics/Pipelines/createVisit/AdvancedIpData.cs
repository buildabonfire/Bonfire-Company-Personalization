using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bonfire.Feature.Kickfire.Analytics.Constants;
using Bonfire.Feature.Kickfire.Analytics.Helpers;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Services;
using Sitecore.Analytics;
using Sitecore.Analytics.Automation.Data;
using Sitecore.Analytics.Pipelines.CreateVisits;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Items = Bonfire.Feature.Kickfire.Analytics.Helpers.Items;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.createVisit
{
    public class AdvancedIpData : CreateVisitProcessor
    {
        public override void Process(CreateVisitArgs args)
        {
            var companyService = DependencyResolver.Current.GetService<ICompanyService>();

            Assert.ArgumentNotNull(args, "args");
            Assert.IsNotNull(args.Session, "args.Session is null");
            Assert.IsNotNull(args.Session.Contact, "args.Session.Contact is null");
            Assert.IsNotNull(companyService, "Company service can not be null");

            // convert the IP from the tracker
            var clientIp = args.Session.Interaction.Ip[0] + "."
                              + args.Session.Interaction.Ip[1] + "."
                              + args.Session.Interaction.Ip[2] + "."
                              + args.Session.Interaction.Ip[3];

            if (args.Request.Params["Spoof"] != null)
            {
                clientIp = args.Request.Params["Spoof"];
            }

            //if (clientIp.StartsWith("127"))
            //    clientIp = "23.0.59.195";

            Log.Info("KickFire: ====== Starting Kickfire for " + clientIp + "======", "KickFire");

            Log.Info("KickFire: GEOIP Country IS : " + Tracker.Current.Session.Interaction.GeoData.Country, "KickFire");
            
            // make the call
            try
            {
                // check to see if we only want USA to keep the API hits down
                if (AnalyticsConfigurationHelper.OnlyUsa() &&
                    (Tracker.Current.Session.Interaction.GeoData.Country != "United States"
                    && Tracker.Current.Session.Interaction.GeoData.Country != "US"))
                {
                    Log.Info("KickFire: Not USA, kicking out", "KickFire");
                    return;
                }

                var model = companyService.GetRootObject(clientIp);

                // Make sure our request is good.
                if (model != null
                    && model.status == "success"
                    && model.data.Count > 0
                    &&
                    (!AnalyticsConfigurationHelper.SkipIsp() ||
                     (AnalyticsConfigurationHelper.SkipIsp() && model.data[0].isISP == 0)))
                {
                    // add company data to xDB
                    AddCompanyData(model);

                    // add company information node
                    AddCompanyInformation(model);

                    // add the page event
                    //Helpers.Events.PageEvent.RegisterCompanyEvent(model.data[0].name);

                    Log.Info("KickFire: Logged for " + clientIp, "KickFire");

                    var processSicCode = false;
                    bool.TryParse(Sitecore.Configuration.Settings.GetSetting("Bonfire.Kickfire.ProcessSicCode"), out processSicCode);

                    if (processSicCode)
                        ProcessSicCode(clientIp, model);

                    // lets kick off the Engagement Plan
                }
                else
                {
                    if (model == null)
                        Log.Info("KickFire: Model null. IP is " + clientIp, this);

                    else
                    {
                        if (model.status != "success")
                            Log.Info("KickFire: Model capture not successful. IP is " + clientIp, "KickFire");

                        if (model?.data?.Count == 0 && model.data == null)
                            Log.Info("KickFire: No data records. IP is " + clientIp, "KickFire");

                        if (model?.data != null && model?.data[0].isISP != 0)
                            Log.Info("KickFire: Is ISP, skipped. IP is " + clientIp, "KickFire");

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("KickFire: KickFire Error" + ex.Message, ex);
            }
        }

        private static void ProcessProfile(Sitecore.Data.Items.Item profileItem)
        {
            // update the traker
            var profile = Tracker.Current.Interaction.Profiles[Constants.Items.Profiles.Industry.Name];
            var scores = new Dictionary<string, float> { { profileItem.Name, 10 } };

            Log.Info("KickFire: Looking up profile " + profile.ProfileName, "KickFire");

            profile.Score(scores);
            profile.UpdatePattern();

            Log.Info("KickFire: Putting user is EngagementPlans plan.", "KickFire");

            var a = AutomationStateManager.Create(Tracker.Current.Contact);
            a.EnrollInEngagementPlan(IDs.EngagementPlans.KickFire,
                IDs.EngagementPlans.State.BeginingState);

            Log.Info("KickFire: ====== ALL IS DONE ======", "KickFire");
        }

        public static void ProcessSicCode(string clientIp, KickFireModel.RootObject model )
        {
            // Lets put the user into the right pattern
            if (string.IsNullOrWhiteSpace(model.data[0].sicCode))
            {
                Log.Info("KickFire: No SicCode from Kickfire API call. IP is " + clientIp, "KickFire");
                return;
            }

            var profileItem = Items.GetProfileItemBySicCode(model.data[0].sicCode);

            if (profileItem != null)
                Log.Info("KickFire: Override SicCode for " + model.data[0].sicCode, "KickFire");

            // convert the response to our object
            var sicCodeModel = AnalyticsHelper.GetKickfireSicCodeModel(model.data[0].sicCode);

            if (sicCodeModel == null)
            {
                Log.Info("KickFire: Could not decode API call for " + model.data[0].sicCode, "Kickfire");
                return;
            }

            // get the group by group name
            var groupItem = Items.GetSicGroup(sicCodeModel.Grouping);

            if (groupItem?.Fields[IDs.Fields.GroupParent.Profile] != null && ((LookupField)groupItem.Fields[IDs.Fields.GroupParent.Profile]).TargetItem != null)
                // get profile key from group item
                profileItem = ((LookupField)groupItem.Fields[IDs.Fields.GroupParent.Profile]).TargetItem;
            else
                Log.Info("KickFire: Could not find group in Sitecore for " + sicCodeModel.Grouping, "Kickfire");

            if (profileItem == null)
            {
                Log.Info(
                    "Profile Item is null. This is the lookup to the profile item from the Grouping item in Sitecore. ",
                    "KickFire");
                return;
            }

            ProcessProfile(profileItem);

        }

        private static void AddCompanyData(KickFireModel.RootObject model)
        {
            var data = Tracker.Current.Contact.GetFacet<ICustomerLookup>("CompanyData");
            data.name = model.data[0].name;
            data.CID = model.data[0].CID;
            data.category = model.data[0].category;
            data.city = model.data[0].city;
            data.confidence = model.data[0].confidence;
            data.country = model.data[0].country;
            data.countryShort = model.data[0].countryShort;
            data.employees = model.data[0].employees;
            data.facebook = model.data[0].facebook;
            data.isISP = model.data[0].isISP;
            data.latitude = model.data[0].latitude;
            data.linkedIn = model.data[0].linkedIn;
            data.linkedInID = model.data[0].linkedInID;
            data.longitude = model.data[0].longitude;
            data.phone = model.data[0].phone;
            data.postal = model.data[0].postal;
            data.region = model.data[0].region;
            data.regionShort = model.data[0].regionShort;
            data.revenue = model.data[0].revenue;
            data.sicCode = model.data[0].sicCode;
            data.stockSymbol = model.data[0].stockSymbol;
            data.street = model.data[0].street;
            data.twitter = model.data[0].twitter;
            data.website = model.data[0].website;
            data.isISP = model.data[0].isISP;
            data.confidence = model.data[0].confidence;
        }

        private void AddCompanyInformation(KickFireModel.RootObject model)
        {
            var companyInformationFacet = Tracker.Current.Contact.GetFacet<IContactCustomerLookups>("CompanyInformation");
            if (!companyInformationFacet.Entries.Contains(CleanName(model.data[0].name)))
            {
                var customer = companyInformationFacet.Entries.Create(CleanName(model.data[0].name));

                customer.name = model.data[0].name;
                customer.CID = model.data[0].CID;
                customer.category = model.data[0].category;
                customer.city = model.data[0].city;
                customer.confidence = model.data[0].confidence;
                customer.country = model.data[0].country;
                customer.countryShort = model.data[0].countryShort;
                customer.employees = model.data[0].employees;
                customer.facebook = model.data[0].facebook;
                customer.isISP = model.data[0].isISP;
                customer.latitude = model.data[0].latitude;
                customer.linkedIn = model.data[0].linkedIn;
                customer.linkedInID = model.data[0].linkedInID;
                customer.longitude = model.data[0].longitude;
                customer.phone = model.data[0].phone;
                customer.postal = model.data[0].postal;
                customer.region = model.data[0].region;
                customer.regionShort = model.data[0].regionShort;
                customer.revenue = model.data[0].revenue;
                customer.sicCode = model.data[0].sicCode;
                customer.stockSymbol = model.data[0].stockSymbol;
                customer.street = model.data[0].street;
                customer.twitter = model.data[0].twitter;
                customer.website = model.data[0].website;
                customer.isISP = model.data[0].isISP;
                customer.confidence = model.data[0].confidence;
                customer.modified = DateTime.Now;
            }
            else
            {
                Log.Info("KickFire: Error, could not create new companyInformationFacet", "KickFire");
            }
        }

        private string CleanName(string name)
        {
            return name.Replace(".", "").Replace(",","");
        }
    }
}