using System;
using System.Linq;
using Bonfire.Feature.KickfireCore.Models.Facets;
using Bonfire.Foundation.XConnectService.Services;
using Sitecore.Analytics;
using Sitecore.Analytics.Pipelines.InitializeTracker;
using Sitecore.Analytics.Pipelines.StartTracking;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Data;

namespace Bonfire.Feature.KickfireCore.Pipelines.startTracking
{
    public class RegisterCompanyGoal : InitializeTrackerProcessor//StartTrackingProcessor
    {
        private readonly IEventTrackerService _eventTrackerService;

        public RegisterCompanyGoal(IEventTrackerService eventTrackerService)
        {
            _eventTrackerService = eventTrackerService;
        }

        //public override void Process(StartTrackingArgs args)
        public override void Process(InitializeTrackerArgs args)
        {
            //Assert.IsNotNull((object)Tracker.Current, "Tracker.Current is not initialized");
            //Assert.IsNotNull((object)Tracker.Current.Session, "Tracker.Current.Session is not initialized");
            //if (Tracker.Current.Session.Interaction == null)
            //    return;
            //Tracker.Current.Session.Interaction.UpdateGeoIpData();
            var goldId = new ID("{025AE5A2-A107-4E4E-B26A-E4207C1FAE40}").ToGuid();

            var test = HasCompany();

            if (IfFirstPage() && HasCompany())
                _eventTrackerService.TrackGoal(goldId);

        }

        private static bool HasCompany()
        {
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            if (xConnectFacet.Facets == null) return false;

            return xConnectFacet.Facets[CompanyFacet.DefaultFacetKey] is CompanyFacet company && company.LastModified > DateTime.Now.AddSeconds(-5);
        }

        private static bool IfFirstPage()
        {
            return  Tracker.Current.Interaction.Pages.Length == 1;
        }

        private static bool IfGoalExists(Guid goalId)
        {
            var goals = (from page in Tracker.Current.Interaction.GetPages()
                from pageEventData in page.PageEvents
                where pageEventData.IsGoal
                select pageEventData).ToList();

            return goals.Any(x => x.ItemId == goalId);
        }

        private static bool IfGoalExistsInPast(Guid goalId)
        {
            return Tracker.Current.Contact.KeyBehaviorCache.Goals.Any(x => x.Id == goalId);
        }
    }
}