using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models;

namespace MoneyCarCar.DAL
{
    /// <summary>
    /// 易宝支付业务处理类: 处理回调
    /// </summary>
    public class YeePayOper
    {
        SQLHelper db = SQLHelper.Single;

        #region 2. 处理回调（网关接口）

        /// <summary>
        /// 2.1 注册
        /// </summary>
        /// <param name="platformUserNo"></param>
        /// <returns></returns>
        public bool ToRegister(string platformUserNo)
        {
            RQProcParam proc = new RQProcParam();
            proc.ProcName = "Pro_ToRegister";
            proc.DicParam.Add("Uid", platformUserNo);

            BaseHelper baseHelper = new BaseHelper();
            return baseHelper.ExecByProc(proc) > 0 ? true : false;

            //SystemUsersOper systemUsersOper = new SystemUsersOper();
            //return systemUsersOper.UpdateById(platformUserNo,1);
        }
        /// <summary>
        /// 2.2 充值
        /// </summary>
        /// <param name="platformUserNo"></param>
        /// <param name="requestNo"></param>
        /// <returns></returns>
        public bool ToRecharge(string platformUserNo, string requestNo)
        {
            RQProcParam proc = new RQProcParam();
            proc.ProcName = "pro_Recharge";
            proc.DicParam.Add("Uid", platformUserNo);
            proc.DicParam.Add("PayNo", requestNo);

            BaseHelper baseHelper = new BaseHelper();
            return baseHelper.ExecByProc(proc) > 0 ? true : false;
        }
        /// <summary>
        /// 2.3 提现
        /// </summary>
        /// <param name="platformUserNo"></param>
        /// <param name="requestNo"></param>
        /// <returns></returns>
        public bool ToWithdraw(string platformUserNo, string requestNo)
        {
            RQProcParam proc = new RQProcParam();
            proc.ProcName = "pro_Withdraw";
            proc.DicParam.Add("Uid", platformUserNo);
            proc.DicParam.Add("PayNo", requestNo);

            BaseHelper baseHelper = new BaseHelper();
            return baseHelper.ExecByProc(proc) > 0 ? true : false;
        }
        /// <summary>
        /// 2.4 绑卡     
        /// </summary>
        /// <param name="bankCardNo">卡号</param>
        /// <param name="bank">开户行</param>
        /// <param name="platformUserNo">商户号</param>
        /// <param name="requestNo">请求流水号</param>
        /// <returns></returns>
        public bool ToBindBank(string bankCardNo, string bank, string platformUserNo, string requestNo)
        {
            RQProcParam proc = new RQProcParam();
            proc.ProcName = "Pro_BankCard";

            proc.DicParam.Add("Uid", platformUserNo);
            proc.DicParam.Add("BankCardNumber", bankCardNo);
            proc.DicParam.Add("OpenAnAccountBankCard", bank);
             proc.DicParam.Add("PayNo", requestNo);

            BaseHelper baseHelper = new BaseHelper();
            return baseHelper.ExecByProc(proc) > 0 ? true : false;
        }
        /// <summary>
        /// 2.5 取消绑卡     
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        public bool ToUnbindBankCard(string platformUserNo)
        {
            //  需要修改成 存储过程用事务执行操作
            SystemUsersOper systemUsersOper = new SystemUsersOper();
            if (systemUsersOper.UpdateById(platformUserNo, 0))
            {
                SystemBankCardOper systemBankCardOper = new SystemBankCardOper();
                return systemBankCardOper.UpdateByUserId(platformUserNo);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 2.6 企业用户注册
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        public bool ToEnterpriseRegister()
        {
           return false;
        }

        /// <summary>
        /// 2.7. 转账 （用于投资）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="PayNo"></param>
        /// <returns></returns>
        public bool ToCpTransaction(string PayNo, out string errorMsg)
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_Transaction_PayCompleted");
            db.AddInputParameter(cmd, "@PayNo", System.Data.DbType.String, PayNo);
            db.AddOutputParameter(cmd, "@ErrorMsg", System.Data.DbType.String, 4);
            db.AddReturnValueParameter(cmd, "@ReturnValue", System.Data.DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            errorMsg = db.GetParameter(cmd, "@ErrorMsg").Value.ToString();
            return db.GetParameter(cmd, "@ReturnValue").Value.ToInt() == 1;
        }


        /// <summary>
        /// 2.8 自动投标授权
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        public bool ToAuthorizeAutoTransfer()
        {
            return false;
        }
        /// <summary>
        /// 2.9 自动还款授权  
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        public bool ToAuthorizeAutoRepayment()
        {
            return false;
        }

        #endregion

        #region 3. 处理回调（直连接口）

        /// <summary>
        /// 3.4. 直接转账 (用于结算利息)、3.5 自动转账（自动还款）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="PayNo"></param>
        /// <returns></returns>
        public bool Direct_Transaction(string PayNo)
        {
            string errorMsg = "";
            return ToCpTransaction(PayNo, out  errorMsg);
        }

        ///// <summary>
        ///// 3.5. 自动转账授权
        ///// </summary>
        ///// <param name="toRegister"></param>
        ///// <returns></returns>
        //public bool Direct_Transaction()
        //{
        //     return false;
        //}

        ///// <summary>
        ///// 3.7. 转账确认
        ///// </summary>
        ///// <param name="toRegister"></param>
        ///// <returns></returns>
        //public bool Direct_Transaction()
        //{
        //     return false;
        //}

       
        #endregion

    }
}
