using System;
using System.Data.Entity;
using System.Diagnostics;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Local.Migrations;
using Trellendar.DataAccess.Remote.Calendar._Impl;
using Trellendar.DataAccess.Remote.Trello._Impl;
using Trellendar.DataAccess.Remote._Impl;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;
using UserPreferences = Trellendar.Domain.Trellendar.UserPreferences;

namespace Trellendar.AuthorizationConsole
{
    public class Program
    {
        private const string CALENDAR_AUTHORIZATION_REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrellendarDataContext, Configuration>());

            var restClientFactory = new RestClientFactory(null);
            var trelloAuthorization = new TrelloAuthorizationAPI(restClientFactory);
            var calendarAuthorization = new CalendarAuthorizationAPI(restClientFactory, new JsonSerializer());
            var dataContext = new TrellendarDataContext();

            // Get Trello token
            Console.WriteLine("You'll be directed to Trello login page.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

            var trelloUri = trelloAuthorization.GetAuthorizationUri();
            Process.Start(trelloUri);

            Console.WriteLine("Plase paste the token here:");
            var trelloToken = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Now paste your Trello Board ID here:");
            var trelloBoardId = Console.ReadLine();

            // Get Google API token
            Console.WriteLine();
            Console.WriteLine("Now You'll be directed to Google login page.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

            var caledndarUri = calendarAuthorization.GetAuthorizationUri(CALENDAR_AUTHORIZATION_REDIRECT_URI);
            Process.Start(caledndarUri);

            Console.WriteLine("Plase paste the code here:");
            var calendarCode = Console.ReadLine();
            var calendarToken = calendarAuthorization.GetToken(calendarCode, CALENDAR_AUTHORIZATION_REDIRECT_URI);
            var userInfo = calendarAuthorization.GetUserInfo(calendarToken.IdToken);

            Console.WriteLine();
            Console.WriteLine("Now paste your Google Calendar ID here:");
            var calendarId = Console.ReadLine();

            // Create User
            var user = new User
                {
                    Email = userInfo.Email,
                    TrelloBoardID = trelloBoardId,
                    TrelloAccessToken = trelloToken,
                    CalendarID = calendarId,
                    CalendarAccessToken = calendarToken.AccessToken,
                    CalendarAccessTokenExpirationTS = calendarToken.GetExpirationTS(),
                    CalendarRefreshToken = calendarToken.RefreshToken,
                    LastSynchronizationTS = new DateTime(1900, 1, 1),
					UserPreferences = new UserPreferences()
                };

            dataContext.Users.Add(user);
            dataContext.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("User {0} created.", user.Email);
            Console.WriteLine();
        }
    }
}
