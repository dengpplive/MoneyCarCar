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
    ///  2.7（1）转账［TRANSFER］ :接口输入 
    /// </summary>
    public class ToCpTransaction_TRANSFER : ToCpTransaction
    {
        public ToCpTransaction_TRANSFER()
        {
            _bizType = EnumBizType.TRANSFER.ToEnumDesc(); // 转账
        }
    }
}
