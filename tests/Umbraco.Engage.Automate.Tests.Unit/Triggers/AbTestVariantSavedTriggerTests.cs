using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class AbTestVariantSavedTriggerTests
{
    private readonly AbTestVariantSavedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAbTestVariantId()
    {
        const long variantId = 77L;
        var notification = new EngageAbTestVariantSavedNotification(new AbTestVariantSavedEvent(variantId));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();
        ((TriggerEvent<AbTestVariantSavedTriggerOutput>)events[0]).Output.AbTestVariantId.ShouldBe(variantId);
    }
}
