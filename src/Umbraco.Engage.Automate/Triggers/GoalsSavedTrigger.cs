using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.goalsSaved", "Goals Saved",
    Description = "Fires when goals are saved.",
    Group = "Engage",
    Icon = "icon-trophy")]
public sealed class GoalsSavedTrigger
    : NotificationTriggerBase<object, GoalsSavedTriggerOutput, EngageGoalsSavedNotification>
{
    public GoalsSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageGoalsSavedNotification notification)
    {
        yield return new TriggerEvent<GoalsSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new GoalsSavedTriggerOutput(),
        };
    }
}
