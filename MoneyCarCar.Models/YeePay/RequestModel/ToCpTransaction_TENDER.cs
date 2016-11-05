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
    /// 2.7（2）投标［TENDER］］ :接口输入
    /// </summary>
    public class ToCpTransaction_TENDER : ToCpTransaction
    {
        public ToCpTransaction_TENDER()
        {
            _bizType = EnumBizType.TENDER.ToEnumDesc(); //投标
        }
        /// <summary>
        /// Y 项目编号
        /// </summary>
        public string tenderOrderNo { get; set; }
        /// <summary>
        ///  Y 项目名称
        /// </summary>
        public string tenderName { get; set; }
        /// <summary>
        ///  Y 项目金额
        /// </summary>
        public string tenderAmount { get; set; }
        /// <summary>
        /// Y 项目描述信息
        /// </summary>
        public string tenderDescription { get; set; }
        /// <summary>
        ///  Y 项目的借款人平台用户编号
        /// </summary>
        public string borrowerPlatformUserNo { get; set; }
    }
}
