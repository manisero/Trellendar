using System.Collections.Generic;
using Trellendar.DataAccess._Core._Impl;

namespace Trellendar.DataAccess.Trello._Impl
{
    public class TrelloClient : RestClient, ITrelloClient
    {
        private readonly UserContext _userContext;

        public TrelloClient(UserContext userContext) : base("https://api.trello.com/1/")
        {
            _userContext = userContext;
        }

        protected override void IncludeAuthorizationParameters(ref IDictionary<string, object> parameters)
        {
            if (!_userContext.IsFilled())
            {
                return;
            }

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            if (!parameters.ContainsKey("key"))
            {
                parameters.Add("key", ApplicationKeys.TRELLO_APPLICATION_KEY);
            }

            if (!parameters.ContainsKey("token"))
            {
                parameters.Add("token", _userContext.User.TrelloAccessToken);
            }
        }
    }
}
