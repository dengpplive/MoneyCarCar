using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.Response.QUERY.CP_TRANSACTION
{
    /// <summary>
    /// 3.6 CP_TRANSACTION：划款记录
    /// </summary>
    public class response : BaseResponse
    {
        /// <summary>
        /// 转账总金额
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        ///  Y 订单状态：PREAUTH 已授权。CONFIRM：已确认出款。CANCEL：已取消。DIRECT：直接划转
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Y 处理状态 : PROCESSING：处理中。SUCCESS：成功。ERROR：异常。FAIL：失败
        /// </summary>
        public string subStatus { get; set; }
    }
}
