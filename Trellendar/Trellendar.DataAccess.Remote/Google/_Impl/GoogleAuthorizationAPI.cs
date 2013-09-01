using System;
using System.Collections.Generic;
using Trellendar.Core.Serialization;
using Trellendar.Domain;
using System.Linq;
using Trellendar.Domain.Google;

namespace Trellendar.DataAccess.Remote.Google._Impl
{
    public class GoogleAuthorizationAPI : IGoogleAuthorizationAPI
    {
        private readonly IRestClientFactory _restClientFactory;
        private readonly IJsonSerializer _jsonSerializer;

        private IRestClient _googleClient;
        private IRestClient GoogleClient
        {
            get { return _googleClient ?? (_googleClient = _restClientFactory.CreateClient(DomainType.Google)); }
        }

        public GoogleAuthorizationAPI(IRestClientFactory restClientFactory, IJsonSerializer jsonSerializer)
        {
            _restClientFactory = restClientFactory;
            _jsonSerializer = jsonSerializer;
        }

        public string GetAuthorizationUri(string redirectUri, string state = null, bool requestRefreshToken = false)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "response_type", "code" },
                    { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                    { "redirect_uri", redirectUri },
                    { "scope", "openid email https://www.googleapis.com/auth/calendar" },
                    { "access_type", "offline" }
                };

            if (state != null)
            {
                parameters["state"] = state;
            }

            if (requestRefreshToken)
            {
                parameters["prompt"] = "consent";
            }

            return GoogleClient.FormatRequestUri("auth", parameters);
        }

        public Token GetToken(string authorizationCode, string redirectUri)
        {
            var timeStamp = DateTime.UtcNow;
            var tokenJson = GoogleClient.Post("token",
                                              new Dictionary<string, object>
                                                  {
                                                      { "code", authorizationCode },
                                                      { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                      { "client_secret", ApplicationKeys.GOOGLE_API_CLIENT_SECRET },
                                                      { "redirect_uri", redirectUri },
                                                      { "grant_type", "authorization_code" }
                                                  });

            var token = _jsonSerializer.Deserialize<Token>(tokenJson);
            token.UserEmail = GetUserEmail(token.IdToken);
            token.CreateTS = timeStamp;

            return token;
        }

        public Token GetNewToken(string refreshToken)
        {
            var timeStamp = DateTime.UtcNow;
            var tokenJson = GoogleClient.Post("token",
                                              new Dictionary<string, object>
                                                  {
                                                      { "refresh_token", refreshToken },
                                                      { "client_id", ApplicationKeys.GOOGLE_API_CLIENT_ID },
                                                      { "client_secret", ApplicationKeys.GOOGLE_API_CLIENT_SECRET },
                                                      { "grant_type", "refresh_token" }
                                                  });

            var token = _jsonSerializer.Deserialize<Token>(tokenJson);
            token.CreateTS = timeStamp;

            return token;
        }

        private string GetUserEmail(string idToken)
        {
            var claims = _jsonSerializer.DeserializeJWT(idToken);
            var emailClaim = claims.SingleOrDefault(x => x.Type == "email");

            return emailClaim != null ? emailClaim.Value : null;
        }
    }
}