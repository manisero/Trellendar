using Trellendar.DataAccess.Trello;

namespace Trellendar
{
    public class TrelloTest
    {
        private readonly ITrelloAPI _trelloAPI;

        public TrelloTest(ITrelloAPI trelloAPI)
        {
            _trelloAPI = trelloAPI;
        }

        public void Test()
        {
            var result = _trelloAPI.GetBoard("4d5ea62fd76aa1136000000c");
        }
    }
}
