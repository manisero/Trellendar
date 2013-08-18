using System.Configuration;
using System.Runtime.CompilerServices;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.UserProfileSynchronization;

namespace Trellendar.Service
{
    public class SettingsProvider : IDataAccessSettingsProvider, IUserProfileSynchronizaionSettingsProvider, ITrellendarServiceSettingsProvider
    {
        public int CalendarAccessTokenExpirationReserve
        {
            get { return GetIntSetting(); }
        }

        public string TrellendarConfigurationTrelloCardName
        {
            get { return GetStringSetting(); }
        }

        public int WorkInterval
        {
            get { return GetIntSetting(); }
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
