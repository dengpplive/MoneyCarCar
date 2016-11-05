using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MoneyCarCar.Models;
using AutoMapper;
using System.Text;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.ModelDto.RQParam;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    /// <summary>
    /// 公告
    /// </summary>
    public class NoticeController : BaseController
    {
        //
        // GET: /Notice/

        public ActionResult NoticeIndex()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now,
                    title = string.Empty,
                    status = string.Empty
                },
                urls = new
                {
                    search = "/Notice/SearchNotice",
                    edit = "/Notice/AddUpdateNotice",
                    del = "/Notice/DelNotice",
                    patchDel = "/Notice/PatchDelNotice"
                }
            };
            return View(model);
        }
        /// <summary>
        /// 查询公告
        /// </summary>       
        public JsonResult SearchNotice(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                if (form["title"] != null && !string.IsNullOrEmpty(form["title"]))
                {
                    string title = form["title"].ToString();
                    sbCon.AppendFormat(" and NoticeTitle like '%{0}%'", title.Replace("'", ""));
                }
                if (form["status"] != null && !string.IsNullOrEmpty(form["status"]))
                {
                    string status = form["status"].ToString();
                    sbCon.AppendFormat(" and NoticeStatus = '{0}'", status.Replace("'", ""));
                }
                //表单的实体             
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and NoticeAddDate between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                ModelByCount<SystemNotice> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemNotice>>(string.Format("{0}Notice/GetList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        [HttpPost]
        public JsonResult AddUpdateNotice(SystemNotice notice)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                //添加或者修改                
                if (notice.Id > 0)
                {
                    //修改
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}Notice/Update", this.WebApiUrl), notice);
                }
                else
                {
                    notice.NoticeAddDate = System.DateTime.Now;
                    notice.NoticeRealseAccount = this.UserInfo.UserName;
                    //添加
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}Notice/Add", this.WebApiUrl), notice);
                }
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public JsonResult DelNotice(SystemNotice notice)
        {
            JsonResult result = null;
            try
            {
                //删除公告             
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<string>>(string.Format("{0}Notice/Delete?Id={1}", this.WebApiUrl, notice.Id));
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        public JsonResult PatchDelNotice()
        {
            JsonResult result = null;
            try
            {
                RQIdModel<int> model = new RQIdModel<int>();
                //......
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}Notice/DeleteAll", this.WebApiUrl), model);
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
