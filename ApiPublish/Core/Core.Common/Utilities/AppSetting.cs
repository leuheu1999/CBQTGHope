using System.Configuration;
using System.Web.Configuration;

namespace Core.Common.Utilities
{
    public static class AppSetting
    {
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = WebConfigurationManager.AppSettings;
                return appSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                return string.Empty;
            }
        }
    }
}
