using System.Collections.Generic;
using Trellendar.DataAccess._Core._Impl;

namespace Trellendar.DataAccess.Trello._Impl
{
    public class TrelloClient : RestClient, ITrelloClient
    {
        public TrelloClient() : base("https://api.trello.com/1/")
        {
        }

        protected override void IncludeAuthorizationParameters(ref IDictionary<string, object> parameters)
        {
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
                parameters.Add("token", ApplicationKeys.TRELLO_ACCESS_TOKEN);
            }
        }
    }
}
