using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Commons
{
    public class IpHelper
    {
        #region 获取IP
        /// <summary>
        /// 客户端ip(访问用户)
        /// </summary>
        public static string GetUserIp
        {
            get
            {
                string realRemoteIP = "";
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
                return realRemoteIP;
            }
        }
        #endregion
    }
}
