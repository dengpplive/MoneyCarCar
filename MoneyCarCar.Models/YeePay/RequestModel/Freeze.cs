using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 3.2. 资金冻结 : 接口输入
    /// </summary>
    public class Freeze : YeePayConfig
    {
        /// <summary>
        /// Y 冻结金额：保留分 两位小数
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 自动解冻时间点 Y 到期自动解冻:时间格式：2014-12-29 12:12:12
        /// </summary>
        public string expired { get; set; }
    }
}
