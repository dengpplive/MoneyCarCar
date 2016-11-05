using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay
{
    public class YeePayNotify
    { 
        /// <summary>
        /// xml
        /// </summary>
        public string notify { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

    }
}
