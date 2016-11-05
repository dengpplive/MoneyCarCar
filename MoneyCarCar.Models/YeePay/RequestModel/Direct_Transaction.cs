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
    ///  3.4 直接转账 : 接口输入
    /// </summary>
    public class Direct_Transaction : YeePayConfig
    {
        public Direct_Transaction()
        {
            // Y 出款人用户类型，目前只支持传入 MERCHANT , 默认：MERCHANT
            _userType = EnumUserType.MERCHANT.ToEnumDesc();

            //  Y 目前只支持传入 TRANSFER ， 默认值：TRANSFER
            _bizType = EnumBizType.TRANSFER.ToEnumDesc();
        }

        /// <summary>
        /// Y 用户类型, 见【用户类型】 ： MEMBER 个人会员、MERCHANT 商户、默认： MEMBER
        /// </summary>
       // public string _targetUserType { get; set; }
        /// <summary>
        /// Y 转入金额：保留两位小数，保留分
        /// </summary>
        //public string amount { get; set; }
        /// <summary>
        /// // Y 平台用户编号( 转入商户 )
        /// </summary>
        //public string targetPlatformUserNo { get; set; }

        /// <summary>
        /// Y 资金明细记录
        /// </summary>
        public List<ToCpTransactionDetail> details { get; set; }
    }
}
