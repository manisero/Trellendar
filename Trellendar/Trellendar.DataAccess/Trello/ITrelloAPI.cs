using Trellendar.Domain.Trello;

namespace Trellendar.DataAccess.Trello
{
    public interface ITrelloAPI
    {
        Board GetBoard(string boardId);
    }
}
