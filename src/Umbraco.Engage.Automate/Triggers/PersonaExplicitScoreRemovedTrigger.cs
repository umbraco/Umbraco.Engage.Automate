using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.personaExplicitScoreRemoved", "Persona Assignment Removed",
    Description = "Fires when a visitor's manual persona assignment is removed.",
    Group = "Engage",
    Icon = "icon-user",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class PersonaExplicitScoreRemovedTrigger
    : NotificationTriggerBase<object, PersonaExplicitScoreRemovedTriggerOutput, EngagePersonaExplicitScoreRemovedNotification>
{
    public PersonaExplicitScoreRemovedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngagePersonaExplicitScoreRemovedNotification notification)
    {
        yield return new TriggerEvent<PersonaExplicitScoreRemovedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new PersonaExplicitScoreRemovedTriggerOutput
            {
                VisitorId = notification.VisitorId,
                GroupId = notification.GroupId,
                PersonaId = notification.PersonaId,
            },
        };
    }
}
