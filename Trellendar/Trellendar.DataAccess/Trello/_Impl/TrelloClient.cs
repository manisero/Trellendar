using Trellendar.DataAccess._Core._Impl;

namespace Trellendar.DataAccess.Trello._Impl
{
    public class TrelloClient : RestClient, ITrelloClient
    {
        public TrelloClient() : base("https://api.trello.com/1/")
        {
        }
    }
}
