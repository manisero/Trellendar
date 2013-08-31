using System.Collections.Generic;
using Trellendar.Domain.Trello;

namespace Trellendar.DataAccess.Remote.Trello
{
    public interface ITrelloAPI
    {
        IEnumerable<Board> GetBoards();

        Board GetBoard(string boardId);
    }
}
