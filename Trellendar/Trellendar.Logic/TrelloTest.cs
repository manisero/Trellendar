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
            var result = _trelloAPI.GetBoard("51e072d0f1171f9b1e002b48");
        }
    }
}
