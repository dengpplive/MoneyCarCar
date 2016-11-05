using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Web.Security;
using MoneyCarCar.Models;
using MoneyCarCar.Commons;
using MoneyCarCar.AdminWebsite.Controllers.CommHelper;
using MoneyCarCar.Website.Controllers.CommHelper;
namespace MoneyCarCar.AdminWebsite.Controllers
{
    public class BaseController : Controller
    {
        public SystemUsers UserInfo { get; set; }
        public string RequestIP
        {
            get
            {
                return AppConfigHelper.IP;
            }
        }
        /// <summary>
        /// 请求的WebApi的基地址 如http://127.0.0.1/
        /// </summary>
        public string WebApiUrl { get { return AppConfigHelper.WebApiUrl; } }
        /// <summary>
        /// 请求WebApi的超时时间
        /// </summary>
        public int WebApiTimeOut
        {
            get
            {
                return AppConfigHelper.WebApiTimeOut;
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登陆验证
            if (this.HttpContext.Request.IsAuthenticated)
            {
                //获取授权cookie
                HttpCookie authCookie = this.HttpContext.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
                //根据Cookie得到登录用户票据
                FormsAuthenticationTicket ticke = FormsAuthentication.Decrypt(authCookie.Value);
                this.UserInfo = JsonConvert.DeserializeObject<SystemUsers>(ticke.UserData);
                if (this.UserInfo == null)
                {
                    this.HttpContext.Response.Redirect("/Home/Index");
                }
            }
            else
            {
                this.HttpContext.Response.Redirect("/Home/Index");
            }
            base.OnAuthorization(filterContext);
        }
        public ActionResult Error()
        {
            return View();
        }
        public string GetJson(object data)
        {
            return data.ToJson();
        }
        /// <summary>
        /// 处理JSON中的时间格式
        /// </summary>       
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new NewtonJsonResult()
            {
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                Data = data,
                JsonRequestBehavior = behavior
            };
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                string errorMsg = filterContext.Exception.Message;
                filterContext.Result = Json(new { ErrorCode = filterContext.Exception.Source, ErrorMsg = errorMsg }, filterContext.Exception.Message);

            }
            else
            {
                filterContext.ExceptionHandled = true;
                var errorView = View("Error", (object)filterContext.Exception.Message);
                filterContext.Result = errorView;
            }
            base.OnException(filterContext);
        }

    }
    public class NewtonJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("不允许的请求");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "text/html";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                //设置输出的时间格式
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                string strData = JsonConvert.SerializeObject(this.Data, Newtonsoft.Json.Formatting.Indented, timeFormat);
                response.Write(strData);
            }
        }
    }
}
