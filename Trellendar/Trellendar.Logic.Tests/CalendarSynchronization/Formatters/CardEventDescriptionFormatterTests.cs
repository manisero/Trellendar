﻿using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters._Impl;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatters
{
    [TestFixture]
    public class CardEventDescriptionFormatterTests : TestsBase
    {
        [Test]
        public void appends_card_url(
            [Values("description")] string cardDescription,
            [Values("url")] string cardUrl,
            [Values("description\n\nLink: url")] string expectedDescription)
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.Description = cardDescription)
                                    .With(x => x.Url = cardUrl)
                                    .Build();

            // Act
            var result = AutoMoqer.Resolve<CardEventDescriptionFormatter>().Format(card);

            // Assert
            Assert.AreEqual(expectedDescription, result);
        }
    }
}
