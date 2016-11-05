using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 2.3 提现：接口输入 
    /// </summary>
    public class ToWithdraw : YeePayConfig
    {
        public ToWithdraw()
        {
            _feeMode = EnumFeeMode.USER.ToEnumDesc();
        }
        /// <summary>
        /// Y 费率模式， 固定值：PLATFORM ,收取商户手续费:PLATFORM、收取用户手续费:USER
        /// </summary>
        public string _feeMode { get; set; }
        /// <summary>
        /// N 提现金额, 如果传入此将使用该金额提现且用户将不可更改
        /// </summary>
        public string _amount { get; set; }
    }
}
