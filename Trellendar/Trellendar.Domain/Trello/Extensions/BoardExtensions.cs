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

            if (board.Lists == null || board.Cards == null)
            {
                return board;
            }

            foreach (var list in board.Lists)
            {
                foreach (var card in board.Cards.Where(x => x.IdList == list.Id))
                {
                    card.List = list;
                }
            }

            if (board.CheckLists == null)
            {
                return board;
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
                if (checkList.CheckItems == null)
                {
                    continue;
                }

                foreach (var checkItem in checkList.CheckItems)
                {
                    checkItem.CheckList = checkList;
                }
            }

            return board;
        }
    }
}
