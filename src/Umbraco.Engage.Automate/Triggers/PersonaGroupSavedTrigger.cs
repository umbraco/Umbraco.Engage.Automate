using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.personaGroupSaved", "Persona Group Saved",
    Description = "Fires when a persona group is saved.",
    Group = "Engage",
    Icon = "icon-user",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class PersonaGroupSavedTrigger
    : NotificationTriggerBase<object, PersonaGroupSavedTriggerOutput, EngagePersonaGroupSavedNotification>
{
    public PersonaGroupSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngagePersonaGroupSavedNotification notification)
    {
        yield return new TriggerEvent<PersonaGroupSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new PersonaGroupSavedTriggerOutput(),
        };
    }
}
