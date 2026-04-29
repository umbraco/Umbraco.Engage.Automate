using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.Core.Runs;
using Umbraco.Engage.Infrastructure.Analytics.Goals;

namespace Umbraco.Engage.Automate.Actions;

/// <summary>
/// Triggers an Engage goal by its key.
/// </summary>
[Action("umbracoEngage.triggerGoal", "Trigger Goal",
    Description = "Triggers an Engage goal by its key.",
    Group = "Engage",
    Icon = "icon-trophy")]
public sealed class TriggerGoalAction : ActionBase<TriggerGoalSettings, TriggerGoalOutput>
{
    private readonly IGoalService _goalService;

    public TriggerGoalAction(ActionInfrastructure infrastructure, IGoalService goalService)
        : base(infrastructure) => _goalService = goalService;

    public override Task<ActionResult> ExecuteAsync(ActionContext context, CancellationToken cancellationToken)
    {
        var settings = context.GetSettings<TriggerGoalSettings>();

        if (!Guid.TryParse(settings.GoalKey, out var goalKey))
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("A valid Goal Key (GUID) is required."),
                StepRunErrorCategory.Validation));
        }

        var triggered = _goalService.TriggerGoal(goalKey, settings.Value);

        if (!triggered)
        {
            return Task.FromResult(ActionResult.Failed(
                new InvalidOperationException($"Goal '{goalKey}' could not be triggered. Verify the goal exists and is active."),
                StepRunErrorCategory.Validation));
        }

        return Task.FromResult(Success(new TriggerGoalOutput
        {
            GoalKey = goalKey,
            Value = settings.Value,
        }));
    }
}
