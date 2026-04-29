namespace Umbraco.Engage.Automate.Triggers;

public sealed class PersonaExplicitScoreRemovedTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long GroupId { get; init; }
    public long PersonaId { get; init; }
}
