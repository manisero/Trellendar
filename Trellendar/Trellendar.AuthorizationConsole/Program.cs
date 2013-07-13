using System;
using System.Diagnostics;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Calendar._Impl;

namespace Trellendar.AuthorizationConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var authorizationApi = new CalendarAuthorizationAPI(new CalendarClient(), new JsonSerializer());

            Console.WriteLine("You'll be directed to Google login page.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            
            var authorizationUri = authorizationApi.GetAuthorizationUri();
            Process.Start(authorizationUri);

            Console.WriteLine("Plase paste the code here:");
            var authorizationCode = Console.ReadLine();

            var token = authorizationApi.GetToken(authorizationCode);

            Console.WriteLine();
            Console.WriteLine("The access token is:");
            Console.WriteLine(token.Access_Token);
            Console.WriteLine();
        }
    }
}
