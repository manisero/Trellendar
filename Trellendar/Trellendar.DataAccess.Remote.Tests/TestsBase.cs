using Moq;
using NUnit.Framework;
using Trellendar.Domain;

namespace Trellendar.DataAccess.Remote.Tests
{
	[TestFixture]
	public class TestsBase : TestsCore.TestsBase
	{
		protected Mock<IAccessTokenProviderFactory> MockAccessTokenProviderFactory(DomainType domainType, bool canProvideAccessToken, string accessToken = null)
		{
			var accessTokenProviderMock = MockAccessTokenProvider(canProvideAccessToken, accessToken);
			var mock = AutoMoqer.GetMock<IAccessTokenProviderFactory>();
			mock.Setup(x => x.Create(domainType)).Returns(accessTokenProviderMock.Object);

			return mock;
		}

		protected Mock<IAccessTokenProvider> MockAccessTokenProvider(bool canProvideAccessToken, string accessToken = null)
		{
			var mock = AutoMoqer.GetMock<IAccessTokenProvider>();
			mock.Setup(x => x.CanProvideAccessToken).Returns(canProvideAccessToken);

			if (canProvideAccessToken)
			{
				mock.Setup(x => x.GetAccessToken()).Returns(accessToken);
			}

			return mock;
		}
	}
}
