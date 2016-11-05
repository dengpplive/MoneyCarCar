using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.Response.RECONCILIATION
{
    /// <summary>
    /// 3.10 使用
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 业务类型 Y 枚举值: EnumBizType  //4.10 业务类型
        /// </summary>
        public string bizType { get; set; }
        /// <summary>
        /// 易宝收取手续费
        /// </summary>
        public string fee { get; set; }
        /// <summary>
        /// 商户平台收取分润
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 业务金额
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 请求流水号
        /// </summary>
        public string requestNo { get; set; }
        /// <summary>
        /// 平台编号
        /// </summary>
        public string platformNo { get; set; }
    }
}
