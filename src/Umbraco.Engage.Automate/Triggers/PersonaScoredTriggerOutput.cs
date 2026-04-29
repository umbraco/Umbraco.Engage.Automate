namespace Umbraco.Engage.Automate.Triggers;

public sealed class PersonaScoredTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long PersonaId { get; init; }
    public int Score { get; init; }
    public bool IsLocked { get; init; }
}
