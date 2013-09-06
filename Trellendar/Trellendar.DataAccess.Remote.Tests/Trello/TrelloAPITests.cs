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
		private const string ACCESS_TOKEN = "16638490314345dea35f480c55f83786d9a6b2186a4810fd62a6462f4784d514";

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
			Assert.AreEqual(2, result.Count());
			Assert.IsNotNull(result.SingleOrDefault(x => x.Id == "5225a72859447646530060b8" && x.Name == "UnitTestsBoard"));
			Assert.IsNotNull(result.SingleOrDefault(x => x.Id == "5225a6f46a25183531005b8c" && x.Name == "Welcome Board"));
		}

		[Test]
		public void gets_board()
		{
			// Arrange
			var boardId = "5225a72859447646530060b8";

			// Act
			var result = AutoMoqer.Resolve<TrelloAPI>().GetBoard(boardId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(boardId, result.Id);
			Assert.AreEqual("UnitTestsBoard", result.Name);
			Assert.AreEqual(false, result.IsClosed);

			Assert.IsNotNull(result.Lists);
			Assert.AreEqual(3, result.Lists.Count);
			// TODO: Assert Lists
		}
	}
}
