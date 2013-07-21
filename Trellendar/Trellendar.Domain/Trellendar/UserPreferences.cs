namespace Trellendar.Domain.Trellendar
{
    public class UserPreferences
    {
        public int UserPreferencesID { get; set; }

        public User User { get; set; }

        public string CardEventNameTemplate { get; set; }
    }
}
