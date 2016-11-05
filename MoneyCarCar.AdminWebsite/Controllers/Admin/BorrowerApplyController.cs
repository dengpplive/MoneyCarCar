using MoneyCarCar.AdminWebsite.Controllers.CommHelper;
using MoneyCarCar.Commons;
using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    public class BorrowerApplyController : BaseController
    {
        //
        // GET: /BorrowerApply/

        public ActionResult ApplayUserIndex()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    borrowerName = string.Empty,
                    borrowerType = string.Empty,
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now
                },
                urls = new
                {
                    search = "/BorrowerApply/SearchBorrowerApply",
                    edit = "/BorrowerApply/AddUpdateBorrowerApply",
                    del = "/BorrowerApply/DelBorrowerApply",
                    hand = "/BorrowerApply/HandApplay"
                }
            };
            return View(model);
        }

        //加载债权数据
        public ActionResult AduitEditApplay(int Id)
        {
            ApplayClaimsDto model = new ApplayClaimsDto();
            model = HttpHelper.CreatHelper().DoGetObject<ApplayClaimsDto>(string.Format("{0}ClaimsApplay/GetApplayClaims?ApplyId={1}", this.WebApiUrl, Id));
            if (model == null)
            {
                throw new Exception("加载失败,未获取到数据");
            }
            return View(model);
        }
        /// <summary>
        /// 添加或者编辑债权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddUpdateClaims(SystemClaims systemClaims, int clickType)
        {
            JsonResult result = null;
            try
            {
                Type type = systemClaims.GetType();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
                foreach (var item in properties)
                {
                    if (item.PropertyType == typeof(string) && item.GetValue(systemClaims, null) == null)
                    {
                        item.SetValue(systemClaims, "", null);
                    }
                }
                //操作数据之前  对的补齐和正确性的验证
                //审核通过之类的该状态的调用存储过程 暂时未处理
                //......                

                if (clickType == 1)//审核未通过
                {
                    systemClaims.IsApproved = false;
                    systemClaims.ApproverUserName = this.UserInfo.RealName;
                    systemClaims.ApproverTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else if (clickType == 2)//审核通过
                {
                    systemClaims.IsApproved = true;
                    systemClaims.ApproverUserName = this.UserInfo.RealName;
                    systemClaims.ApproverTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                systemClaims.DetailsImages1 = HttpUtility.UrlDecode(systemClaims.DetailsImages1);
                systemClaims.DetailsImages2 = HttpUtility.UrlDecode(systemClaims.DetailsImages2);
                systemClaims.DetailsImages3 = HttpUtility.UrlDecode(systemClaims.DetailsImages3);
                systemClaims.DetailsImages4 = HttpUtility.UrlDecode(systemClaims.DetailsImages4);
                systemClaims.DetailsImages5 = HttpUtility.UrlDecode(systemClaims.DetailsImages5);

                DataFornat dataformat = new DataFornat();
                systemClaims.TitleImagePath = dataformat.ReplaceDomain(systemClaims.TitleImagePath, AppConfigHelper.MainSiteUrl);
                systemClaims.DetailsImages1 = dataformat.ReplaceImageSrc(systemClaims.DetailsImages1, AppConfigHelper.MainSiteUrl);
                systemClaims.DetailsImages2 = dataformat.ReplaceImageSrc(systemClaims.DetailsImages2, AppConfigHelper.MainSiteUrl);
                systemClaims.DetailsImages3 = dataformat.ReplaceImageSrc(systemClaims.DetailsImages3, AppConfigHelper.MainSiteUrl);
                systemClaims.DetailsImages4 = dataformat.ReplaceImageSrc(systemClaims.DetailsImages4, AppConfigHelper.MainSiteUrl);
                systemClaims.DetailsImages5 = dataformat.ReplaceImageSrc(systemClaims.DetailsImages5, AppConfigHelper.MainSiteUrl);



                string strIcons = HttpUtility.UrlDecode(systemClaims.Icons);
                List<IconModel> IconModelList = new List<IconModel>();
                string[] strArr = strIcons.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in strArr)
                {
                    IconModel model = new IconModel();
                    string[] strItem = item.Split('^');
                    if (strItem.Length >= 3)
                    {
                        model.IconType = strItem[0];
                        model.Title = strItem[1];
                        model.Description = strItem[2];
                        model.StyleName = strItem[3];
                    }
                    if (strItem.Length >= 4)
                        model.AtrrCode = strItem[4];
                    IconModelList.Add(model);
                }
                systemClaims.Icons = IconModelList.ToJsonString<List<IconModel>>();

                //添加或者修改
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                string message = string.Empty;
                if (systemClaims.ID > 0)
                {
                    #region 修改
                    RQUpdate<SystemClaims> model = new RQUpdate<SystemClaims>();
                    model.Tag = systemClaims;
                    bool rs = false;
                    if (clickType == 0 || clickType == 2)//修改资料
                    {
                        //更新修改的字段数据
                        model.UpdateFileds.AddRange(new string[]{
                            "Title","TitleImagePath","LoanAmount","APR","LoanPeriod","SingleAmount",
                            "GuaranteeWay","RepaymentWat","InvestmentEndTime","EarningsStartTime","PawnSpec",
                            "Icons","DetailsImages1","DetailsImages2","DetailsImages3","DetailsImages4","DetailsImages5"
                        });
                        rs = HttpHelper.CreatHelper().DoPostObject<bool>(string.Format("{0}Claims/UpdatePartal", this.WebApiUrl), model);
                    }
                    if (clickType == 2 && rs)
                    {
                        //更新审核字段数据                     
                        RQAduitApplay aduitApplay = new RQAduitApplay();
                        aduitApplay.ApplyID = systemClaims.ClaimsApplayID;
                        aduitApplay.ClaimsID = systemClaims.ID;
                        aduitApplay.ApproverUserName = systemClaims.ApproverUserName;
                        aduitApplay.OperatorUserId = this.UserInfo.ID;
                        aduitApplay.Succeed = systemClaims.IsApproved;
                        aduitApplay.IP = this.RequestIP;
                        aduitApplay.OperatorContent = (clickType == 1 ? "审核未通过" : "审核通过");
                        rs = HttpHelper.CreatHelper().DoPostObject<bool>(string.Format("{0}ClaimsApplay/AduitApplayClaims", this.WebApiUrl), aduitApplay);
                    }
                    //修改                 
                    if (rs)
                    {
                        resultDto.ErrorCode = 1;
                        resultDto.ErrorMsg = "成功";
                    }
                    else
                    {
                        resultDto.ErrorMsg = "失败";
                    }
                    #endregion
                }
                else
                {
                    systemClaims.PublishTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    systemClaims.Publisher = this.UserInfo.RealName;
                    int rs = HttpHelper.CreatHelper().DoPostObject<int>(string.Format("{0}Claims/Add", this.WebApiUrl), systemClaims);
                    //添加                    
                    if (rs > 0)
                    {
                        resultDto.ErrorCode = 1;
                        resultDto.ErrorMsg = "成功";
                    }
                    else
                    {
                        resultDto.ErrorMsg = "失败";
                    }
                }
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 查询债权申请
        /// </summary>       
        public JsonResult SearchBorrowerApply(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                //表单的实体    

                //借款人姓名
                if (form["borrowerName"] != null && !string.IsNullOrEmpty(form["borrowerName"]))
                {
                    string borrowerName = form["borrowerName"].ToString();
                    sbCon.AppendFormat(" and BorrowerName = '{0}'", borrowerName.Replace("'", ""));
                }
                //申请状态
                if (form["borrowerType"] != null && !string.IsNullOrEmpty(form["borrowerType"]))
                {
                    string borrowerType = form["borrowerType"].ToString();
                    sbCon.AppendFormat(" and BorrowerType = {0} ", borrowerType.Replace("'", ""));
                }
                //申请日期         
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and BorrowerTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
                }
                string OrderBy = string.Empty;
                //排序的东西
                if (form["sort"] != null && form["order"] != null)
                {
                    string sort = form["sort"].ToString();
                    string order = form["order"].ToString();
                    if (!string.IsNullOrEmpty(sort))
                    {
                        OrderBy = sort + " " + order;
                    }
                }
                RQPagerDto pager = new RQPagerDto();
                pager.PageSize = rows;
                pager.PageIndex = page;
                pager.Where = sbCon.ToString();
                pager.OrderBy = OrderBy;
                ModelByCount<SystemBorrowerApply> applyData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemBorrowerApply>>(string.Format("{0}ClaimsApplay/GetList", this.WebApiUrl), pager);
                result = Json(new { total = applyData.AllCount, rows = applyData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult DelBorrowerApply(int Id)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<string>>(string.Format("{0}ClaimsApplay/Delete?Id={1}", this.WebApiUrl, Id));
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 修改状态 正在审核
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult HandApplay(int Id)
        {
            JsonResult result = null;
            try
            {
                RQApplyData model = new RQApplyData();
                model.ApplyId = Id;
                model.Publisher = this.UserInfo.RealName;
                model.OperatorUserId = this.UserInfo.ID;
                model.OperatorUserName = this.UserInfo.RealName;
                model.IP = this.RequestIP;
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}ClaimsApplay/UpdateBorrowerApplyStatus", this.WebApiUrl), model);
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }


    }
}
