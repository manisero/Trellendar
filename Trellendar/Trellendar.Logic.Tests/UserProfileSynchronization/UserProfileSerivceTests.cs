using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Core.Serialization;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.UserProfileSynchronization;
using Trellendar.Logic.UserProfileSynchronization._Impl;

namespace Trellendar.Logic.Tests.UserProfileSynchronization
{
    [TestFixture]
    public class UserProfileSerivceTests : TestsBase
    {
        [Test]
        public void updates_user_properly()
        {
            // Arrange
            var calendar = Builder<Calendar>.CreateNew()
                                            .With(x => x.TimeZone = "test time zone")
                                            .Build();

            var user = Builder<User>.CreateNew().Build();

            MockUserContext(user);
            AutoMoqer.GetMock<IUnitOfWork>().Setup(x => x.SaveChanges());

            // Act
            AutoMoqer.Resolve<UserProfileService>().UpdateUser(calendar);

            // Assert
            Assert.AreEqual(calendar.TimeZone, user.CalendarTimeZone);
            VerifyMock<IUnitOfWork>();
        }

        [Test]
        public void updates_user_preferences_properly()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew().Build())
                                    .Build();

            var configurationCard = Builder<Card>.CreateNew().Build();
            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = new[] { configurationCard })
                                      .Build();

            var newPreferences = Builder<UserPreferences>.CreateNew().Build();

            MockUserContext(user);

            AutoMoqer.GetMock<IUserProfileSynchronizaionSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns(configurationCard.Name);

            AutoMoqer.GetMock<IJsonSerializer>()
                     .Setup(x => x.Deserialize<UserPreferences>(configurationCard.Description))
                     .Returns(newPreferences);

            AutoMoqer.GetMock<IRepositoryFactory>()
                     .Setup(x => x.Create<UserPreferences>())
                     .Returns(AutoMoqer.GetMock<IRepository<UserPreferences>>().Object);

            AutoMoqer.GetMock<IRepository<UserPreferences>>().Setup(x => x.Remove(user.UserPreferences));
            AutoMoqer.GetMock<IUnitOfWork>().Setup(x => x.SaveChanges());

            // Act
            AutoMoqer.Resolve<UserProfileService>().UpdateUserPreferences(board);

            // Assert
            Assert.AreSame(newPreferences, user.UserPreferences);

            VerifyMock<IUserProfileSynchronizaionSettingsProvider>();
            VerifyMock<IJsonSerializer>();
            VerifyMock<IUnitOfWork>();
        }

        [Test]
        public void does_not_touch_preferences_for_null_cards()
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = preferences)
                                    .Build();

            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = null)
                                      .Build();

            MockUserContext(user);

            // Act
            AutoMoqer.Resolve<UserProfileService>().UpdateUserPreferences(board);

            // Assert
            Assert.AreSame(preferences, user.UserPreferences);
        }

        [Test]
        public void does_not_touch_preferences_if_configuration_card_not_found()
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = preferences)
                                    .Build();

            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = new[] { Builder<Card>.CreateNew().Build() })
                                      .Build();

            MockUserContext(user);

            AutoMoqer.GetMock<IUserProfileSynchronizaionSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns("configuration card");

            // Act
            AutoMoqer.Resolve<UserProfileService>().UpdateUserPreferences(board);

            // Assert
            Assert.AreSame(preferences, user.UserPreferences);

            VerifyMock<IUserProfileSynchronizaionSettingsProvider>();
        }

        [Test]
        public void does_not_touch_preferences_if_configuration_card_content_not_deserializable()
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = preferences)
                                    .Build();

            var configurationCard = Builder<Card>.CreateNew().Build();
            var board = Builder<Board>.CreateNew()
                                      .With(x => x.Cards = new[] { configurationCard })
                                      .Build();

            MockUserContext(user);

            AutoMoqer.GetMock<IUserProfileSynchronizaionSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns(configurationCard.Name);

            AutoMoqer.GetMock<IJsonSerializer>()
                     .Setup(x => x.Deserialize<UserPreferences>(configurationCard.Description))
                     .Returns((UserPreferences)null);

            AutoMoqer.GetMock<IUnitOfWork>().Setup(x => x.SaveChanges());

            // Act
            AutoMoqer.Resolve<UserProfileService>().UpdateUserPreferences(board);

            // Assert
            Assert.AreSame(preferences, user.UserPreferences);

            VerifyMock<IUserProfileSynchronizaionSettingsProvider>();
            VerifyMock<IJsonSerializer>();
        }
    }
}
