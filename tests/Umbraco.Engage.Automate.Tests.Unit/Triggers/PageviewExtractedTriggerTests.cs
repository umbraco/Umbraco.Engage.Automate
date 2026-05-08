using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class PageviewExtractedTriggerTests
{
    private readonly PageviewExtractedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var pageviewGuid = Guid.NewGuid();
        var timestamp = new DateTime(2026, 1, 15, 13, 45, 0, DateTimeKind.Utc);
        const long pageviewId = 200L;
        const long sessionId = 500L;

        var session = new Mock<ISession>();
        session.Setup(s => s.Id).Returns(sessionId);

        var pageview = new Mock<IPageview>();
        pageview.Setup(p => p.Id).Returns(pageviewId);
        pageview.Setup(p => p.Guid).Returns(pageviewGuid);
        pageview.Setup(p => p.Timestamp).Returns(timestamp);
        pageview.Setup(p => p.Session).Returns(session.Object);
        pageview.Setup(p => p.WasPersonalized).Returns(true);

        var notification = new EngagePageviewExtractedNotification(new AnalyticsPageviewExtractedEvent(pageview.Object));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<PageviewExtractedTriggerOutput>)events[0]).Output;
        output.PageviewId.ShouldBe(pageviewId);
        output.PageviewGuid.ShouldBe(pageviewGuid);
        output.Timestamp.ShouldBe(timestamp);
        output.SessionId.ShouldBe(sessionId);
        output.WasPersonalized.ShouldBeTrue();
    }

    [Fact]
    public void MapEvent_NotPersonalized_WasPersonalizedFalse()
    {
        var session = new Mock<ISession>();
        session.Setup(s => s.Id).Returns(1L);

        var pageview = new Mock<IPageview>();
        pageview.Setup(p => p.Id).Returns(1L);
        pageview.Setup(p => p.Guid).Returns(Guid.NewGuid());
        pageview.Setup(p => p.Timestamp).Returns(DateTime.UtcNow);
        pageview.Setup(p => p.Session).Returns(session.Object);
        pageview.Setup(p => p.WasPersonalized).Returns(false);

        var notification = new EngagePageviewExtractedNotification(new AnalyticsPageviewExtractedEvent(pageview.Object));

        var events = _trigger.MapEvent(notification).ToList();

        ((TriggerEvent<PageviewExtractedTriggerOutput>)events[0]).Output.WasPersonalized.ShouldBeFalse();
    }
}
