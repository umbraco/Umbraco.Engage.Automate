using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.Core.Runs;
using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Actions;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Personalization.Services;

namespace Umbraco.Engage.Automate.Tests.Unit.Actions;

public class ScoreCustomerJourneyStepActionTests
{
    private readonly Mock<ICustomerJourneyService> _customerJourneyService = new();

    [Fact]
    public async Task InvalidVisitorExternalId_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<ScoreCustomerJourneyStepAction>()
            .WithService(_customerJourneyService.Object)
            .WithSettings(new ScoreCustomerJourneyStepSettings
            {
                VisitorExternalId = "not-a-guid",
                StepKey = Guid.NewGuid().ToString(),
                Score = 1,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task InvalidStepKey_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<ScoreCustomerJourneyStepAction>()
            .WithService(_customerJourneyService.Object)
            .WithSettings(new ScoreCustomerJourneyStepSettings
            {
                VisitorExternalId = Guid.NewGuid().ToString(),
                StepKey = "not-a-guid",
                Score = 1,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task ZeroScoreWithoutLock_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<ScoreCustomerJourneyStepAction>()
            .WithService(_customerJourneyService.Object)
            .WithSettings(new ScoreCustomerJourneyStepSettings
            {
                VisitorExternalId = Guid.NewGuid().ToString(),
                StepKey = Guid.NewGuid().ToString(),
                Score = 0,
                IsLocked = false,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task ValidSettings_ScoresStepAndReturnsSuccess()
    {
        var visitorId = Guid.NewGuid();
        var stepKey = Guid.NewGuid();

        var result = await ActionTestHarness.For<ScoreCustomerJourneyStepAction>()
            .WithService(_customerJourneyService.Object)
            .WithSettings(new ScoreCustomerJourneyStepSettings
            {
                VisitorExternalId = visitorId.ToString(),
                StepKey = stepKey.ToString(),
                Score = 5,
                IsLocked = false,
            })
            .ExecuteAsync();

        _customerJourneyService.Verify(s => s.ScoreCustomerJourneyStep(
            visitorId, stepKey, 5, 0, PersonalizationScoreType.Custom, false), Times.Once);

        result.Status.ShouldBe(ActionResultStatus.Succeeded);

        var output = result.Output<ScoreCustomerJourneyStepOutput>();
        output.VisitorExternalId.ShouldBe(visitorId);
        output.StepKey.ShouldBe(stepKey);
        output.Score.ShouldBe(5);
    }
}
