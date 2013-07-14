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
            Console.WriteLine("The token is:");
            Console.WriteLine(trelloToken);
            Console.WriteLine();

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
            Console.WriteLine("The access token is:");
            Console.WriteLine(calendarToken.Access_Token);
            Console.WriteLine();

            dataContext.Users.Add(new User
                {
                    Email = "TODO",
                    TrelloAccessToken = trelloToken,
                    CalendarAccessToken = calendarToken.Access_Token,
                    CalendarAccessTokenExpirationTS = DateTime.Now.AddSeconds(calendarToken.Expires_In)
                });

            dataContext.SaveChanges();
        }
    }
}
