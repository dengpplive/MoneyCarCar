using MoneyCarCar.Commons;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.ResParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    /// <summary>
    /// 管理员操作
    /// </summary>
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            ViewBag.curUser = this.UserInfo;
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdatePwd()
        {
            return null;
        }

        //退出
        public ActionResult Logont()
        {
            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                authCookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(authCookie);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 首页显示的统计数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowStatistiIndex()
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<StatisticalDto> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<StatisticalDto>>(string.Format("{0}SysSetting/GetStatistical", this.WebApiUrl));
                result = Json(new { data = resultDto.Tag }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

    }
}
