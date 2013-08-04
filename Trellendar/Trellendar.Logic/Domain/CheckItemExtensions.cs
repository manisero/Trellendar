using Trellendar.Domain.Trello;

namespace Trellendar.Logic.Domain
{
    public static class CheckItemExtensions
    {
        public const string STATE_DONE = "complete";
        public const string STATE_NOT_DONE = "incomplete";

        public static bool IsDone(this CheckItem checkItem)
        {
            return checkItem.State == STATE_DONE;
        }
    }
}
