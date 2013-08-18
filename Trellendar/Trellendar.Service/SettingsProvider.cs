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
            get { return int.Parse(GetSetting()); }
        }

        public string TrellendarConfigurationTrelloCardName
        {
            get { return GetSetting(); }
        }

        public int WorkInterval
        {
            get { return int.Parse(GetSetting()); }
        }

        private string GetSetting([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
