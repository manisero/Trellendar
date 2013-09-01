using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Core.Serialization;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization;
using Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization._Impl;

namespace Trellendar.Logic.Tests.Synchronization.BoardCalendarBondSynchronization
{
    [TestFixture]
    public class BoardCalendarBondSerivceTests : TestsBase
    {
        [Test]
        public void updates_bond_properly()
        {
            // Arrange
            var calendar = Builder<Calendar>.CreateNew()
                                            .With(x => x.TimeZone = "test time zone")
                                            .Build();

            var bond = Builder<BoardCalendarBond>.CreateNew().Build();

            MockBoardCalendarContext(bond);
            AutoMoqer.GetMock<IUnitOfWork>().Setup(x => x.SaveChanges());

            // Act
            AutoMoqer.Resolve<BoardCalendarBondSynchronizationService>().UpdateBond(calendar);

            // Assert
            Assert.AreEqual(calendar.TimeZone, bond.CalendarTimeZone);
            VerifyMock<IUnitOfWork>();
        }

        [Test]
        public void updates_bond_settings_properly()
        {
            // Arrange
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                    .With(x => x.Settings = Builder<BoardCalendarBondSettings>.CreateNew().Build())
                                    .Build();

            var configurationCard = Builder<Card>.CreateNew().Build();
            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = new[] { configurationCard })
                                      .Build();

            var newSettings = Builder<BoardCalendarBondSettings>.CreateNew().Build();

            MockBoardCalendarContext(bond);

            AutoMoqer.GetMock<IBoardCalendarBondSynchronizationSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns(configurationCard.Name);

            AutoMoqer.GetMock<IJsonSerializer>()
                     .Setup(x => x.Deserialize<BoardCalendarBondSettings>(configurationCard.Description))
                     .Returns(newSettings);

            AutoMoqer.GetMock<IRepositoryFactory>()
                     .Setup(x => x.Create<BoardCalendarBondSettings>())
                     .Returns(AutoMoqer.GetMock<IRepository<BoardCalendarBondSettings>>().Object);

            AutoMoqer.GetMock<IRepository<BoardCalendarBondSettings>>().Setup(x => x.Remove(bond.Settings));
            AutoMoqer.GetMock<IUnitOfWork>().Setup(x => x.SaveChanges());

            // Act
            AutoMoqer.Resolve<BoardCalendarBondSynchronizationService>().UpdateBondSettings(board);

            // Assert
            Assert.AreSame(newSettings, bond.Settings);

            VerifyMock<IBoardCalendarBondSynchronizationSettingsProvider>();
            VerifyMock<IJsonSerializer>();
            VerifyMock<IRepository<BoardCalendarBondSettings>>();
            VerifyMock<IUnitOfWork>();
        }

        [Test]
        public void does_not_touch_settings_for_null_cards()
        {
            // Arrange
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                    .With(x => x.Settings = settings)
                                    .Build();

            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = null)
                                      .Build();

            MockBoardCalendarContext(bond);

            // Act
            AutoMoqer.Resolve<BoardCalendarBondSynchronizationService>().UpdateBondSettings(board);

            // Assert
            Assert.AreSame(settings, bond.Settings);
        }

        [Test]
        public void does_not_touch_settings_if_configuration_card_not_found()
        {
            // Arrange
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                    .With(x => x.Settings = settings)
                                    .Build();

            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = new[] { Builder<Card>.CreateNew().Build() })
                                      .Build();

            MockBoardCalendarContext(bond);

            AutoMoqer.GetMock<IBoardCalendarBondSynchronizationSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns("configuration card");

            // Act
            AutoMoqer.Resolve<BoardCalendarBondSynchronizationService>().UpdateBondSettings(board);

            // Assert
            Assert.AreSame(settings, bond.Settings);

            VerifyMock<IBoardCalendarBondSynchronizationSettingsProvider>();
        }

        [Test]
        public void does_not_touch_settings_if_configuration_card_content_not_deserializable()
        {
            // Arrange
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                    .With(x => x.Settings = settings)
                                    .Build();

            var configurationCard = Builder<Card>.CreateNew().Build();
            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = new[] { configurationCard })
                                      .Build();

            MockBoardCalendarContext(bond);

            AutoMoqer.GetMock<IBoardCalendarBondSynchronizationSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns(configurationCard.Name);

            AutoMoqer.GetMock<IJsonSerializer>()
                     .Setup(x => x.Deserialize<BoardCalendarBondSettings>(configurationCard.Description))
                     .Returns((BoardCalendarBondSettings)null);

            AutoMoqer.GetMock<IUnitOfWork>().Setup(x => x.SaveChanges());

            // Act
            AutoMoqer.Resolve<BoardCalendarBondSynchronizationService>().UpdateBondSettings(board);

            // Assert
            Assert.AreSame(settings, bond.Settings);

            VerifyMock<IBoardCalendarBondSynchronizationSettingsProvider>();
            VerifyMock<IJsonSerializer>();
        }
    }
}
