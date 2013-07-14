namespace Trellendar.Logic.DataAccess
{
    public interface IDataAccessSettingsProvider
    {
        int CalendarAccessTokenExpirationReserve { get; }
    }
}