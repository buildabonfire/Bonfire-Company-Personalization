using System;
using Sitecore.Analytics;

namespace Bonfire.Kickfire.Analytics.Helpers.Events
{
    public static class PageEvent
    {
        private static readonly Guid CompanyEvent = Constants.IDs.Events.CompanyEvent.Guid;

        public static void RegisterCompanyEvent(string companyName)
        {
            var item = Sitecore.Context.Item;

            var pageEventModel = new Sitecore.Analytics.Model.PageEventData()
            {
                PageEventDefinitionId = CompanyEvent,
                ItemId = item.ID.Guid,
                Name = "Company Lookup",
                DateTime = DateTime.UtcNow,
                Text = $"Comopany {companyName} started a visit on the site."
            };

            var pageEventData = new Sitecore.Analytics.Data.PageEventData(pageEventModel);

            Tracker.Current.CurrentPage.Item = new Sitecore.Analytics.Model.ItemData
            {
                Id = item.ID.Guid,
                Language = item.Language.Name,
                Version = item.Version.Number
            };

            Tracker.Current.CurrentPage.Register(pageEventData);
            Tracker.Current.Interaction.AcceptModifications();
        }
    }
}
