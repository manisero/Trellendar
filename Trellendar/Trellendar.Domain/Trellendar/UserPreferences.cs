namespace Trellendar.Domain.Trellendar
{
    public class UserPreferences
    {
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public string ListShortcutBeginningMarker { get; set; }

        public string ListShortcutEndMarker { get; set; }

        public string CardEventNameTemplate { get; set; }
    }
}
