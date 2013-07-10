namespace Trellendar.Trello._Impl
{
    public class TrelloAPI : ITrelloAPI
    {
        private readonly ITrelloClient _trelloClient;

        public TrelloAPI(ITrelloClient trelloClient)
        {
            _trelloClient = trelloClient;
        }

        public void Test()
        {
            _trelloClient.Get(@"board/4d5ea62fd76aa1136000000c");
        }
    }
}
