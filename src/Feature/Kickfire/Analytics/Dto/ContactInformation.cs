using System;
using System.Collections.Generic;
using System.Linq;
using Bonfire.Feature.Kickfire.Analytics.Extensions;
using Sitecore.Analytics;
using Sitecore.Analytics.Automation.Data;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Analytics.Tracking;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Bonfire.Feature.Kickfire.Analytics.Dto
{
    public class VisitorInformation
    {

        public TrackerDto GetTrackerDto()
        {
            var currentTracker = Tracker.Current;

            var trackerDto = new TrackerDto
            {
                CurrentPage = new CurrentPage { Url = currentTracker.CurrentPage.Url },
                Interaction = GetInteractions(currentTracker.Interaction),
                IsActive = currentTracker.IsActive,
                Session = new Session
                {
                    Contact = GetContact(currentTracker.Session.Contact),
                    Interaction = GetInteractions(currentTracker.Session.Interaction)
                },
                Campaign = GetCampaign(currentTracker.Interaction),
                Contact = GetContact(currentTracker.Contact)
            };

            return trackerDto;
        }

        public IVisitProfiles GetTrackerDtoProfiles()
        {
            var currentTracker = Tracker.Current;

            //Sitecore.Analytics.Tracking.VisitProfiles profiles = currentTracker.Interaction.Profiles;
            return currentTracker.Interaction.Profiles;
        }


        #region  fill functions
        public Interactions GetInteractions(CurrentInteraction currentInteraction)
        {
            var interactions = new Interactions
            {
                BrowserInfo = currentInteraction.BrowserInfo,
                CampaignId = currentInteraction.CampaignId,
                ContactId = currentInteraction.ContactId,
                ChannelId = currentInteraction.ChannelId,
                ContactVisitIndex = currentInteraction.ContactVisitIndex,
                CustomValues = currentInteraction.CustomValues,
                DeviceId = currentInteraction.DeviceId,
                GeoData = currentInteraction.GeoData,
                HasGeoIpData = currentInteraction.HasGeoIpData,
                InteractionId = currentInteraction.InteractionId,
                Ip = currentInteraction.Ip,
                Keywords = currentInteraction.Keywords,
                Language = currentInteraction.Language,
                Profiles = currentInteraction.Profiles,
                ScreenInfo = currentInteraction.ScreenInfo,
                SiteName = currentInteraction.SiteName,
                Value = currentInteraction.Value
            };


            return interactions;
        }

        public Contact GetContact(Sitecore.Analytics.Tracking.Contact currectContact)
        {
            var contact = new Contact
            {
                //Attachments = currectContact.Attachments,
                Profiles = currectContact.BehaviorProfiles.Profiles.Select(profile => new ExtraBehaviorProfileContext
                {
                    Id = profile.Id,
                    NumberOfTimesScored = profile.NumberOfTimesScored,
                    Scores = profile.Scores,
                    PatternId = profile.PatternId,
                    ProfileName = Sitecore.Context.Database.GetItem(profile.Id).Name,
                    PatterneName = (!ID.IsNullOrEmpty(profile.PatternId)) ? Sitecore.Context.Database.GetItem(profile.PatternId).Name : ""
                }).ToList(),
                ContactId = currectContact.ContactId,
                ContactSaveMode = currectContact.ContactSaveMode,
                Extensions = currectContact.Extensions,
                Facets = currectContact.Facets,
                Identifiers = currectContact.Identifiers,
                IsTemporaryInstance = currectContact.IsTemporaryInstance,
                System = currectContact.System,
                Tags = currectContact.Tags
            };


            var items = Tracker.Current.Interaction.Profiles.GetProfileNames();

            List<ExtraBehaviorProfileContext> newProfile = (from profileItem in items
                select Tracker.Current.Interaction.Profiles[profileItem]
                into profile
                let scores = profile.ToList()
                select new ExtraBehaviorProfileContext
                {
                    PatterneName = profile.PatternLabel, ProfileName = profile.ProfileName, Total = profile.Total, PatternId = profile.PatternId.ToId(), NumberOfTimesScored = profile.Count, StringScore = scores
                }).ToList();

            contact.NewProfiles = newProfile;

            


            return contact;
        }

        public string GetCampaign(CurrentInteraction currentInteraction)
        {
            if (currentInteraction.CampaignId.HasValue)
            {
                Item campaign = Sitecore.Context.Database.GetItem(currentInteraction.CampaignId.ToId());
                if (campaign != null) return campaign.Name;
            }

            return "Current Campaign Empty";
        }

        //public List<PatternMatch> LoadPatterns()
        //{
        //    List<PatternMatch> patternMatches = new List<PatternMatch>();

        //    if (Tracker.IsActive)
        //    {
        //        if (SiteConfiguration.GetSiteConfigurationItem() != null)
        //        {
        //            MultilistField profiles = SiteConfiguration.GetSiteConfigurationItem().Fields["Visible Profiles"];
        //            foreach (Item visibleProfile in profiles.GetItems())
        //            {
        //                Item visibleProfileItem = Sitecore.Context.Database.GetItem(visibleProfile.ID);
        //                if (visibleProfileItem != null)
        //                {
        //                    // show the pattern match if there is one.
        //                    var userPattern = Tracker.Current.Interaction.Profiles[visibleProfileItem.Name];
        //                    if (userPattern != null)
        //                    {
        //                        // load the details about the matching pattern
        //                        if (userPattern.PatternId.HasValue)
        //                        {
        //                            Item matchingPattern = Sitecore.Context.Database.GetItem(userPattern.PatternId.ToId());
        //                            if (matchingPattern != null)
        //                            {
        //                                Sitecore.Data.Items.MediaItem image =
        //                                    new Sitecore.Data.Items.MediaItem(
        //                                        ((ImageField)matchingPattern.Fields["Image"]).MediaItem);
        //                                string src = Sitecore.StringUtil.EnsurePrefix('/',
        //                                    Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
        //                                patternMatches.Add(new PatternMatch(visibleProfileItem["Name"],
        //                                    matchingPattern.Name, src));
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return patternMatches;
        //}

        public List<GenericLink> LoadPages()
        {
            //var a = Tracker.Current.Interaction.GetPages().Reverse();
            List<GenericLink> pagesViewed = new List<GenericLink>();
            foreach (IPageContext page in Tracker.Current.Interaction.GetPages())
            {
                GenericLink link = new GenericLink(CleanPageName(page), page.Url.Path, false);
                pagesViewed.Add(link);
            }
            pagesViewed.Reverse();
            return pagesViewed;
        }

        public List<string> LoadGoals()
        {
            List<string> goals = new List<string>();

            var conversions = (from page in Tracker.Current.Interaction.GetPages()
                               from pageEventData in page.PageEvents
                               where pageEventData.IsGoal
                               select pageEventData).ToList();

            if (conversions.Any())
            {
                conversions.Reverse();
                foreach (var goal in conversions)
                {
                    goals.Add(String.Format("{0} ({1})", goal.Name, goal.Value));
                }
            }
            else
            {
                goals.Add("No Goals");
            }

            return goals;
        }

        public List<string> LoadEngagementStates()
        {
            List<string> states = new List<string>();

            try
            {
                var engagementstates = AutomationStateManager.Create(Tracker.Current.Contact).GetAutomationStates();

                if (engagementstates.Any())
                {
                    foreach (
                        AutomationStateContext context in
                            AutomationStateManager.Create(Tracker.Current.Contact).GetAutomationStates())
                    {
                        states.Add(String.Format("{0}: {1}", context.PlanItem.DisplayName, context.StateItem.DisplayName));
                    }
                }
                else
                {
                    states.Add("No Engagement States");
                }
            }
            catch (Exception)
            {
                states.Add("Unable to load Engagement States");
            }
            return states;
        }

        private string CleanPageName(IPageContext p)
        {
            string pageName = p.Url.Path.Replace("/en", "/").Replace("//", "/").Remove(0, 1).Replace(".aspx", "");
            if (pageName == String.Empty || pageName == "en") pageName = "Home";
            if (pageName.Contains("/"))
            {
                //pageName.Substring(0, pageName.IndexOf("/") + 1) +
                pageName = "..." + pageName.Substring(pageName.LastIndexOf("/"));
            }
            return (pageName.Length < 27) ? String.Format("{0} ({1}s)", pageName, (p.Duration / 1000.0).ToString("f2")) :
                String.Format("{0}... ({1}s)", pageName.Substring(0, 26), (p.Duration / 1000.0).ToString("f2"));
        }

        #endregion
    }

    [Serializable]
    public class TrackerDto
    {
        public Contact Contact { get; set; }
        public CurrentPage CurrentPage { get; set; }
        public Interactions Interaction { get; set; }

        public bool IsActive { get; set; }

        public Session Session { get; set; }

        public string Campaign { get; set; }

        //public List<PatternMatch> PatternMatches
        //{
        //    get
        //    {
        //        VisitorInformation visitorInformation = new VisitorInformation();
        //        return visitorInformation.LoadPatterns();
        //    }
        //}

        public List<GenericLink> PagesViewed
        {
            get
            {
                VisitorInformation visitorInformation = new VisitorInformation();
                return visitorInformation.LoadPages();
            }
        }

        public List<string> GoalsList
        {
            get
            {
                VisitorInformation visitorInformation = new VisitorInformation();
                return visitorInformation.LoadGoals();
            }
        }

        public List<string> EngagementStates
        {
            get
            {
                VisitorInformation visitorInformation = new VisitorInformation();
                return visitorInformation.LoadEngagementStates();
            }
        }

    }

    public class Contact
    {
        //public IDictionary<string, object> Attachments { get; set; }
        //public BehaviorProfiles BehaviorProfiles { get; set; }
        public Guid ContactId { get; set; }
        public Sitecore.Analytics.Model.ContactSaveMode ContactSaveMode { get; set; }
        public IContactExtensionsContext Extensions { get; set; }
        public IReadOnlyDictionary<string, IFacet> Facets { get; set; }
        public IContactIdentifiersContext Identifiers { get; set; }
        public bool IsTemporaryInstance { get; set; }
        public IContactSystemInfoContext System { get; set; }
        public IContactTagsContext Tags { get; set; }
        public List<ExtraBehaviorProfileContext> Profiles { get; set; }
        public List<ExtraBehaviorProfileContext> NewProfiles { get; set; }

    }

    public class BehaviorProfiles
    {
        public IEnumerable<IBehaviorProfileContext> Profiles { get; set; }
        
    }

    public class ExtraBehaviorProfileContext
    {
        public string ProfileName { get; set; }
        public ID Id { get; set; }
        public int NumberOfTimesScored { get; set; }
        public double Total { get; set; }
        public IEnumerable<KeyValuePair<ID, float>> Scores { get; set; }
        public IEnumerable<KeyValuePair<string, float>> StringScore { get; set; }
        public string PatterneName { get; set; }
        public ID PatternId { get; set; }
    }

    public class CurrentPage
    {
        public Sitecore.Analytics.Model.UrlData Url { get; set; }
    }

    public class Interactions
    {
        public Sitecore.Analytics.Core.BrowserInformationBase BrowserInfo { get; set; }

        public Guid? CampaignId { get; set; }

        public Guid ContactId { get; set; }

        public Guid ChannelId { get; set; }

        public int ContactVisitIndex { get; set; }

        public IDictionary<string, object> CustomValues { get; set; }

        public Guid DeviceId { get; set; }

        public ContactLocation GeoData { get; set; }

        public bool HasGeoIpData { get; set; }

        public Guid InteractionId { get; set; }

        public byte[] Ip { get; set; }

        public string Keywords { get; set; }

        public string Language { get; set; }

        public IVisitProfiles Profiles { get; set; }

        public Sitecore.Analytics.Core.ScreenInformationBase ScreenInfo { get; set; }

        public string SiteName { get; set; }

        public int Value { get; set; }
    }

    public class Session
    {
        public Interactions Interaction { get; set; }
        public Contact Contact { get; set; }
    }

    public class GenericLink
    {
        public GenericLink(string title, string url, bool openInBlankWindow)
        {
            Title = title;
            Url = url;
            OpenInBlankWindow = openInBlankWindow;
        }

        public string Title
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public bool OpenInBlankWindow
        {
            get;
            set;
        }
    }
}
