using System.Configuration;
using System.Runtime.CompilerServices;

namespace Trellendar.Core.Settings
{
    public abstract class ConfigurationBasedSettingsProvider
    {
        protected string GetStringSetting([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }

        protected int GetIntSetting([CallerMemberName] string key = null)
        {
            return int.Parse(GetStringSetting(key));
        }
    }
}
