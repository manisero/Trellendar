using System.Collections.Generic;
using Trellendar.Core.Extensions;
using Trellendar.Core.Serialization;
using Trellendar.Domain;
using Trellendar.Domain.Trello;

namespace Trellendar.DataAccess.Remote.Trello._Impl
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
            var board = TrelloClient.Get("board/{0}".FormatWith(boardId),
                                         new Dictionary<string, object>
                                             {
                                                 { "fields", "name" },
                                                 { "lists", "open" },
                                                 { "list_fields", "name" },
                                                 { "cards", "all" },
                                                 { "card_fields", "closed,desc,due,idList,name" }
                                             });



            return _jsonSerializer.Deserialize<Board>(board);
        }
    }
}
