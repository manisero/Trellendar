using Trellendar.Domain.Trello;

namespace Trellendar.DataAccess.Remote.Trello
{
    public interface ITrelloAPI
    {
        Board GetBoard(string boardId);
    }
}
