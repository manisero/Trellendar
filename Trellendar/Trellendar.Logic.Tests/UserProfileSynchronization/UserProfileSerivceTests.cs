using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Core.Serialization;
using Trellendar.DataAccess.Local.Repository;
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

            AutoMoqer.SetInstance(new UserContext { User = user });

            AutoMoqer.GetMock<IUserProfileSynchronizaionSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns(configurationCard.Name);

            AutoMoqer.GetMock<IJsonSerializer>()
                     .Setup(x => x.Deserialize<UserPreferences>(configurationCard.Desc))
                     .Returns(newPreferences);

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

            AutoMoqer.SetInstance(new UserContext { User = user });

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

            AutoMoqer.SetInstance(new UserContext { User = user });

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

            AutoMoqer.SetInstance(new UserContext { User = user });

            AutoMoqer.GetMock<IUserProfileSynchronizaionSettingsProvider>()
                     .Setup(x => x.TrellendarConfigurationTrelloCardName)
                     .Returns(configurationCard.Name);

            AutoMoqer.GetMock<IJsonSerializer>()
                     .Setup(x => x.Deserialize<UserPreferences>(configurationCard.Desc))
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
