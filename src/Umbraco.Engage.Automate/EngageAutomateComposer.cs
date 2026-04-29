using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Engage.Automate.Notifications.Handlers;

namespace Umbraco.Engage.Automate;

/// <summary>
/// Registers Engage event bridge handlers that republish Engage events
/// as Umbraco CMS notifications for Automate's trigger pipeline.
/// </summary>
public sealed class EngageAutomateComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // Register all bridge handlers in DI as singletons
        builder.Services.AddSingleton<AbTestSavedBridgeHandler>();
        builder.Services.AddSingleton<AbTestScheduledBridgeHandler>();
        builder.Services.AddSingleton<AbTestStartedBridgeHandler>();
        builder.Services.AddSingleton<AbTestStoppedBridgeHandler>();
        builder.Services.AddSingleton<AbTestVariantSavedBridgeHandler>();
        builder.Services.AddSingleton<NewSessionStartedBridgeHandler>();
        builder.Services.AddSingleton<PageviewExtractedBridgeHandler>();
        builder.Services.AddSingleton<AppliedPersonalizationSavedBridgeHandler>();
        builder.Services.AddSingleton<SegmentSavedBridgeHandler>();
        builder.Services.AddSingleton<SegmentDeletedBridgeHandler>();
        builder.Services.AddSingleton<CustomerJourneyGroupSavedBridgeHandler>();
        builder.Services.AddSingleton<CustomerJourneyStepScoredBridgeHandler>();
        builder.Services.AddSingleton<CustomerJourneyStepExplicitScoredBridgeHandler>();
        builder.Services.AddSingleton<CustomerJourneyStepExplicitScoreRemovedBridgeHandler>();
        builder.Services.AddSingleton<PersonaGroupSavedBridgeHandler>();
        builder.Services.AddSingleton<PersonaScoredBridgeHandler>();
        builder.Services.AddSingleton<PersonaExplicitScoredBridgeHandler>();
        builder.Services.AddSingleton<PersonaExplicitScoreRemovedBridgeHandler>();
        builder.Services.AddSingleton<GoalsSavedBridgeHandler>();
        builder.Services.AddSingleton<CustomGoalCompletedBridgeHandler>();
        builder.Services.AddSingleton<ClientSideGoalCompletedBridgeHandler>();
        builder.Services.AddSingleton<CampaignGroupSavedBridgeHandler>();

        // The component wires up bridge handlers with Engage's SystemEventService
        builder.Components().Append<EngageAutomateComponent>();
    }
}
