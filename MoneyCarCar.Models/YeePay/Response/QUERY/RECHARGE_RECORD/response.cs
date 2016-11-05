using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.Response.QUERY.RECHARGE_RECORD
{
    /// <summary>
    /// 3.6 RECHARGE_RECORD：充值记录
    /// </summary>
    public class response : BaseResponse
    {
        /// <summary>
        /// Y 充值金额 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// Y　充值用户
        /// </summary>
        public string userNo { get; set; }
        /// <summary>
        ///　Y 充值时间 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        ///　Y 充值状态:INIT,SUCCESS
        /// </summary>
        public string status { get; set; }
    }
}
