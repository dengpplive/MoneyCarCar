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
    ///  2.7（4）债权转让［CREDIT_ASSIGNMENT］ :接口输入 
    /// </summary>
    public class ToCpTransaction_CREDIT_ASSIGNMENT : ToCpTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        public ToCpTransaction_CREDIT_ASSIGNMENT()
        {
            _bizType = EnumBizType.CREDIT_ASSIGNMENT.ToEnumDesc();//债权转让
        }
        /// <summary>
        ///  Y 项目的借款人平台用户编号
        /// </summary>
        public string borrowerPlatformUserNo { get; set; }
        /// <summary>
        ///  Y 债权购买人
        /// </summary>
        public string creditorPlatformUserNo { get; set; }
        /// <summary>
        ///  Y 需要转让的投资记录流水号
        /// </summary>
        public string originalRequestNo { get; set; }
        /// <summary>
        /// Y 项目编号
        /// </summary>
        public string tenderOrderNo { get; set; }
    }
}
