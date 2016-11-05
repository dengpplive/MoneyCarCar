using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyCarCar.AdminWebsite.Controllers.CommHelper
{
    /// <summary>
    /// 站点配置信息
    /// </summary>
    public class AppConfigHelper
    {
        private static string _WebApiUrl = string.Empty;
        public static string WebApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_WebApiUrl))
                {
                    _WebApiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"].ToString();
                    _WebApiUrl = _WebApiUrl.TrimEnd('/') + "/";
                }
                return _WebApiUrl;
            }
        }
        private static string _MainSiteUrl = string.Empty;
        public static string MainSiteUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_MainSiteUrl))
                {
                    _MainSiteUrl = System.Configuration.ConfigurationManager.AppSettings["MainSiteUrl"].ToString();
                    _MainSiteUrl = _MainSiteUrl.TrimEnd('/');
                }
                return _MainSiteUrl;
            }
        }
        private static int _WebApiTimeOut = 10;
        public static int WebApiTimeOut
        {
            get
            {
                try
                {
                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["WebApiTimeOut"].ToString(), out _WebApiTimeOut);
                }
                catch
                {
                }
                return _WebApiTimeOut;
            }
        }

        public static string IP
        {
            get
            {
                string realRemoteIP = "";
                try
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        realRemoteIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0];
                    }
                    if (string.IsNullOrEmpty(realRemoteIP))
                    {
                        realRemoteIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    if (string.IsNullOrEmpty(realRemoteIP))
                    {
                        realRemoteIP = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }
                }
                catch
                {
                }
                return realRemoteIP;
            }
        }
    }
}