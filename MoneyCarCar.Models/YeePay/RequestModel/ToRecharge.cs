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
    /// 2.2 充值：接口输入
    /// </summary>
    public class ToRecharge : YeePayConfig
    {
        public ToRecharge()
        {
            _feeMode = EnumFeeMode.PLATFORM.ToEnumDesc(); 
        }
        /// <summary>
        /// Y 费率模式， 固定值：PLATFORM ,收取商户手续费:PLATFORM、收取用户手续费:USER
        /// </summary>
        public string _feeMode{ get; set; }

        /// <summary>
        /// N 充值金额,如果传入金额，则跳过金额填写页面
        /// </summary>
        public string _amount{ get; set; }
    }
}
