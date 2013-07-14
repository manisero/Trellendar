using System;
using System.Diagnostics;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Calendar._Impl;
using Trellendar.DataAccess.Trellendar;
using Trellendar.DataAccess.Trello._Impl;
using Trellendar.Domain.Trellendar;

namespace Trellendar.AuthorizationConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var trelloAuthorization = new TrelloAuthorizationAPI(new TrelloClient());
            var calendarAuthorization = new CalendarAuthorizationAPI(new CalendarClient(), new JsonSerializer());
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
            Console.WriteLine("Now You'll be directed to Google login page.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            
            var caledndarUri = calendarAuthorization.GetAuthorizationUri();
            Process.Start(caledndarUri);

            Console.WriteLine("Plase paste the code here:");
            var calendarCode = Console.ReadLine();
            var calendarToken = calendarAuthorization.GetToken(calendarCode);

            Console.WriteLine();
            Console.WriteLine("Now paste your Google Calendar ID here:");
            var calendarId = Console.ReadLine();

            // Create User
            var user = new User
                {
                    Email = "TODO",
                    TrelloBoardID = trelloBoardId,
                    TrelloAccessToken = trelloToken,
                    CalendarID = calendarId,
                    CalendarAccessToken = calendarToken.Access_Token,
                    CalendarAccessTokenExpirationTS = DateTime.Now.AddSeconds(calendarToken.Expires_In),
                    CalendarRefreshToken = calendarToken.Refresh_Token
                };

            dataContext.Users.Add(user);
            dataContext.SaveChanges();

            Console.WriteLine("User {0} created.", user.Email);
        }
    }
}
