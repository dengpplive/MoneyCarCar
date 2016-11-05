using MoneyCarCar.AdminWebsite.Controllers.CommHelper;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    /// <summary>
    /// 新闻动态
    /// </summary>
    public class NewsController : BaseController
    {
        //
        // GET: /News/

        public ActionResult NewsIndex()
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
                    search = "/News/SearchNews",
                    edit = "/News/AddUpdateNews",
                    del = "/News/DelNew",
                    patchDel = "/News/PatchDelNews"
                }
            };
            return View(model);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult AddNews(int Id)
        {
            SystemNews model = new SystemNews();
            if (Id > 0)
            {
                BaseResultDto<SystemNews> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<SystemNews>>(string.Format("{0}News/GetNews?Id={1}", this.WebApiUrl, Id));
                model = resultDto.Tag;
            }
            return View(model);
        }

        /// <summary>
        /// 查询新闻
        /// </summary>       
        public JsonResult SearchNews(FormCollection form, int page = 1, int rows = 2)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                if (form["title"] != null && !string.IsNullOrEmpty(form["title"]))
                {
                    string title = form["title"].ToString();
                    sbCon.AppendFormat(" and NewsTitle like '%{0}%'", title.Replace("'", ""));
                }
                if (form["status"] != null && !string.IsNullOrEmpty(form["status"]))
                {
                    string status = form["status"].ToString();
                    sbCon.AppendFormat(" and NewsStatus = '{0}'", status.Replace("'", ""));
                }
                //表单的实体             
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and NewsRealseTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                ModelByCount<SystemNews> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemNews>>(string.Format("{0}News/GetList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// 添加或者修改新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddUpdateNews(SystemNews news)
        {
            //FormCollection form
            DataFornat dataformat = new DataFornat();

            JsonResult result = null;
            try
            {
                news.NewsContent = HttpUtility.UrlDecode(news.NewsContent);
                news.NewsContent = dataformat.ReplaceImageSrc(news.NewsContent, AppConfigHelper.MainSiteUrl);
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                //添加或者修改               
                if (news.Id > 0)
                {
                    //修改
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}News/Update", this.WebApiUrl), news);
                }
                else
                {
                    news.NewsRealseTime = System.DateTime.Now;
                    news.UserName = this.UserInfo.UserName;
                    news.UserId = this.UserInfo.ID;
                    //添加    
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}News/Add", this.WebApiUrl), news);
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
        /// 删除新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public JsonResult DelNew(SystemNews news)
        {
            JsonResult result = null;
            try
            {
                //删除新闻
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<string>>(string.Format("{0}News/Delete?Id={1}", this.WebApiUrl, news.Id));
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <returns></returns>
        public JsonResult PatchDelNews()
        {
            JsonResult result = null;
            try
            {
                RQIdModel<int> model = new RQIdModel<int>();
                //添加id
                //
                //model.IdList.Add();
                //删除新闻                
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}News/DeleteAll", this.WebApiUrl), model);
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
