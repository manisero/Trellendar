using Newtonsoft.Json;
using Trellendar.Trello.Domain;

namespace Trellendar.Trello._Impl
{
    public class TrelloAPI : ITrelloAPI
    {
        private readonly ITrelloClient _trelloClient;

        public TrelloAPI(ITrelloClient trelloClient)
        {
            _trelloClient = trelloClient;
        }

        public Board GetBoard(string boardId)
        {
            var board = _trelloClient.Get(string.Format("board/{0}", boardId));

            return JsonConvert.DeserializeObject<Board>(board);
        }

        public void Test()
        {
            var result = GetBoard("4d5ea62fd76aa1136000000c");
        }
    }
}
