using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.ResParam;
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
    /// <summary>
    /// 债权
    /// </summary>
    public class ClaimsController : BaseController
    {
        //
        // GET: /Claims/
        public ActionResult Index()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now,
                    title = string.Empty,
                    isApproved = string.Empty,
                    borrower = string.Empty
                },
                urls = new
                {
                    search = "/Claims/SearchClaims",
                    edit = "/Claims/AddUpdateClaims",
                    del = "/Claims/DelClaims"
                }
            };
            return View(model);
        }
        public ActionResult ShowAddUpdate(int ID)
        {
            SystemClaims systemClaims = new SystemClaims();
            if (ID > 0)
            {

                systemClaims = HttpHelper.CreatHelper().DoGetObject<SystemClaims>(string.Format("{0}Claims/GetClaims?Id={1}", this.WebApiUrl, ID));
            }
            return View(systemClaims);
        }
        /// <summary>
        /// 查询债权
        /// </summary>       
        public JsonResult SearchClaims(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                //表单的实体 

                if (form["title"] != null && !string.IsNullOrEmpty(form["title"]))
                {
                    string title = form["title"].ToString();
                    sbCon.AppendFormat(" and Title like '%{0}%'", title.Replace("'", ""));
                }
                //借款人姓名
                if (form["Borrower"] != null && !string.IsNullOrEmpty(form["Borrower"]))
                {
                    string Borrower = form["Borrower"].ToString();
                    sbCon.AppendFormat(" and Borrower like '%{0}%' ", Borrower.Replace("'", ""));
                }
                //债权审核状态
                if (form["isApproved"] != null && !string.IsNullOrEmpty(form["isApproved"]))
                {
                    string isApproved = form["isApproved"].ToString();
                    sbCon.AppendFormat(" and IsApproved ={0}", isApproved.Replace("'", ""));
                }
                //债权发布时间
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and PublishTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
                }
                //排序的东西
                string OrderBy = string.Empty;
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
                ModelByCount<SystemClaims> pagerData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemClaims>>(string.Format("{0}Claims/GetList", this.WebApiUrl), pager);
                result = Json(new { total = pagerData.AllCount, rows = pagerData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// 删除债权信息
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public JsonResult DelClaims(int ID)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<string>>(string.Format("{0}Claims/Delete?Id={1}", this.WebApiUrl, ID));
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public JsonResult DelImage(int Id, string imgUrl)
        {
            JsonResult result = null;
            try
            {
                if (Id > 0)
                {
                    RQUpdate<SystemClaims> model = new RQUpdate<SystemClaims>();
                    SystemClaims systemClaims = new SystemClaims();
                    systemClaims.ID = Id;
                    systemClaims.TitleImagePath = "";
                    model.Tag = systemClaims;
                    //更新审核字段数据
                    model.UpdateFileds.AddRange(new string[]{
                            "TitleImagePath"
                        });
                    bool rs = HttpHelper.CreatHelper().DoPostObject<bool>(string.Format("{0}Claims/UpdatePartal", this.WebApiUrl), model);
                }
                try
                {
                    DataFornat dataformat = new DataFornat();
                    string strPath = dataformat.GetResovePath(imgUrl);
                    if (System.IO.File.Exists(strPath))
                    {
                        System.IO.File.Delete(strPath);
                    }
                    string strThumbnailFileName = System.IO.Path.GetFullPath(strPath) + "\\64_" + System.IO.Path.GetFileName(strPath);
                    if (System.IO.File.Exists(strThumbnailFileName))
                    {
                        System.IO.File.Delete(strThumbnailFileName);
                    }
                }
                catch (Exception)
                {
                }
                result = Json(new { status = 1, message = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;

        }

        /// <summary>
        /// 展开收缩 显示债权明细 部分
        /// </summary>
        /// <param name="systemClaims"></param>
        /// <returns></returns>
        public ActionResult ShowClaimsDetails(int ID)
        {
            ClaimsDetailsDto claimsDetailsDto = new ClaimsDetailsDto();
            if (ID > 0)
            {
                BaseResultDto<ClaimsDetailsDto> baseResultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<ClaimsDetailsDto>>(string.Format("{0}Claims/GetSystemClaimsDetails?ClaimsId={1}", this.WebApiUrl, ID));
                claimsDetailsDto = baseResultDto.Tag;
            }
            return PartialView("ClaimsDetail", claimsDetailsDto);
        }
        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult ShowPageClaimsDetails(int ID)
        {
            ClaimsDetailsDto claimsDetailsDto = new ClaimsDetailsDto();
            if (ID > 0)
            {
                BaseResultDto<ClaimsDetailsDto> baseResultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<ClaimsDetailsDto>>(string.Format("{0}Claims/GetSystemClaimsDetails?ClaimsId={1}", this.WebApiUrl, ID));
                claimsDetailsDto = baseResultDto.Tag;
            }
            return View(claimsDetailsDto);
        }
        /// <summary>
        /// 信息统计
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoStatistics()
        {
            return View();
        }
    }
}
