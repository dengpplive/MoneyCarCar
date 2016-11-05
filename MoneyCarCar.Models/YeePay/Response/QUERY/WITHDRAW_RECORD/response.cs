using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.Response.QUERY.WITHDRAW_RECORD
{
    /// <summary>
    /// 3.6 WITHDRAW_RECORD：提现记录
    /// </summary>
    public class response : BaseResponse
    {
        /// <summary>
        /// 投资金额 Y
        /// </summary>
        public string paymentAmount { get; set; }
        /// <summary>
        /// 还款金额 Y
        /// </summary>
        public string repaymentAmount { get; set; }
        /// <summary>
        /// Y  提现时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// Y 提现状态 ：INIT, SUCCESS 成功
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// N 打款状态  REMIT_SUCCESS 打款成功、REMIT_FAILURE 打款失败、REMITING 打款中
        /// </summary>
        public string remitStatus { get; set; }
    }
}
