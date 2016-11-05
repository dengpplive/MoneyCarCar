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
    /// 资金明细记录
    /// </summary>
    public class ToCpTransactionDetail
    {
        public ToCpTransactionDetail()
        {
            targetUserType = EnumUserType.MEMBER.ToEnumDesc();//默认个人
        }

        /// <summary>
        /// Y 转入金额：保留分
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// Y 转入商户 用户类型：MEMBER 个人会员、MERCHANT 商户会员（理财公司），默认：MEMBER
        /// </summary>
        public string targetUserType { get; set; }
        /// <summary>
        /// Y 平台用户编号：转入商户
        /// </summary>
        public string targetPlatformUserNo { get; set; }
        /// <summary>
        /// Y 根据业务的不同，需要传入不同的值，见【业务类型】： TENDER 投标、REPAYMENT 还款、CREDIT_ASSIGNMENT 债权转让、TRANSFER 转账、COMMISSION 分润，仅在资金转账明细中使用
        /// </summary>
        public string bizType { get; set; }
    }
}
