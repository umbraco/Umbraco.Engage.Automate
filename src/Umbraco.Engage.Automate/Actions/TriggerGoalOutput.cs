namespace Umbraco.Engage.Automate.Actions;

/// <summary>Output produced by the <see cref="TriggerGoalAction"/>.</summary>
public sealed class TriggerGoalOutput
{
    public Guid GoalKey { get; init; }
    public int Value { get; init; }
}
