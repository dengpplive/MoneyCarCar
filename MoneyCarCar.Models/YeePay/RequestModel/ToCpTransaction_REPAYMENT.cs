using MoneyCarCar.Models.YeePay.YeePayEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCarCar.Website.Controllers.CommHelper;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 2.7（3）还款［REPAYMENT］ :接口输入
    /// </summary>
    public class ToCpTransaction_REPAYMENT : ToCpTransaction
    {
        public ToCpTransaction_REPAYMENT()
        {
            _bizType = EnumBizType.REPAYMENT.ToEnumDesc(); //还款
        }
        /// <summary>
        /// Y 项目编号
        /// </summary>
        public string tenderOrderNo { get; set; }
        ///// <summary>
        ///// Y 平台抽成
        ///// </summary>
        //public string platformNoAmount { get; set; }
    }
}
