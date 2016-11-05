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
    /// 3.5. 自动转账授权
    /// </summary>
    public class Auto_Transaction : YeePayConfig
    {
        public Auto_Transaction()
        {
            //Y 业务类型 TENDER 投标、REPAYMENT 还款、CREDIT_ASSIGNMENT 债权转让、TRANSFER 转账、COMMISSION 分润，仅在资金转账明细中使用  默认：TRANSFER
            _bizType = EnumBizType.TRANSFER.ToEnumDesc();
            _userType = EnumUserType.MERCHANT.ToEnumDesc(); //出款人用户类型
        }

        /// <summary>
        /// Y 标的号
        /// </summary>
        public string tenderOrderNo { get; set; }

        /// <summary>
        /// Y 资金明细记录
        /// </summary>
        public List<ToCpTransactionDetail> details { get; set; }

    }
}
