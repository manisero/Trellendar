using Trellendar.DataAccess._Impl;

namespace Trellendar.DataAccess.Trello.RestClients
{
    public class TrelloClient : RestClient
    {
        public TrelloClient() : base("https://api.trello.com/1/")
        {
        }
    }
}
