using System.Collections.Generic;
using Trellendar.Core.Serialization;
using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarAuthorizationAPI : ICalendarAuthorizationAPI
    {
        private readonly ICalendarClient _calendarClient;
        private readonly IJsonSerializer _jsonSerializer;

        public CalendarAuthorizationAPI(ICalendarClient calendarClient, IJsonSerializer jsonSerializer)
        {
            _calendarClient = calendarClient;
            _jsonSerializer = jsonSerializer;
        }

        public string GetAuthorizationUri()
        {
            return _calendarClient.FormatRequestUri("https://accounts.google.com/o/oauth2/auth",
                                                    new Dictionary<string, object>
                                                        {
                                                            { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                            { "response_type", "code" },
                                                            { "scope", "openid email https://www.googleapis.com/auth/calendar" },
                                                            { "redirect_uri", ApplicationKeys.GOOGLE_API_REDIRECT_URI }
                                                        });
        }

        public Token GetToken(string authorizationCode)
        {
            var token = _calendarClient.Post("https://accounts.google.com/o/oauth2/token",
                                             new Dictionary<string, object>
                                                 {
                                                     { "code", authorizationCode },
                                                     { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                     { "client_secret", ApplicationKeys.GOOGLE_API_CLIENT_SECRET },
                                                     { "redirect_uri", ApplicationKeys.GOOGLE_API_REDIRECT_URI },
                                                     { "grant_type", "authorization_code" }
                                                 });

            return _jsonSerializer.Deserialize<Token>(token);
        }

        public Token GetNewToken(string refreshToken)
        {
            var token = _calendarClient.Post("https://accounts.google.com/o/oauth2/token",
                                             new Dictionary<string, object>
                                                 {
                                                     { "refresh_token", refreshToken },
                                                     { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                     { "client_secret", ApplicationKeys.GOOGLE_API_CLIENT_SECRET },
                                                     { "grant_type", "refresh_token" }
                                                 });

            return _jsonSerializer.Deserialize<Token>(token);
        }
    }
}