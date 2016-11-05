using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay
{
    /// <summary>
    /// 同步地址
    /// </summary>
    public class CallbackUrl
    {
        public CallbackUrl(string url)
        {
            // 默认地址
            toRegister = url;
            toRecharge = url;
            toWithdraw = url;
            toBindBankCard = url;
            toUnbindBankCard = url;
            toEnterpriseRegister = url;
            toCpTransaction_TRANSFER = url;
            toCpTransaction_TENDER = url;
            toCpTransaction_REPAYMENT = url;
            toCpTransaction_CREDIT_ASSIGNMENT = url;
            toAuthorizeAutoTransfer = url;
            toAuthorizeAutoRepayment = url;
            account_info = url;
            freeze = url;
            unFreeze = url;
            direct_Transaction = url;
            auto_Transaction = url;
            query = url;
            complete_Transaction = url;
        }

        /// <summary>
        /// 21 注册      
        /// </summary>
        public string toRegister { get; set; }
        /// <summary>
        /// 22 充值      
        /// </summary>
        public string toRecharge { get; set; }
        /// <summary>
        /// 23 提现    
        /// </summary>
        public string toWithdraw { get; set; }
        /// <summary>
        /// 24 绑卡      
        /// </summary>
        public string toBindBankCard { get; set; }
        /// <summary>
        /// 25 取消绑卡  
        /// </summary>
        public string toUnbindBankCard { get; set; }
        /// <summary>
        /// 26 企业用户注册
        /// </summary>
        public string toEnterpriseRegister { get; set; }
        /// <summary>
        /// 27 转账 1
        /// </summary>
        public string toCpTransaction_TRANSFER { get; set; }
        /// <summary>
        ///  2.7  （2）投标［TENDER］
        /// </summary>
        public string toCpTransaction_TENDER { get; set; }
        /// <summary>
        /// 2.7 （3）还款［REPAYMENT］
        /// </summary>
        public string toCpTransaction_REPAYMENT { get; set; }
       /// <summary>
        ///  2.7  （4）债权转让［CREDIT_ASSIGNMENT］
        /// </summary>
        public string toCpTransaction_CREDIT_ASSIGNMENT { get; set; }
        /// <summary>
        /// 28 自动投标授权
        /// </summary>
        public string toAuthorizeAutoTransfer { get; set; }
        /// <summary>
        /// 29 自动还款授权  
        /// </summary>
        public string toAuthorizeAutoRepayment { get; set; }
        /// <summary>
        /// 31 账户查询
        /// </summary>
        public string account_info { get; set; }
        /// <summary>
        /// 32 资金冻结
        /// </summary>
        public string freeze { get; set; }
        /// <summary>
        /// 33 资金解冻
        /// </summary>
        public string unFreeze { get; set; }
        /// <summary>
        /// 34 直接转账
        /// </summary>
        public string direct_Transaction { get; set; }
        /// <summary>
        /// 35 自动转账授权
        /// </summary>
        public string auto_Transaction { get; set; }
        /// <summary>
        /// 36 单笔业务查询
        /// </summary>
        public string query { get; set; }
        /// <summary>
        /// 37 转账确认
        /// </summary>
        public string complete_Transaction { get; set; }
    }
}
