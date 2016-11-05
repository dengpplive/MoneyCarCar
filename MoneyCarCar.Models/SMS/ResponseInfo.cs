using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.SMS
{
    public class ResponseInfo
    {
        public ResponseInfo()
        {
            statusCode = "000000";
            statusMsg = "成功";
        }
        /// <summary>
        /// 请求状态码，取值000000（成功）
        /// </summary>
        public string statusCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string statusMsg { get; set; }

        public object data { get; set; }
        public TemplateSMS templateSMS { get; set; }
    }

    public class TemplateSMS
    {
        /// <summary>
        /// 短信唯一标识符
        /// </summary>
        public string smsMessageSid { get; set; }
        /// <summary>
        /// 短信的创建时间
        /// </summary>
        public string dateCreated { get; set; }
    }
}
