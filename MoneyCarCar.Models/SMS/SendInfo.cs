using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.SMS
{
    public class SendInfo
    {
        /// <summary>
        /// 短信接收端手机号码集合，用英文逗号分开，每批发送的手机号数量不得超过100个
        /// </summary>
        public string to { get; set; }

        public string appId { get; set; }

        public string templateId { get; set; }

        public string[] datas { get; set; }
    }
}
