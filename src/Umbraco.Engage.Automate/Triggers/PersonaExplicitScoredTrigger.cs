using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.personaExplicitScored", "Persona Explicitly Assigned",
    Description = "Fires when a visitor is manually locked to a persona.",
    Group = "Engage",
    Icon = "icon-user")]
public sealed class PersonaExplicitScoredTrigger
    : NotificationTriggerBase<object, PersonaExplicitScoredTriggerOutput, EngagePersonaExplicitScoredNotification>
{
    public PersonaExplicitScoredTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngagePersonaExplicitScoredNotification notification)
    {
        yield return new TriggerEvent<PersonaExplicitScoredTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "system",
            Output = new PersonaExplicitScoredTriggerOutput
            {
                VisitorId = notification.VisitorId,
                PersonaId = notification.Entity.PersonaId,
                GroupId = notification.Entity.GroupId,
            },
        };
    }
}
