using System.Configuration;
using System.Runtime.CompilerServices;
using Trellendar.Logic.DataAccess;

namespace Trellendar.Service
{
    public class SettingsProvider : IDataAccessSettingsProvider
    {
        public int CalendarAccessTokenExpirationReserve
        {
            get { return int.Parse(GetSetting()); }
        }

        private string GetSetting([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
