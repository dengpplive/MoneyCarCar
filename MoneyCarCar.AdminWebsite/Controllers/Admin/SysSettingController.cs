using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    public class SysSettingController : BaseController
    {
        //
        // GET: /SysSetting/

        public ActionResult Index()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now,
                    loginAccount = string.Empty,
                    realName = string.Empty,
                    idNumber = string.Empty,
                    userEmail = string.Empty,
                    userState = string.Empty,
                    userType = this.UserInfo.UserType
                },
                urls = new
                {
                    search = "/Account/SearchUsers",
                    setInfo = "/Account/setInfo",
                    add = "/Account/AddEditUser"
                }
            };
            return View(model);
        }


        #region 日志
        public ActionResult SystemLogIndex()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now,
                    userId = string.Empty,
                    userName = string.Empty,
                    operatorType = string.Empty,
                    businessType = string.Empty,
                    ip = string.Empty,
                    opCon = string.Empty
                },
                urls = new
                {
                    search = "/SysSetting/SearchSystemLog"
                }
            };
            return View(model);
        }
        /// <summary>
        /// 查询系统日志
        /// </summary>
        /// <param name="form"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult SearchSystemLog(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                if (form["userId"] != null && !string.IsNullOrEmpty(form["userId"]))
                {
                    string userId = form["userId"].ToString();
                    sbCon.AppendFormat(" and OperatorUserId ='{0}'", userId.Replace("'", ""));
                }
                if (form["userName"] != null && !string.IsNullOrEmpty(form["userName"]))
                {
                    string userName = form["userName"].ToString();
                    sbCon.AppendFormat(" and OperatorUserName like '%{0}%'", userName.Replace("'", ""));
                }
                if (form["operatorType"] != null
                    && !string.IsNullOrEmpty(form["operatorType"])
                    && form["operatorType"].ToString().Trim() != "0"
                    )
                {
                    string operatorType = form["operatorType"].ToString();
                    sbCon.AppendFormat(" and OperatorType = '{0}'", operatorType.Replace("'", ""));
                }
                if (form["businessType"] != null && !string.IsNullOrEmpty(form["businessType"]))
                {
                    string businessType = form["businessType"].ToString();
                    sbCon.AppendFormat(" and BusinessType = '{0}'", businessType.Replace("'", ""));
                }
                if (form["ip"] != null && !string.IsNullOrEmpty(form["ip"]))
                {
                    string ip = form["ip"].ToString();
                    sbCon.AppendFormat(" and OperatorIP = '{0}'", ip.Replace("'", ""));
                }
                if (form["opCon"] != null && !string.IsNullOrEmpty(form["opCon"]))
                {
                    string opCon = form["opCon"].ToString();
                    sbCon.AppendFormat(" and OperatorContent like '%{0}%'", opCon.Replace("'", ""));
                }
                //表单的实体             
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and OperatorTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                //分页
                RQPagerDto pager = new RQPagerDto();
                pager.PageSize = rows;
                pager.PageIndex = page;
                pager.Where = sbCon.ToString();
                pager.OrderBy = OrderBy;
                ModelByCount<SystemLog> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemLog>>(string.Format("{0}SysSetting/GetLogList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        #endregion


        #region 字典

        public ActionResult SysConso()
        {
            var model = new
            {
                searchForm = new SystemDictionary(),
                urls = new
                {
                    search = "/SysSetting/SearchSystemDic",
                    addEdit = "/SysSetting/addEditSystemDic",
                    del = "/SysSetting/DelSystemDic"
                }
            };
            return View(model);
        }
        public JsonResult SearchSystemDic(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                if (form["DicKey"] != null && !string.IsNullOrEmpty(form["DicKey"]))
                {
                    string DicKey = form["DicKey"].ToString();
                    sbCon.AppendFormat(" and DicKey ='{0}'", DicKey.Replace("'", ""));
                }
                if (form["DicValue"] != null && !string.IsNullOrEmpty(form["DicValue"]))
                {
                    string DicValue = form["DicValue"].ToString();
                    sbCon.AppendFormat(" and DicValue like '%{0}%'", DicValue.Replace("'", ""));
                }
                if (form["DicType"] != null
                    && !string.IsNullOrEmpty(form["DicType"]))
                {
                    string DicType = form["DicType"].ToString();
                    sbCon.AppendFormat(" and DicType = '{0}'", DicType.Replace("'", ""));
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
                //分页
                RQPagerDto pager = new RQPagerDto();
                pager.PageSize = rows;
                pager.PageIndex = page;
                pager.Where = sbCon.ToString();
                pager.OrderBy = OrderBy;
                ModelByCount<SystemDictionary> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemDictionary>>(string.Format("{0}SysSetting/GetDicList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        public JsonResult addEditSystemDic(SystemDictionary model, int opType)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                //添加或者修改               
                if (opType == 1)
                {
                    //修改
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}SysSetting/UpdateDic", this.WebApiUrl), model);
                }
                else if (opType == 2)
                {
                    //添加
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}SysSetting/AddDic", this.WebApiUrl), model);
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
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult DelSystemDic(int Id)
        {
            JsonResult result = null;
            try
            {
                //删除字典                
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<string>>(string.Format("{0}SysSetting/DeleteDic?Id={1}", this.WebApiUrl, Id));
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        #endregion

        #region 帮助



        public ActionResult SysHelpIndex()
        {
            BaseResultDto<List<SystemDictionary>> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<List<SystemDictionary>>>(string.Format("{0}SysSetting/GetAllHelpType", this.WebApiUrl));
            List<SystemDictionary> dicHelpType = resultDto.Tag;
            if (dicHelpType == null)
            {
                dicHelpType = new List<SystemDictionary>();
            }
            ViewBag.HelpType = dicHelpType;
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    askContent = string.Empty,
                    askAccount = string.Empty,
                    helpType = string.Empty,
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now
                },
                urls = new
                {
                    search = "/SysSetting/SearchSysHelp",
                    addEdit = "/SysSetting/addEditSysHelp",
                    del = "/SysSetting/DelHelp"
                }
            };
            return View(model);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="form"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SearchSysHelp(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                //提问内容
                if (form["askContent"] != null && !string.IsNullOrEmpty(form["askContent"]))
                {
                    string askContent = form["askContent"].ToString();
                    sbCon.AppendFormat(" and AskContent like '%{0}%'", askContent.Replace("'", ""));
                }
                //提问账号
                if (form["askAccount"] != null && !string.IsNullOrEmpty(form["askAccount"]))
                {
                    string askAccount = form["askAccount"].ToString();
                    sbCon.AppendFormat(" and AskAccount like '%{0}%'", askAccount.Replace("'", ""));
                }
                //问题类型
                if (form["helpType"] != null && !string.IsNullOrEmpty(form["helpType"])
                    && form["helpType"].ToString() != "0")
                {
                    string helpType = form["helpType"].ToString();
                    sbCon.AppendFormat(" and HelpType = {0} ", helpType.Replace("'", ""));
                }
                //提问日期       
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and AskDate between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                //分页
                RQPagerDto pager = new RQPagerDto();
                pager.PageSize = rows;
                pager.PageIndex = page;
                pager.Where = sbCon.ToString();
                pager.OrderBy = OrderBy;
                ModelByCount<SystemHelp> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemHelp>>(string.Format("{0}SysSetting/GetHelpList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 添加或者编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addEditSysHelp(SystemHelp systemHelp)
        {
            JsonResult result = null;
            try
            {

                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                //添加或者修改                
                if (systemHelp.Id > 0)
                {
                    //修改
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}SysSetting/UpdateHelp", this.WebApiUrl), systemHelp);
                }
                else
                {
                    systemHelp.ReplyAccount = this.UserInfo.UserName;
                    //systemHelp.ReplyDate = this.UserInfo.UserName;
                    //添加
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}SysSetting/AddHelp", this.WebApiUrl), systemHelp);
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
        /// 删除
        /// </summary>
        /// <param name="help"></param>
        /// <returns></returns>
        public JsonResult DelHelp(SystemHelp help)
        {
            JsonResult result = null;
            try
            {
                //删除帮助        
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<string>>(string.Format("{0}SysSetting/DeleteHelp?Id={1}", this.WebApiUrl, help.Id));
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        #endregion

        #region 问题反馈
        public ActionResult SysFeedbackIndex()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    userName = string.Empty,
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now
                },
                urls = new
                {
                    search = "/SysSetting/SearchFeedback",
                    addEdit = "/SysSetting/addEditFeedback"
                }
            };
            return View(model);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="form"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SearchFeedback(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                if (form["userName"] != null && !string.IsNullOrEmpty(form["userName"]))
                {
                    string userName = form["userName"].ToString();
                    sbCon.AppendFormat(" and UserName like '%{0}%'", userName.Replace("'", ""));
                }

                //表单的实体             
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and FeedbackTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                //分页
                RQPagerDto pager = new RQPagerDto();
                pager.PageSize = rows;
                pager.PageIndex = page;
                pager.Where = sbCon.ToString();
                pager.OrderBy = OrderBy;
                ModelByCount<SystemFeedback> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemFeedback>>(string.Format("{0}SysSetting/GetFeedbackList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 添加或者编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addEditFeedback()
        {
            JsonResult result = null;
            try
            {

            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        #endregion

        #region 请求日志
        public ActionResult SysRequestRecordIndex()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    requestNo = string.Empty,
                    requestMoney = string.Empty,
                    requestType = 0,
                    requestOperStatus = 0,
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now
                },
                urls = new
                {
                    search = "/SysSetting/SearchRequestRecord",
                    hand = "/SysSetting/HandUpdate"
                }
            };
            return View(model);
        }
        /// <summary>
        /// 查询请求日志
        /// </summary>
        /// <param name="form"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SearchRequestRecord(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                //请求流水号
                if (form["requestNo"] != null && !string.IsNullOrEmpty(form["requestNo"]))
                {
                    string requestNo = form["requestNo"].ToString();
                    sbCon.AppendFormat(" and Id like '%{0}%'", requestNo.Replace("'", ""));
                }
                //请求金额
                if (form["requestMoney"] != null && !string.IsNullOrEmpty(form["requestMoney"]))
                {
                    string requestMoney = form["requestMoney"].ToString();
                    sbCon.AppendFormat(" and RequestMoney ={0} ", requestMoney.Replace("'", ""));
                }
                //请求类型
                if (form["requestType"] != null && !string.IsNullOrEmpty(form["requestType"])
                    && form["requestType"].ToString() != "0"
                    )
                {
                    string requestType = form["requestType"].ToString();
                    sbCon.AppendFormat(" and RequestType ={0} ", requestType.Replace("'", ""));
                }
                //请求处理状态
                if (form["operStatus"] != null && !string.IsNullOrEmpty(form["operStatus"])
                    && form["operStatus"].ToString() != "0"
                    )
                {
                    string requestOperStatus = form["operStatus"].ToString();
                    sbCon.AppendFormat(" and RequestOperStatus ={0} ", requestOperStatus.Replace("'", ""));
                }

                //表单的实体             
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and RequestDate between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                //分页
                RQPagerDto pager = new RQPagerDto();
                pager.PageSize = rows;
                pager.PageIndex = page;
                pager.Where = sbCon.ToString();
                pager.OrderBy = OrderBy;
                ModelByCount<SystemRequestRecord> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemRequestRecord>>(string.Format("{0}SysSetting/GetRequestRecordList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        public JsonResult HandUpdate(SystemRequestRecord model)
        {
            JsonResult result = null;
            try
            {
                RQHandExecDto handExecDto = new RQHandExecDto();
                handExecDto.RequestRecord = model;
                handExecDto.IP = this.RequestIP;
                handExecDto.OperatorUserId = this.UserInfo.ID;
                handExecDto.OperatorUserName = this.UserInfo.RealName;
                handExecDto.OperatorContent = "手动同步数据：";
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}SysSetting/HandExec", this.WebApiUrl), handExecDto);
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 账户查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPlatformAcountQuery()
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>>>(string.Format("{0}SysSetting/GetPlatformAcountQuery", this.WebApiUrl), this.WebApiTimeOut);
                BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response> resultModel = resultDto.Tag;
                MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response Res = resultModel.Tag;
                if (Res == null) resultDto.ErrorCode = -1;
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg, data = Res }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }


    }
}
