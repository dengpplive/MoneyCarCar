using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.NotifyModel.toWithdraw
{
    /// <summary>
    /// 2.3. 提现: 回调通知(notify) 
    /// </summary>
    public class notify : BaseNotify
    {
        private string _bankCardNo = "";
        /// <summary>
        /// Y 绑定的卡号
        /// </summary>
        public string bankCardNo
        {
            get { return _bankCardNo; }
            set { _bankCardNo = value; }
        }
        private string _bank = "";
        /// <summary>
        /// Y 【见银行代码】
        /// </summary>
        public string bank
        {
            get { return _bank; }
            set { _bank = value; }
        }
    }
}
