using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.Core.Runs;
using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Actions;
using Umbraco.Engage.Infrastructure.Analytics.Goals;

namespace Umbraco.Engage.Automate.Tests.Unit.Actions;

public class TriggerGoalActionTests
{
    private readonly Mock<IGoalService> _goalService = new();

    [Fact]
    public async Task InvalidGoalKey_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<TriggerGoalAction>()
            .WithService(_goalService.Object)
            .WithSettings(new TriggerGoalSettings { GoalKey = "not-a-guid" })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task EmptyGoalKey_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<TriggerGoalAction>()
            .WithService(_goalService.Object)
            .WithSettings(new TriggerGoalSettings { GoalKey = string.Empty })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task GoalServiceReturnsFalse_ReturnsValidationError()
    {
        var goalKey = Guid.NewGuid();
        _goalService.Setup(s => s.TriggerGoalAsync(goalKey, 0)).ReturnsAsync(false);

        var result = await ActionTestHarness.For<TriggerGoalAction>()
            .WithService(_goalService.Object)
            .WithSettings(new TriggerGoalSettings { GoalKey = goalKey.ToString(), Value = 0 })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task ValidGoalKey_ReturnsSuccess()
    {
        var goalKey = Guid.NewGuid();
        _goalService.Setup(s => s.TriggerGoalAsync(goalKey, 5)).ReturnsAsync(true);

        var result = await ActionTestHarness.For<TriggerGoalAction>()
            .WithService(_goalService.Object)
            .WithSettings(new TriggerGoalSettings { GoalKey = goalKey.ToString(), Value = 5 })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Success);

        var output = (result.OutputData as TriggerGoalOutput)!;
        output.GoalKey.ShouldBe(goalKey);
        output.Value.ShouldBe(5);
    }
}
