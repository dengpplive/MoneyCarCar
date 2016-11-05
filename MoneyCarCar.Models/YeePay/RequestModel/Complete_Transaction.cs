using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 3.7. 转账确认
    /// </summary>
    public class Complete_Transaction : YeePayConfig
    {
        /// <summary>
        /// CONFIRM 表示解冻后完成资金划转，CANCEL 表示解冻后取消转账
        /// </summary>
        public string mode { set; get; }
    }
}
