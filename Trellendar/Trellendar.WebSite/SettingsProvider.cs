using System.Configuration;
using System.Runtime.CompilerServices;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.UserProfileSynchronization;

namespace Trellendar.WebSite
{
    public class SettingsProvider : IDataAccessSettingsProvider, IUserProfileSynchronizaionSettingsProvider
    {
        public int CalendarAccessTokenExpirationReserve
        {
            get { return GetIntSetting(); }
        }

        public string TrellendarConfigurationTrelloCardName
        {
            get { return GetStringSetting(); }
        }

        private string GetStringSetting([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private int GetIntSetting([CallerMemberName] string key = null)
        {
            return int.Parse(GetStringSetting(key));
        }
    }
}
