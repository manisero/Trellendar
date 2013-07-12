using Trellendar.Core.Extensions;
using Trellendar.Core.Serialization;
using Trellendar.Domain.Trello;

namespace Trellendar.DataAccess.Trello._Impl
{
    public class TrelloAPI : ITrelloAPI
    {
        private readonly ITrelloClient _trelloClient;
        private readonly IJsonSerializer _jsonSerializer;

        public TrelloAPI(ITrelloClient trelloClient, IJsonSerializer jsonSerializer)
        {
            _trelloClient = trelloClient;
            _jsonSerializer = jsonSerializer;
        }

        public Board GetBoard(string boardId)
        {
            var board = _trelloClient.Get("board/{0}".FormatWith(boardId));

            return _jsonSerializer.Deserialize<Board>(board);
        }
    }
}
