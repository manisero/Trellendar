using System.Linq;

namespace Trellendar.Domain.Trello.Extensions
{
    public static class BoardExtensions
    {
        public static Board SetChildParentRelations(this Board board)
        {
            if (board == null)
            {
                return null;
            }

            foreach (var list in board.Lists)
            {
                foreach (var card in board.Cards.Where(x => x.IdList == list.Id))
                {
                    card.List = list;
                }
            }

            foreach (var card in board.Cards)
            {
                foreach (var checkList in board.CheckLists.Where(x => x.IdCard == card.Id))
                {
                    checkList.Card = card;
                }
            }

            foreach (var checkList in board.CheckLists)
            {
                foreach (var checkItem in checkList.CheckItems)
                {
                    checkItem.CheckList = checkList;
                }
            }

            return board;
        }
    }
}
