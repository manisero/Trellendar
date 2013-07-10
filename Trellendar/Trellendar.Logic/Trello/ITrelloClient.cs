using System.Collections.Generic;

namespace Trellendar.Trello
{
    public interface ITrelloClient
    {
        string Get(string resource, IDictionary<string, object> parameters = null);
    }
}
