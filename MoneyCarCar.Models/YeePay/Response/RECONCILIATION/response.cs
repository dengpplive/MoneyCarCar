using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.Response.RECONCILIATION
{
    /// <summary>
    /// 
    /// 3.10.对账 : 接口输出
    /// </summary>
    public class response : BaseResponse
    {
        /// <summary>
        /// Y 对账明细  记录列表
        /// </summary>
        public List<Record> records { get; set; }
    }
}
