using System.Collections.Generic;

namespace Trellendar.DataAccess.Trello
{
    public interface ITrelloClient
    {
        string Get(string resource, IDictionary<string, object> parameters = null);
    }
}
