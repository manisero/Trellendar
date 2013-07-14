using System.Configuration;
using Trellendar.Logic.DataAccess;

namespace Trellendar.Service
{
    public class SettingsProvider : IDataAccessSettingsProvider
    {
        public int CalendarAccessTokenExpirationReserve
        {
            get { return int.Parse(GetSetting("CalendarAccessTokenExpirationReserve")); }
        }

        private string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
