using PayPal.Api;
using System;
using System.Collections.Generic;

namespace OnlineStore.Helper
{
    public static class ConfigurationHelper
    {
        //these variables will store the clientID and clientSecret
        //by reading them from the web.config
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        static ConfigurationHelper()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal                
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetApiContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken()) {Config = GetConfig()};
            return apiContext;
        }
    }
}