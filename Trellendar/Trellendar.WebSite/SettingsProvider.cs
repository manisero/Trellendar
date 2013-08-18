using Trellendar.Core.Settings;
using Trellendar.Logic.DataAccess;

namespace Trellendar.WebSite
{
    public class SettingsProvider : ConfigurationBasedSettingsProvider, IDataAccessSettingsProvider
    {
        public int CalendarAccessTokenExpirationReserve
        {
            get { return GetIntSetting(); }
        }
    }
}
