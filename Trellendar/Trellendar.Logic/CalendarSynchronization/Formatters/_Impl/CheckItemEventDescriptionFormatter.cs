using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CheckItemEventDescriptionFormatter : ICheckItemEventDescriptionFormatter
    {
        private const string EVENT_DESCRIPTION_FORMAT = "Card: {0}\nLink: {1}";



        public string Format(CheckItem checkItem)
        {
            throw new System.NotImplementedException();
        }
    }
}