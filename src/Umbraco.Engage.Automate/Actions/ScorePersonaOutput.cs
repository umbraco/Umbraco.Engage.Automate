namespace Umbraco.Engage.Automate.Actions;

/// <summary>Output produced by the <see cref="ScorePersonaAction"/>.</summary>
public sealed class ScorePersonaOutput
{
    public Guid VisitorExternalId { get; init; }
    public Guid PersonaKey { get; init; }
    public int Score { get; init; }
    public bool IsLocked { get; init; }
}
