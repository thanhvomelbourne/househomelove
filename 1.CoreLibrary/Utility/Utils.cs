using System;
using System.Configuration;
using System.Globalization;

namespace CoreLibrary.Utility
{
    public static class Utils
    {
        public static T GetSetting<T>(string key)
        {
            return GetSetting(key, default(T));
        }

        public static T GetSetting<T>(string key, T defaultValue)
        {
            try
            {
                string appSetting = ConfigurationManager.AppSettings[key];

                if (string.IsNullOrEmpty(appSetting)) return defaultValue;

                return (T)Convert.ChangeType(appSetting, typeof(T), CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
