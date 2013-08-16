﻿using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.CalendarSynchronization.Parsers;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Parsers
{
    [TestFixture]
    public class LocationParserTests : TestsBase
    {
        [Test]
        [Sequential]
        public void parses_single_location(
            [Values("{", "<location>", "[location]")] string beginningMarker,
            [Values("}", "</location>", "[endoflocation]")] string endMarker,
            [Values("text {somewhere} text",
                    "text <location>elsewhere</location> text",
                    "text [location]somewhere else[endoflocation] text")] 
                    string text,
            [Values("somewhere", "elsewhere", "somewhere else")] string expectedLocation)
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew()
                                                      .With(x => x.LocationTextBeginningMarker = beginningMarker)
                                                      .With(x => x.LocationTextEndMarker = endMarker)
                                                      .Build();

            // Act
            var result = AutoMoqer.Resolve<LocationParser>().Parse(text, preferences);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLocation, result.Value);
        }

        [Test]
        [Sequential]
        public void handles_multiple_markers_in_text(
            [Values("{", "<location>")] string beginningMarker,
            [Values("}", "</location>")] string endMarker,
            [Values("text {location1} text {location2} text",
                    "text <location>location1</location> text <location>location2</location> text")]
                    string text,
            [Values("location1", "location1")] string expectedLocation)
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew()
                                                      .With(x => x.LocationTextBeginningMarker = beginningMarker)
                                                      .With(x => x.LocationTextEndMarker = endMarker)
                                                      .Build();

            // Act
            var result = AutoMoqer.Resolve<LocationParser>().Parse(text, preferences);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLocation, result.Value);
        }
    }
}