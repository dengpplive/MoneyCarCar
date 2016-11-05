using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    /// <summary>
    /// 短信管理
    /// </summary>
    public class SmsController : BaseController
    {
        #region 短信模板
        public ActionResult SmsTemplteIndex()
        {
            List<SystemDictionary> result = new List<SystemDictionary>();
            try
            {
                BaseResultDto<List<SystemDictionary>> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<List<SystemDictionary>>>(string.Format("{0}SysSetting/GetSmsTemplateOptions", this.WebApiUrl));
                if (resultDto.Tag.Count == 0) resultDto.Tag = new List<SystemDictionary>();
                ViewBag.data = resultDto.Tag;
            }
            catch (Exception)
            {
            }
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    templateName = string.Empty
                },
                urls = new
                {
                    search = "/Sms/SearchSmsTemplte",
                    addEdit = "/Sms/AddEditSmsTemplte"
                }
            };
            return View(model);
        }
        public ActionResult SearchSmsTemplte(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");

                if (form["templateName"] != null && form["templateName"].ToString() != "")
                {
                    string templateName = form["templateName"].ToString();
                    sbCon.AppendFormat(" and TemplateName like '%{0}%' ", templateName);
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
                ModelByCount<SystemSmsTemplate> userData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemSmsTemplate>>(string.Format("{0}Sms/GetTemplateList", this.WebApiUrl), pager);
                result = Json(new { total = userData.AllCount, rows = userData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        [HttpPost]
        public JsonResult AddEditSmsTemplte(SystemSmsTemplate tempate)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                if (tempate.Id > 0)
                {
                    //编辑
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}Sms/Update", this.WebApiUrl), tempate);
                }
                else
                {
                    //添加
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}Sms/Add", this.WebApiUrl), tempate);
                }
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        #endregion

        #region 发送短信记录


        public ActionResult SmsRecordIndex()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    sendMobile = string.Empty,
                    acceptMobile = string.Empty,
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now,
                },
                urls = new
                {
                    search = "/Sms/SearchSmsRecord"
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
        public ActionResult SearchSmsRecord(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");

                if (form["sendMobile"] != null && form["sendMobile"].ToString() != "")
                {
                    string sendMobile = form["sendMobile"].ToString();
                    sbCon.AppendFormat(" and SendMobile = '{0}' ", sendMobile);
                }
                if (form["acceptMobile"] != null && form["acceptMobile"].ToString() != "")
                {
                    string acceptMobile = form["acceptMobile"].ToString();
                    sbCon.AppendFormat(" and AcceptMobile = '{0}' ", acceptMobile);
                }
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and AddTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                ModelByCount<SystemSmsRecord> userData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemSmsRecord>>(string.Format("{0}Sms/GetRecordList", this.WebApiUrl), pager);
                result = Json(new { total = userData.AllCount, rows = userData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        #endregion
    }
}
