using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class NewSessionStartedTriggerTests
{
    private readonly NewSessionStartedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var timestamp = new DateTime(2026, 1, 15, 14, 0, 0, DateTimeKind.Utc);
        const long sessionId = 1000L;
        const long visitorId = 888L;

        var visitor = new Mock<IVisitor>();
        visitor.Setup(v => v.Id).Returns(visitorId);

        var session = new Mock<ISession>();
        session.Setup(s => s.Id).Returns(sessionId);
        session.Setup(s => s.Timestamp).Returns(timestamp);
        session.Setup(s => s.PageviewCount).Returns(5);
        session.Setup(s => s.Visitor).Returns(visitor.Object);

        var notification = new EngageNewSessionStartedNotification(new AnalyticsNewSessionStartedEvent(session.Object));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<NewSessionStartedTriggerOutput>)events[0]).Output;
        output.SessionId.ShouldBe(sessionId);
        output.SessionTimestamp.ShouldBe(timestamp);
        output.PageviewCount.ShouldBe(5);
        output.VisitorId.ShouldBe(visitorId);
    }

    [Fact]
    public void MapEvent_NullVisitor_VisitorIdIsNull()
    {
        var session = new Mock<ISession>();
        session.Setup(s => s.Id).Returns(1L);
        session.Setup(s => s.Timestamp).Returns(DateTime.UtcNow);
        session.Setup(s => s.PageviewCount).Returns(1);
        session.Setup(s => s.Visitor).Returns((IVisitor?)null);

        var notification = new EngageNewSessionStartedNotification(new AnalyticsNewSessionStartedEvent(session.Object));

        var events = _trigger.MapEvent(notification).ToList();

        ((TriggerEvent<NewSessionStartedTriggerOutput>)events[0]).Output.VisitorId.ShouldBeNull();
    }
}
