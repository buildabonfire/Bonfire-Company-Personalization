using Sitecore.Analytics.Model;
using Sitecore.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;

namespace Bonfire.Kickfire.Analytics.ExperienceAnalytics.Aggregation.Dimensions
{
    internal class ByCompany : PageEventDimensionBase
    {
        private readonly Guid redirectEventId = Constants.IDs.Events.CompanyEvent.Guid;

        public ByCompany(Guid dimensionId)
            : base(dimensionId)
        {
        }

        public override IEnumerable<string> ExtractDimensionKeys(PageEventData pageEvent)
        {
            Assert.IsNotNull(pageEvent, "pageEvent");

            var redirectItem = Sitecore.Data.Database.GetDatabase("master").GetItem(pageEvent.ItemId.ToString());

            yield return redirectItem.Paths.ContentPath;
        }

        public override bool Filter(PageEventData pageEvent)
        {
            Assert.IsNotNull(pageEvent, "pageEvent");
            return pageEvent.PageEventDefinitionId.Equals(this.redirectEventId);
        }

    }
}
