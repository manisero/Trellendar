namespace Trellendar.Domain.Trellendar
{
    public class UserPreferences
    {
        public int UserID { get; set; }

        public User User { get; set; }

        public string CardEventNameTemplate { get; set; }
    }
}
