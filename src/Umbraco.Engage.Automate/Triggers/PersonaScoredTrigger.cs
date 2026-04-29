using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.personaScored", "Persona Scored",
    Description = "Fires when a visitor's persona score changes.",
    Group = "Engage",
    Icon = "icon-user")]
public sealed class PersonaScoredTrigger
    : NotificationTriggerBase<object, PersonaScoredTriggerOutput, EngagePersonaScoredNotification>
{
    public PersonaScoredTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngagePersonaScoredNotification notification)
    {
        yield return new TriggerEvent<PersonaScoredTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "visitor",
            IdempotencyKey = GenerateIdempotencyKey(notification.VisitorId, notification.PersonaId, notification.Score),
            Output = new PersonaScoredTriggerOutput
            {
                VisitorId = notification.VisitorId,
                PersonaId = notification.PersonaId,
                Score = notification.Score,
                IsLocked = notification.IsLocked,
            },
        };
    }
}
