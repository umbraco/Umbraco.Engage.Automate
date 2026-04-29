using Umbraco.Automate.Core.Settings;

namespace Umbraco.Engage.Automate.Actions;

/// <summary>Settings for the <see cref="TriggerGoalAction"/>.</summary>
public sealed class TriggerGoalSettings
{
    [Field(Label = "Goal Key", Description = "The GUID key of the Engage goal to trigger.", SupportsBindings = true)]
    public string GoalKey { get; set; } = string.Empty;

    [Field(Label = "Value", Description = "Optional score value for the goal completion.", SortOrder = 1, SupportsBindings = true)]
    public int Value { get; set; } = 0;
}
