using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

namespace BinaShop.Core.Models
{
    public class PayPalConfiguration
    {
        public readonly static string ClinetId;
        public readonly static string ClinetSecret;
        static PayPalConfiguration()
        {
            var config = GetConfig();
            ClinetId = config["clientId"];
            ClinetSecret = config["clientSecret"];
        }
        
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        //create access token
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClinetId, ClinetSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }


        //this will return APIContext object
        public static  APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}
