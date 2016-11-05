using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 3.2. 资金解冻 : 接口输入
    /// </summary>
    public class UnFreeze : YeePayConfig
    {
        /// <summary>
        /// Y 冻结时的请求流水号
        /// </summary>
        public string freezeRequestNo { get; set; }
    }
}
