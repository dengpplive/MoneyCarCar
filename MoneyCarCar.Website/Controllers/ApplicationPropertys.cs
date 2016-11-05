using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyCarCar.Website.Controllers
{
    public static class ApplicationPropertys
    {
        private static string webapiUrl = "";
        public static string WEBAPI_URL
        {
            get
            {
                if (string.IsNullOrEmpty(webapiUrl))
                {
                    webapiUrl = System.Configuration.ConfigurationManager.ConnectionStrings["WebApiUrl"].ConnectionString;
                }
                return webapiUrl;
            }
        }

        private static string adminWebSiteUrl = "";
        public static string ADMINWebSITE_URL
        {
            get
            {
                if (string.IsNullOrEmpty(adminWebSiteUrl))
                {
                    adminWebSiteUrl = System.Configuration.ConfigurationManager.ConnectionStrings["AdminWebSiteUrl"].ConnectionString;
                }
                return adminWebSiteUrl;
            }
        }
    }
}