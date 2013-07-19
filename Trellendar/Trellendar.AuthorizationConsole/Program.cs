﻿using System;
using System.Diagnostics;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Calendar._Impl;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Trello._Impl;
using Trellendar.DataAccess._Impl;
using Trellendar.Domain.Trellendar;

namespace Trellendar.AuthorizationConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
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
            
            var caledndarUri = calendarAuthorization.GetAuthorizationUri();
            Process.Start(caledndarUri);

            Console.WriteLine("Plase paste the code here:");
            var calendarCode = Console.ReadLine();
            var calendarToken = calendarAuthorization.GetToken(calendarCode);
            var userInfo = calendarAuthorization.GetUserInfo(calendarToken.Id_Token);

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
                    CalendarAccessToken = calendarToken.Access_Token,
                    CalendarAccessTokenExpirationTS = calendarToken.ExpirationTS,
                    CalendarRefreshToken = calendarToken.Refresh_Token
                };

            dataContext.Users.Add(user);
            dataContext.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("User {0} created.", user.Email);
            Console.WriteLine();
        }
    }
}
