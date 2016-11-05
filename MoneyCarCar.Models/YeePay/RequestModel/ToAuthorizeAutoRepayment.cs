using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 2.9 自动还款授权：接口输入
    /// </summary>
    public class ToAuthorizeAutoRepayment : YeePayConfig
    {
        /// <summary>
        /// Y 标的号，标识要自动还款的标的号
        /// </summary>
        public string orderNo { get; set; }
    }
}
