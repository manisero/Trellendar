using System;
using System.Collections.Generic;
using Trellendar.Core.Serialization;
using Trellendar.DataAccess._Core;
using Trellendar.Domain;
using Trellendar.Domain.Calendar;
using System.Linq;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarAuthorizationAPI : ICalendarAuthorizationAPI
    {
        private readonly IRestClientFactory _restClientFactory;
        private readonly IJsonSerializer _jsonSerializer;

        private IRestClient _calendarClient;
        private IRestClient CalendarClient
        {
            get { return _calendarClient ?? (_calendarClient = _restClientFactory.CreateClient(DomainType.Calendar)); }
        }

        public CalendarAuthorizationAPI(IRestClientFactory restClientFactory, IJsonSerializer jsonSerializer)
        {
            _restClientFactory = restClientFactory;
            _jsonSerializer = jsonSerializer;
        }

        public string GetAuthorizationUri()
        {
            return CalendarClient.FormatRequestUri("https://accounts.google.com/o/oauth2/auth",
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
            var timeStamp = DateTime.UtcNow;
            var tokenJson = CalendarClient.Post("https://accounts.google.com/o/oauth2/token",
                                                new Dictionary<string, object>
                                                    {
                                                        { "code", authorizationCode },
                                                        { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                        { "client_secret", ApplicationKeys.GOOGLE_API_CLIENT_SECRET },
                                                        { "redirect_uri", ApplicationKeys.GOOGLE_API_REDIRECT_URI },
                                                        { "grant_type", "authorization_code" }
                                                    });

            var token = _jsonSerializer.Deserialize<Token>(tokenJson);
            token.CreationTS = timeStamp;

            return token;
        }

        public Token GetNewToken(string refreshToken)
        {
            var timeStamp = DateTime.UtcNow;
            var tokenJson = CalendarClient.Post("https://accounts.google.com/o/oauth2/token",
                                                new Dictionary<string, object>
                                                    {
                                                        { "refresh_token", refreshToken },
                                                        { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                        { "client_secret", ApplicationKeys.GOOGLE_API_CLIENT_SECRET },
                                                        { "grant_type", "refresh_token" }
                                                    });

            var token = _jsonSerializer.Deserialize<Token>(tokenJson);
            token.CreationTS = timeStamp;

            return token;
        }

        public UserInfo GetUserInfo(string idToken)
        {
            var claims = _jsonSerializer.DeserializeJWT(idToken);
            var emailClaim = claims.SingleOrDefault(x => x.Type == "email");

            return new UserInfo
                {
                    Email = emailClaim != null ? emailClaim.Value : null
                };
        }
    }
}