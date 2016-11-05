using MoneyCarCar.Commons.Enum;
using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Commons;
namespace MoneyCarCar.DataApi.Controllers
{
    public class ClaimsApplayController : ApiController
    {
        BaseHelper helper = new BaseHelper();


        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemBorrowerApply> GetList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemBorrowerApply> list = helper.GetPagerList<SystemBorrowerApply>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemBorrowerApply> mc = new ModelByCount<SystemBorrowerApply>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>      
        [HttpGet]
        public BaseResultDto<string> Delete(int Id)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (helper.DeleteById<SystemBorrowerApply>(Id))
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }

        /// <summary>
        /// 获取申请的债权信息
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApplayClaimsDto GetApplayClaims(int ApplyId)
        {
            SystemBorrowerApply systemBorrowerApply = helper.GetModelById<SystemBorrowerApply>(ApplyId);
            //借款人账号和申请的债权 唯一对应一条债权
            string sqlwhere = string.Format(" BorrowerID={0} and ClaimsApplayID={1}", systemBorrowerApply.BorrowerID, systemBorrowerApply.Id);
            SystemClaims systemClaims = helper.GetList<SystemClaims>(sqlwhere).FirstOrDefault();
            if (systemClaims == null)
            {
                systemClaims = new SystemClaims();
                systemClaims.EarningsStartTime = System.DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                systemClaims.InvestmentEndTime = System.DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            }
            systemClaims.BorrowerID = systemBorrowerApply.BorrowerID;
            systemClaims.ClaimsApplayID = systemBorrowerApply.Id;
            systemClaims.Borrower = systemBorrowerApply.BorrowerName;
            systemClaims.LoanAmount = systemBorrowerApply.LoanAmount;
            //从字典表获取所有设置的小图标           
            sqlwhere = string.Format(" DicKey ='{0}' ", EnumDictionary.SmallIcon);
            List<SystemDictionary> dics = helper.GetList<SystemDictionary>(sqlwhere);
            List<IconProperty> ShowIcons = new List<IconProperty>();
            List<IconProperty> ShowIconsAttrCode = new List<IconProperty>();
            List<IconModel> icoList = new List<IconModel>();
            if (systemClaims.Icons != "")
            {
                icoList = systemClaims.Icons.ToModel<List<IconModel>>();
            }
            bool IsOpen = false;
            dics.ForEach(p =>
            {
                //类型^标题^弹出提示^背景样式字符串^日息
                string[] strArr = p.DicValue.Split('^');
                if (strArr.Length >= 4)
                {
                    string AtrrCode = strArr.Length >= 5 ? strArr[4] : "";
                    if (AtrrCode != "")
                    {
                        IsOpen = icoList.Exists(p1 => p1.IconType.Trim() == strArr[0].Trim() && p1.AtrrCode == AtrrCode);
                        ShowIconsAttrCode.Add(new IconProperty()
                        {
                            IsOpen = IsOpen,
                            AddShow = p.DicType,//显示
                            IconType = strArr[0],//类型
                            Title = strArr[1],//标题
                            TipMessage = strArr[2],//弹出提示
                            BackgroundClass = strArr[3],//背景样式字符串
                            AtrrCode = AtrrCode//还款方式                                                       
                        });
                    }
                    else
                    {
                        IsOpen = icoList.Exists(p1 => p1.IconType.Trim() == strArr[0].Trim());
                        ShowIcons.Add(new IconProperty()
                        {
                            IsOpen = IsOpen,
                            AddShow = p.DicType,//显示    
                            IconType = strArr[0],//类型
                            Title = strArr[1],//标题
                            TipMessage = strArr[2],//弹出提示
                            BackgroundClass = strArr[3],//背景样式字符串
                            AtrrCode = AtrrCode//还款方式                            
                        });
                    }
                }
            });
            var model = new ApplayClaimsDto()
            {
                Applay = systemBorrowerApply,
                Claims = systemClaims,
                ShowIcons = ShowIcons,
                ShowAttrCode = ShowIconsAttrCode
            };
            return model;
        }

        /// <summary>
        /// 申请借贷
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> ApplyBorrower(SystemBorrowerApply model)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (helper.Add<SystemBorrowerApply>(model) > 0)
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                    result.IsSeccess = true;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }
        /// <summary>
        /// 处理申请
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BaseResultDto<string> UpdateBorrowerApplyStatus(RQApplyData data)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                RQProcParam proc = new RQProcParam();
                proc.ProcName = "Proc_UpdateSystemBorrowerApply";
                proc.DicParam.Add("Id", data.ApplyId);
                proc.DicParam.Add("Publisher", data.Publisher);
                proc.DicParam.Add("OperatorUserId", data.OperatorUserId);
                proc.DicParam.Add("OperatorUserName", data.OperatorUserName);
                proc.DicParam.Add("IP", data.IP);
                if (helper.ExecByProc(proc) > 0)
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                    result.IsSeccess = true;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }
        /// <summary>
        /// 审核债权
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AduitApplayClaims(RQAduitApplay data)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                RQProcParam proc = new RQProcParam();
                proc.ProcName = "Proc_UpdateSystemBorrowerChecked";
                proc.DicParam.Add("ApplyID", data.ApplyID);
                proc.DicParam.Add("ClaimsID", data.ClaimsID);
                proc.DicParam.Add("ApproverUserName", data.ApproverUserName);
                proc.DicParam.Add("Succeed", data.Succeed);
                proc.DicParam.Add("OperatorUserId", data.OperatorUserId);
                proc.DicParam.Add("OperatorContent", data.OperatorContent);
                proc.DicParam.Add("IP", data.IP);
                if (helper.ExecByProc(proc) > 0)
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                    result.IsSeccess = true;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result.IsSeccess;
        }





    }
}
