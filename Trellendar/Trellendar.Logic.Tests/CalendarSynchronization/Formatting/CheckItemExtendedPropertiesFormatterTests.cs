﻿using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatting.Formatters;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CheckItemExtendedPropertiesFormatterTests : TestsBase
    {
        [Test]
        public void formats_extended_properties()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();

            // Act
            var result = AutoMoqer.Resolve<CheckItemExtendedPropertiesFormatter>().Format(checkItem);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Private);
            Assert.IsTrue(result.Private.ContainsKey(EventExtensions.GENERATED_PROPERTY_KEY));
            Assert.IsTrue(result.Private.ContainsKey(EventExtensions.SOURCE_ID_PROPERTY_KEY));
            Assert.AreEqual(checkItem.Id, result.Private[EventExtensions.SOURCE_ID_PROPERTY_KEY]);
        }
    }
}
