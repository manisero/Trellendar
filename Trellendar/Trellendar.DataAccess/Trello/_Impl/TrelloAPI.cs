using Trellendar.Core.Extensions;
using Trellendar.Core.Serialization;
using Trellendar.DataAccess._Core;
using Trellendar.Domain;
using Trellendar.Domain.Trello;

namespace Trellendar.DataAccess.Trello._Impl
{
    public class TrelloAPI : ITrelloAPI
    {
        private readonly IRestClientFactory _restClientFactory;
        private readonly IJsonSerializer _jsonSerializer;

        private IRestClient _trelloClient;
        private IRestClient TrelloClient
        {
            get { return _trelloClient ?? (_trelloClient = _restClientFactory.CreateAuthorizedClient(DomainType.Trello)); }
        }

        public TrelloAPI(IRestClientFactory restClientFactory, IJsonSerializer jsonSerializer)
        {
            _restClientFactory = restClientFactory;
            _jsonSerializer = jsonSerializer;
        }

        public Board GetBoard(string boardId)
        {
            var board = TrelloClient.Get("board/{0}".FormatWith(boardId));

            return _jsonSerializer.Deserialize<Board>(board);
        }
    }
}
