using System.Collections.Generic;
using Trellendar.Core.Serialization;
using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarAPI : ICalendarAPI
    {
        private readonly ICalendarClient _calendarClient;
        private readonly IJsonSerializer _jsonSerializer;

        public CalendarAPI(ICalendarClient calendarClient, IJsonSerializer jsonSerializer)
        {
            _calendarClient = calendarClient;
            _jsonSerializer = jsonSerializer;
        }

        public void Authorize()
        {
            _calendarClient.Get("https://accounts.google.com/o/oauth2/auth",
                                new Dictionary<string, object>
                                    {
                                        { "client_id", CalendarKeys.CLIENT_ID },
                                        { "response_type", "code" },
                                        { "scope", "openid email https://www.googleapis.com/auth/calendar" },
                                        { "redirect_uri", CalendarKeys.REDIRECT_URI }
                                    });
        }

        public Token GetToken(string authorizationCode)
        {
            var token = _calendarClient.Post("https://accounts.google.com/o/oauth2/token",
                                             new Dictionary<string, object>
                                                 {
                                                     { "code", authorizationCode },
                                                     { "client_id", CalendarKeys.CLIENT_ID },
                                                     { "client_secret", CalendarKeys.CLIENT_SECRET },
                                                     { "redirect_uri", CalendarKeys.REDIRECT_URI },
                                                     { "grant_type", "authorization_code" }
                                                 });

            return _jsonSerializer.Deserialize<Token>(token);
        }
    }
}