using JustEat.Simples.NotificationStack.Messaging;
using JustEat.Testing;
using NSubstitute;
using NUnit.Framework;

namespace SimpleMessageMule.UnitTests.NotificationStack
{
    public class WhenStartingThenStopping : NotificationStackBaseTest
    {
        private INotificationSubscriber _subscriber1;

        protected override void Given()
        {
            base.Given();
            _subscriber1 = Substitute.For<INotificationSubscriber>();
        }

        protected override void When()
        {
            SystemUnderTest.AddNotificationTopicSubscriber("OrderDispatch", _subscriber1);
            SystemUnderTest.Start();
            SystemUnderTest.Stop();
        }

        [Then]
        public void StateIsNotListening()
        {
            Assert.False(SystemUnderTest.Listening);
        }

        [Then]
        public void CallingStopTwiceDoesNotStopListeningTwice()
        {
            SystemUnderTest.Stop();
            _subscriber1.Received(1).StopListening();
        }
    }
}