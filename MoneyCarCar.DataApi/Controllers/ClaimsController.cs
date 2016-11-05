using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Models;
using MoneyCarCar.DAL;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using System.Web;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.YeePay.RequestModel;
using MoneyCarCar.Models.ModelDto.ResParam;
using MoneyCarCar.Models.YeePay;
using MoneyCarCar.Models.YeePay.YeePayEnum;
using System.Data;

namespace MoneyCarCar.DataApi.Controllers
{
    /// <summary>
    /// 债权
    /// </summary>
    public class ClaimsController : ApiController
    {
        BaseHelper helper = new BaseHelper();
        SystemClaimsOper claimsOper = new SystemClaimsOper();
        SystemClaimsDetailsOper detail = new SystemClaimsDetailsOper();
        SystemUsersOper userOper = new SystemUsersOper();
        YeePay yeePay = new YeePay();

        //添加债权
        [HttpPost]
        public int Add(SystemClaims model)
        {
            return helper.Add<SystemClaims>(model);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public SystemClaims GetClaims(int value)
        {
            return helper.GetModelById<SystemClaims>(value);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemClaims> GetList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemClaims> list = helper.GetPagerList<SystemClaims>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemClaims> mc = new ModelByCount<SystemClaims>();
            mc.PageIndex = pager.PageIndex;
            mc.PageSize = pager.PageSize;
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Update(SystemClaims model)
        {
            return helper.Update<SystemClaims>(model);
        }
        /// <summary>
        ///  更新具体的部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdatePartal(RQUpdate<SystemClaims> model)
        {
            return helper.Update<SystemClaims>(model.Tag, model.UpdateFileds);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public BaseResultDto<string> Delete(int Id)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            if (helper.DeleteById<SystemClaims>(Id))
            {
                resultDto.ErrorCode = 1;
                resultDto.ErrorMsg = "删除成功";
            }
            else
            {
                resultDto.ErrorCode = -1;
                resultDto.ErrorMsg = "删除失败";
            }
            return resultDto;
        }
        /// <summary>
        /// 查询债权明细
        /// </summary>
        /// <param name="ClaimsId"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<ClaimsDetailsDto> GetSystemClaimsDetails(int ClaimsId)
        {
            BaseResultDto<ClaimsDetailsDto> baseResultDto = new BaseResultDto<ClaimsDetailsDto>();
            SystemClaimsOper systemClaimsOper = new SystemClaimsOper();
            ClaimsDetailsDto claimsDetailsDto = systemClaimsOper.GetInvestorsClaimsDetails(ClaimsId);
            if (claimsDetailsDto.UserInfoList.Count > 0)
            {
                baseResultDto.IsSeccess = true;
                baseResultDto.ErrorMsg = "成功";
                baseResultDto.ErrorCode = 1;
            }
            else
            {
                baseResultDto.ErrorMsg = "没有数据";
            }
            baseResultDto.Tag = claimsDetailsDto;
            return baseResultDto;
        }
        /// <summary>
        /// 添加债券明细（投资）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> AddSystemClaimsDetails(RQSubmitOrder model)
        {
            BaseResultDto<PostBaseYeePayPar> result = new BaseResultDto<PostBaseYeePayPar>();
            SystemClaims claims = claimsOper.GetModel("ID = " + model.InvestorsID);
            SystemUsers user = userOper.GetUserInfo(model.UserID);

            int days = (int)(claims.EarningsStartTime.ToDateTime().AddMonths(claims.LoanPeriod) - claims.EarningsStartTime.ToDateTime()).TotalDays;
            string errorMsg = "";
            int targetUserID = 0;
            decimal dayEarnings = (claims.SingleAmount * model.BuyCount * claims.APR / 100 / 365).ToMoney(2).ToDecimal();

            decimal virtualMoney = userOper.GetUserVirtualMoney(model.UserID).Tag;
            decimal virtualMoneyDayEarnings = (virtualMoney * claims.APR / 100 / 365).ToMoney(2).ToDecimal();

            int detailID = detail.SystemClaimsDetails_Add(user.ID, claims.ID, model.BuyCount, dayEarnings, dayEarnings * days, model.IsUserBounty, virtualMoney,virtualMoneyDayEarnings, out errorMsg, out targetUserID);

            result.IsSeccess = detailID > 0;
            if (!result.IsSeccess)
            {
                result.ErrorMsg = errorMsg;
            }
            else
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_TENDER toCpTransaction = new MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_TENDER();

                toCpTransaction.platformUserNo = user.ID + "";
                toCpTransaction.requestNo = detailID + ""; ;

                //（2）投标［TENDER］

                toCpTransaction.tenderOrderNo = claims.ID + "";
                toCpTransaction.tenderName = claims.Title;
                toCpTransaction.tenderAmount = claims.LoanAmount.ToMoney(2);
                toCpTransaction.tenderDescription = claims.Title;
                toCpTransaction.borrowerPlatformUserNo = claims.BorrowerID + "";


                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                paydetail.amount = (claims.SingleAmount * model.BuyCount).ToMoney(2);
                paydetail.targetPlatformUserNo = claims.BorrowerID + "";
                //paydetail.targetUserType = EnumUserType.MERCHANT.ToEnumDesc(); // 用户类型
                paydetail.bizType = EnumBizType.TENDER.ToString();//转账

                details.Add(paydetail);

                toCpTransaction.details = details;

                return yeePay.ToCpTransaction_TENDER(toCpTransaction);
            }
            return result;
        }

        #region 统计
        [HttpGet]
        public DataTableCollection GetInvestment_Ing(int value)
        {
            return claimsOper.GetInvestment_Ing(value);
        }
        [HttpGet]
        public DataTable GetInvestment_All(int value)
        {
            return claimsOper.GetInvestment_All(value);
        }
        [HttpGet]
        public DataTable GetInvestment_Ed(int value)
        {
            return claimsOper.GetInvestment_Ed(value);
        }
        #endregion
    }
}