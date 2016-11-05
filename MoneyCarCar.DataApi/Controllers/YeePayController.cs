using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.YeePay;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.DAL;

namespace MoneyCarCar.DataApi.Controllers
{
    public class YeePayController : ApiController
    {
        YeePay yeePay = new YeePay();

        #region 2.网关接口

        /// <summary>
        /// 2.1 注册
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToRegisterRequest(Models.YeePay.RequestModel.ToRegister toRegister)
        {
            return yeePay.ToRegister(toRegister);
        }
        /// <summary>
        /// 2.2 充值
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToRechargeRequest(Models.YeePay.RequestModel.ToRecharge toRecharge)
        {
            return yeePay.ToRecharge(toRecharge);
        }
        /// <summary>
        /// 2.3 提现
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToWithdrawRequest(Models.YeePay.RequestModel.ToWithdraw toWithdraw)
        {
            return yeePay.ToWithdraw(toWithdraw);
        }
        /// <summary>
        /// 2.4 绑卡     
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToBindBankCardRequest(Models.YeePay.RequestModel.ToBindBankCard toBindBankCard)
        {
            return yeePay.ToBindBankCard(toBindBankCard);
        }
        /// <summary>
        /// 2.5 取消绑卡     
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToUnbindBankCardRequest(Models.YeePay.RequestModel.ToUnbindBankCard toUnbindBankCard)
        {
            return yeePay.ToUnbindBankCard(toUnbindBankCard);
        }
        /// <summary>
        /// 2.6 企业用户注册
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToEnterpriseRegisterRequest(Models.YeePay.RequestModel.ToEnterpriseRegister toEnterpriseRegister)
        {
            return yeePay.ToEnterpriseRegister(toEnterpriseRegister);
        }
        /// <summary>
        /// 2.7 转账
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> ToCpTransactionRequest(Models.YeePay.RequestModel.ToCpTransaction_TRANSFER toCpTransaction)
        {
            return yeePay.ToCpTransaction_TRANSFER(toCpTransaction);
        }
        /// <summary>
        /// 2.8 自动投标授权
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public string ToAuthorizeAutoTransferRequest(Models.YeePay.RequestModel.ToAuthorizeAutoTransfer toAuthorizeAutoTransfer)
        {
            return "";
        }
        /// <summary>
        /// 2.9 自动还款授权  
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public string ToAuthorizeAutoRepaymentRequest(Models.YeePay.RequestModel.ToAuthorizeAutoRepayment toAuthorizeAutoRepayment)
        {
            return "";
        }

        #endregion

        #region 3.直连接口

        /// <summary>
        /// 3.1 账户查询
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response> Account_info(Models.YeePay.RequestModel.Account_Info account_info)
        {
            return yeePay.ACCOUNT_INFO(account_info);
        }
        
        #endregion
    }
}