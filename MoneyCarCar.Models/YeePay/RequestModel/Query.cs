using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 3.6. 单笔业务查询: 接口输入
    /// </summary>
    public class Query : YeePayConfig
    {
        /// <summary>
        /// //mode Y 查询模式，有如下枚举值：WITHDRAW_RECORD：提现记录、RECHARGE_RECORD：充值记录、CP_TRANSACTION：划款记录
        /// </summary>
        public string mode { get; set; }
    }
}
