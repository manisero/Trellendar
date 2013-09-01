using AutoMoq;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Tests
{
    [TestFixture]
    public abstract class TestsBase
    {
        protected AutoMoqer AutoMoqer { get; private set; }

        [SetUp]
        public void SetUp()
        {
            AutoMoqer = new AutoMoqer();
        }

        protected void MockUserContext(User user)
        {
            AutoMoqer.SetInstance(new UserContext(user));
        }

        protected void MockUserContext(UserPreferences userPreferences)
        {
            AutoMoqer.SetInstance(new UserContext(new User { UserPreferences = userPreferences }));
        }

        protected void VerifyMock<TMock>() where TMock : class
        {
            AutoMoqer.GetMock<TMock>().VerifyAll();
        }
    }
}
