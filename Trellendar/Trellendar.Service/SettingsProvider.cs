using Trellendar.Core.Settings;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.UserProfileSynchronization;

namespace Trellendar.Service
{
    public class SettingsProvider : ConfigurationBasedSettingsProvider,
                                    IDataAccessSettingsProvider, IUserProfileSynchronizaionSettingsProvider, ITrellendarServiceSettingsProvider
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
    }
}
