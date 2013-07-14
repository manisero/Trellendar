namespace Trellendar.Domain.Calendar
{
    public class Event
    {
        public string Id { get; set; }

        public string summary { get; set; }

        public TimeStamp start { get; set; }

        public TimeStamp end { get; set; }
    }
}
