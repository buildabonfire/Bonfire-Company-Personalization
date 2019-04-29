using System;

namespace Bonfire.Foundation.XConnectService.Services
{
    public interface IEventTrackerService
    {
        bool IsActive { get; }
        void TrackPageEvent(Guid pageEventId, string text = null, string data = null, string dataKey = null, int? value = null);
        void TrackGoal(Guid goalId, string text = null, string data = null, string dataKey = null, int? value = null);
        void TrackOutcome(Guid outComeDefinitionId);
        void IdentifyContact(string source, string identifier);
    }
}