using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.Response.QUERY.FREEZERE_RECORD
{
    /// <summary>
    /// 3.6 FREEZERE_RECORD：冻结/解冻记录
    /// </summary>
    public class response : BaseResponse
    {
        public records records { get; set; }
    }
    public class record
    {
        /// <summary>
        /// Y 流水号 
        /// </summary>
        public string requestNo { get; set; }
        /// <summary>
        /// Y　平台会员编号
        /// </summary>
        public string platformUserNo { get; set; }
        /// <summary>
        /// Y 冻结金额 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// Y 过期时间
        /// </summary>
        public string expired { get; set; }
        /// <summary>
        /// Y  创建时间
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// Y  处理状态:INIT，FREEZED，UNFREEZED
        /// </summary>
        public string status { get; set; }


    }
    public class records
    {

        public record record { get; set; }
    }
}
