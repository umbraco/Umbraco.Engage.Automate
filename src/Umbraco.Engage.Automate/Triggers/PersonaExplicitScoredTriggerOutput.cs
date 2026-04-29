namespace Umbraco.Engage.Automate.Triggers;

public sealed class PersonaExplicitScoredTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long PersonaId { get; init; }
    public long GroupId { get; init; }
}
