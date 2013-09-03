using AutoMoq;
using NUnit.Framework;

namespace Trellendar.TestsCore
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

        protected void VerifyMock<TMock>() where TMock : class
        {
            AutoMoqer.GetMock<TMock>().VerifyAll();
        }
    }
}
