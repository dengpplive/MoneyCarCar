using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.YeePay.RequestModel;
using MoneyCarCar.Models.YeePay.YeePayEnum;
using RECHARGE_RECORD = MoneyCarCar.Models.YeePay.Response.QUERY.RECHARGE_RECORD;
using CP_TRANSACTION = MoneyCarCar.Models.YeePay.Response.QUERY.CP_TRANSACTION;
using WITHDRAW_RECORD = MoneyCarCar.Models.YeePay.Response.QUERY.WITHDRAW_RECORD;

namespace MoneyCarCar.DataApi.Controllers
{
    public class ServicesController : ApiController
    {
        SystemClaimsOper claims = new SystemClaimsOper();
        SystemRequestRecordOper request = new SystemRequestRecordOper();
        SystemInterestSettlementOper interest = new SystemInterestSettlementOper();
        YeePay yeepay = new YeePay();

        /// <summary>
        /// 查询单笔订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<bool> GetScanList()
        {
            List<SystemRequestRecord> list = request.GetList();
            string errorMsg = "";
            foreach (SystemRequestRecord item in list)
            {
                Query query = new Query();
                query.requestNo = item.Id + "";
                switch (item.RequestType)
                {
                    case 2://充值
                        {
                            query.mode = EnumMode.RECHARGE_RECORD.ToString();
                            BaseResultDto<RECHARGE_RECORD.response> result  = yeepay.QUERY<RECHARGE_RECORD.response>(query);
                            request.InquiryQueryBack(result.Tag.requestNo, result.Tag.code.Equals("1") && result.Tag.status.Equals("SUCCESS"), out errorMsg);
                            break;
                        }
                    case 3://投资
                    case 8://结息
                    case 9://返还本金
                        {
                            query.mode = EnumMode.CP_TRANSACTION.ToString();
                            BaseResultDto<CP_TRANSACTION.response> result = yeepay.QUERY<CP_TRANSACTION.response>(query);
                            request.InquiryQueryBack(result.Tag.requestNo, result.Tag.code.Equals("1") && result.Tag.status.Equals("DIRECT") && result.Tag.subStatus.Equals("SUCCESS"), out errorMsg);
                            break;
                        }
                    case 4://提款
                        {
                            query.mode = EnumMode.WITHDRAW_RECORD.ToString();
                            BaseResultDto<WITHDRAW_RECORD.response> result  = yeepay.QUERY<WITHDRAW_RECORD.response>(query);
                            request.InquiryQueryBack(result.Tag.requestNo, result.Tag.code.Equals("1") && result.Tag.status.Equals("SUCCESS") && result.Tag.remitStatus.Equals("REMIT_SUCCESS"), out errorMsg);
                            break;
                        }
                }
            }
            return null;
        }

        /// <summary>
        /// 结息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<bool> InterestSettlement_Add()
        {
            List<SystemRequestRecord> list = interest.GetList();
            foreach (SystemRequestRecord item in list)
            {
                MoneyCarCar.Models.YeePay.RequestModel.Direct_Transaction direct_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Direct_Transaction();
                direct_Transaction.requestNo = item.Id + "";
                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();

                paydetail.amount = item.RequestMoney.ToMoney(2);
                paydetail.targetPlatformUserNo = item.UserId + "";
                //paydetail.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail.bizType = direct_Transaction._bizType;
                details.Add(paydetail);

                direct_Transaction.details = details;
                yeepay.DIRECT_TRANSACTION(direct_Transaction);

            }
            return new BaseResultDto<bool> { IsSeccess = true, Tag = true };
        }
    
        /// <summary>
        /// 返还本金
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<bool> ReturnPrincipal()
        {
            List<SystemRequestRecord> list = claims.GetReturnPrincipalList();//查询需要返还本金的处理
            Log.RecordLog("Services", list.Count.ToString(), false);
            foreach (SystemRequestRecord item in list)
            {
                MoneyCarCar.Models.YeePay.RequestModel.Auto_Transaction direct_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Auto_Transaction();
                direct_Transaction.requestNo = item.Id + "";
                direct_Transaction.platformUserNo = item.RequestMark.Split('-')[0];
                direct_Transaction._bizType = EnumBizType.REPAYMENT.ToString();
                direct_Transaction._userType = EnumUserType.MEMBER.ToString(); //出款人用户类型
                direct_Transaction.tenderOrderNo = item.RequestMark.Split('-')[1];

                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();

                paydetail.amount = item.RequestMoney.ToMoney(2);
                paydetail.targetPlatformUserNo = item.UserId + "";
                paydetail.targetUserType = EnumUserType.MEMBER.ToString(); // 用户类型
                paydetail.bizType = direct_Transaction._bizType;
                details.Add(paydetail);

                direct_Transaction.details = details;
                yeepay.AUTO_TRANSACTION(direct_Transaction);
            }
            return new BaseResultDto<bool> { IsSeccess = true, Tag = true };
        }
    }
}
