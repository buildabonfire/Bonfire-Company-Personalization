using System;
using Bonfire.Feature.KickfireCore.Helpers;
using Sitecore.XConnect;

namespace Bonfire.Feature.KickfireCore.Models.Goals
{
    public class IdentifiedGoal : Goal
    {
        public static Guid IdentifiedGoalEventDefinitionId
        {
            get
            {
                var goal = AnalyticsConfigurationHelper.IdentificationGoal()?.ID.Guid;
                if (goal == null)
                    return Guid.Empty;

                return goal.Value;
            }
        }

        public IdentifiedGoal(DateTime timestamp) : base(IdentifiedGoalEventDefinitionId, timestamp)
        {
        }
    }
}