namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarAccessTokenProvider
    {
        bool CanProvideCalendarAccessToken { get; }

        string GetCalendarAccessToken();
    }
}
