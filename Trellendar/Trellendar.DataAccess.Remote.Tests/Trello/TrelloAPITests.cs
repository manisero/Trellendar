using System;
using NUnit.Framework;
using Trellendar.Core.Serialization;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Remote.Trello.RestClients;
using Trellendar.DataAccess.Remote.Trello._Impl;
using Trellendar.Domain;
using System.Linq;
using Trellendar.Domain.Trello;
using List = Trellendar.Domain.Trello.List;

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

			// Lists:
			Assert.IsNotNull(result.Lists);
			Assert.AreEqual(3, result.Lists.Count);

			var todoList = GetAndAssertList(result, "To Do", "5225a72859447646530060b9");
			var doingList = GetAndAssertList(result, "Doing", "5225a72859447646530060ba");
			var doneList = GetAndAssertList(result, "Done", "5225a72859447646530060bb");

			// Cards:
			Assert.IsNotNull(result.Cards);
			Assert.AreEqual(3, result.Cards.Count);

			var cardWithDue = GetAndAssertCard(result, "Card with due", "5229c59f3c3b48214f001f5d", todoList, new DateTime(2013, 09, 04, 9, 25, 0), "test card with due",
                                               "https://trello.com/c/kwgU1oIs", false, new DateTime(2013, 09, 06, 12, 14, 16, 309));

			var cardWithChecklist = GetAndAssertCard(result, "Card with checklist", "5229c5a928ad0cf33200041b", doingList, null, string.Empty,
			                                         "https://trello.com/c/AOIvyx7c", false, new DateTime(2013, 09, 06, 12, 57, 16, 564));

			var archivedCard = GetAndAssertCard(result, "Archived card", "5229ce1474d337d96c00360d", doneList, null, string.Empty,
			                                    "https://trello.com/c/bNkRddA2", true, new DateTime(2013, 9, 6, 12, 44, 9, 261));

			// Checklists:
			Assert.IsNotNull(result.CheckLists);
			Assert.AreEqual(1, result.CheckLists.Count);

			var checklist = GetAndAssertChecklist(result, "test checklist", "5229cf33b4c7de41530037c9", cardWithChecklist);

			Assert.IsNotNull(checklist.CheckItems);
			Assert.AreEqual(3, checklist.CheckItems.Count);
			AssertCheckItem(checklist.CheckItems[0], "5229cf3afeb8310a040036d8", "test item 1", checklist, "complete");
			AssertCheckItem(checklist.CheckItems[1], "5229cf3cd4135ad21b003583", "test item 2", checklist, "incomplete");
			AssertCheckItem(checklist.CheckItems[2], "5229cf410a71027353003d42", "test item 3", checklist, "incomplete");
		}

		private List GetAndAssertList(Board board, string name, string id)
		{
			var list = board.Lists.SingleOrDefault(x => x.Name == name);
			Assert.IsNotNull(list);
			Assert.AreEqual(id, list.Id);

			return list;
		}

		private Card GetAndAssertCard(Board board, string name, string id, List list, DateTime? due, string description, string url, bool closed, DateTime dateLastActivity)
		{
			var card = board.Cards.SingleOrDefault(x => x.Name == name);
			Assert.IsNotNull(card);
			Assert.AreEqual(id, card.Id);
			Assert.AreSame(list, card.List);
			Assert.AreEqual(due, card.Due);
			Assert.AreEqual(description, card.Description);
			Assert.AreEqual(url, card.Url);
			Assert.AreEqual(closed, card.Closed);
			Assert.AreEqual(dateLastActivity, card.DateLastActivity);

			return card;
		}

		private CheckList GetAndAssertChecklist(Board board, string name, string id, Card card)
		{
			var checklist = board.CheckLists.SingleOrDefault(x => x.Name == name);
			Assert.IsNotNull(checklist);
			Assert.AreEqual(id, checklist.Id);
			Assert.AreSame(card, checklist.Card);

			return checklist;
		}

		private void AssertCheckItem(CheckItem checkItem, string id, string name, CheckList checkList, string state)
		{
			Assert.IsNotNull(checkItem);
			Assert.AreEqual(id, checkItem.Id);
			Assert.AreEqual(name, checkItem.Name);
			Assert.AreEqual(checkList, checkItem.CheckList);
			Assert.AreEqual(state, checkItem.State);
		}
	}
}
