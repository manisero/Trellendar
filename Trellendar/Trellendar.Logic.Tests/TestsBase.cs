using AutoMoq;
using NUnit.Framework;

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
    }
}
