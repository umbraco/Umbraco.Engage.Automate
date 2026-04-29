using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.Core.Runs;
using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Actions;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Personalization.Services;

namespace Umbraco.Engage.Automate.Tests.Unit.Actions;

public class ScorePersonaActionTests
{
    private readonly Mock<IPersonaService> _personaService = new();

    [Fact]
    public async Task InvalidVisitorExternalId_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<ScorePersonaAction>()
            .WithService(_personaService.Object)
            .WithSettings(new ScorePersonaSettings
            {
                VisitorExternalId = "not-a-guid",
                PersonaKey = Guid.NewGuid().ToString(),
                Score = 1,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task InvalidPersonaKey_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<ScorePersonaAction>()
            .WithService(_personaService.Object)
            .WithSettings(new ScorePersonaSettings
            {
                VisitorExternalId = Guid.NewGuid().ToString(),
                PersonaKey = "not-a-guid",
                Score = 1,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task ZeroScoreWithoutLock_ReturnsValidationError()
    {
        var result = await ActionTestHarness.For<ScorePersonaAction>()
            .WithService(_personaService.Object)
            .WithSettings(new ScorePersonaSettings
            {
                VisitorExternalId = Guid.NewGuid().ToString(),
                PersonaKey = Guid.NewGuid().ToString(),
                Score = 0,
                IsLocked = false,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Failed);
        result.ErrorCategory.ShouldBe(StepRunErrorCategory.Validation);
    }

    [Fact]
    public async Task ValidSettings_ScoresPersonaAndReturnsSuccess()
    {
        var visitorId = Guid.NewGuid();
        var personaKey = Guid.NewGuid();

        var result = await ActionTestHarness.For<ScorePersonaAction>()
            .WithService(_personaService.Object)
            .WithSettings(new ScorePersonaSettings
            {
                VisitorExternalId = visitorId.ToString(),
                PersonaKey = personaKey.ToString(),
                Score = 10,
                IsLocked = false,
            })
            .ExecuteAsync();

        _personaService.Verify(s => s.ScorePersona(
            visitorId, personaKey, 10, 0, PersonalizationScoreType.Custom, false), Times.Once);

        result.Status.ShouldBe(ActionResultStatus.Succeeded);

        var output = result.Output<ScorePersonaOutput>();
        output.VisitorExternalId.ShouldBe(visitorId);
        output.PersonaKey.ShouldBe(personaKey);
        output.Score.ShouldBe(10);
        output.IsLocked.ShouldBeFalse();
    }

    [Fact]
    public async Task ZeroScoreWithLock_SucceedsAsExplicitAssignment()
    {
        var visitorId = Guid.NewGuid();
        var personaKey = Guid.NewGuid();

        var result = await ActionTestHarness.For<ScorePersonaAction>()
            .WithService(_personaService.Object)
            .WithSettings(new ScorePersonaSettings
            {
                VisitorExternalId = visitorId.ToString(),
                PersonaKey = personaKey.ToString(),
                Score = 0,
                IsLocked = true,
            })
            .ExecuteAsync();

        result.Status.ShouldBe(ActionResultStatus.Succeeded);
        result.Output<ScorePersonaOutput>().IsLocked.ShouldBeTrue();
    }
}
