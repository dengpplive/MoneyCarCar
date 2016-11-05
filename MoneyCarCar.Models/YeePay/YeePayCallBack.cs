using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay
{
    public class YeePayCallBack
    {
        /// <summary>
        /// xml
        /// </summary>
        public string resp { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
