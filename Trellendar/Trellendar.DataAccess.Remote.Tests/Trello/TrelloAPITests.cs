using NUnit.Framework;
using Trellendar.Core.Serialization;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Remote.Trello.RestClients;
using Trellendar.DataAccess.Remote.Trello._Impl;
using Trellendar.Domain;
using System.Linq;

namespace Trellendar.DataAccess.Remote.Tests.Trello
{
	[TestFixture]
	public class TrelloAPITests : TestsBase
	{
		private const string ACCESS_TOKEN = "confidential";

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			var accessTokenProviderFactoryMock = MockAccessTokenProviderFactory(DomainType.Trello, true, ACCESS_TOKEN);

			AutoMoqer.GetMock<IRestClientFactory>()
			         .Setup(x => x.CreateAuthorizedClient(DomainType.Trello))
			         .Returns(new AuthorizedTrelloClient(accessTokenProviderFactoryMock.Object));

			AutoMoqer.SetInstance<IJsonSerializer>(new JsonSerializer());
		}

		[Test]
		public void gets_boards()
		{
			// Act
			var result = AutoMoqer.Resolve<TrelloAPI>().GetBoards();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(5, result.Count());
			Assert.IsNotNull(result.SingleOrDefault(x => x.Name == "Private"));
			Assert.IsNotNull(result.SingleOrDefault(x => x.Name == "Semestr VIII"));
			Assert.IsNotNull(result.SingleOrDefault(x => x.Name == "Test"));
			Assert.IsNotNull(result.SingleOrDefault(x => x.Name == "Trellendar"));
			Assert.IsNotNull(result.SingleOrDefault(x => x.Name == "Welcome Board"));
		}

		[Test]
		public void gets_board()
		{
			// Arrange
			var boardId = "confidential";

			// Act
			var result = AutoMoqer.Resolve<TrelloAPI>().GetBoard(boardId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(boardId, result.Id);
			Assert.AreEqual("Private", result.Name);
			Assert.AreEqual(false, result.IsClosed);

			Assert.IsNotNull(result.Lists);
			Assert.AreEqual(3, result.Lists.Count);
			// TODO: Assert Lists
		}
	}
}
