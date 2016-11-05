using MoneyCarCar.AdminWebsite.Controllers.CommHelper;
using MoneyCarCar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MoneyCarCar.DAL;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Models.DtoModels;
using Newtonsoft.Json.Converters;

namespace MoneyCarCar.Website.Controllers.Home
{
    /// <summary>
    /// 首页和登录
    /// </summary>
    public class HomeController : Controller
    {
        SystemUsersOper userOp = new SystemUsersOper();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated && !Request.IsAjaxRequest())
                FormsAuthentication.SignOut();
            return View();
        }
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["Vcode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        public JsonResult Login(FormCollection from)
        {
            JsonResult result = null;
            try
            {
                string strAccount = from["account"].ToString();
                string strPwd = from["pwd"].ToString();
                string code = from["vcode"].ToString();
                string sCode = Session["Vcode"] != null ? Session["Vcode"].ToString() : null;
                string Message = string.Empty;
                int status = 0;
                if (sCode == null || string.Compare(sCode, code, true) != 0)
                {
                    if (sCode == null)
                    {
                        Message = "验证码已过期,请重新获取！";
                    }
                    else
                    {
                        //验证码错误
                        Message = "验证码输入错误";
                    }
                    status = -1;
                }
                else
                {
                    //换成WebAPI 调用登录 返回登录信息                    
                    UserLogin userLogin = new UserLogin();
                    userLogin.UserNameOrPhone = strAccount;
                    userLogin.UserPassword = strPwd.GetMd5Code();
                    if (strAccount.ToUpper() == "ADMIN")
                    {
                        userLogin.UserType = 2;
                    }
                    else
                    {
                        userLogin.UserType = 3;
                    }
                    userLogin.UserIP = AppConfigHelper.IP;
                    BaseResultDto<SystemUsers> userDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<SystemUsers>>(string.Format("{0}User/UserLogin", AppConfigHelper.WebApiUrl), userLogin);
                    //管理员
                    SystemUsers user = userDto.Tag as SystemUsers;
                    if (user != null)
                    {
                        //登录成功                                                                  
                        //序列化用户信息
                        string UserData = JsonConvert.SerializeObject(user);
                        //保存序列化的用户信息票据
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddDays(1), false, UserData);
                        //将票据加密并存到cookie
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        //输出到浏览器
                        this.HttpContext.Response.Cookies.Add(cookie);
                        //FormsAuthentication.SetAuthCookie(user.UserName, false);
                        // bool IsAuthenticated = this.HttpContext.Request.IsAuthenticated;
                        status = 1;
                    }
                    else
                    {
                        Message = userDto.ErrorMsg;
                        if (Message.Contains("账号不存在"))
                        {
                            status = -3;
                        }
                        else
                        {
                            status = -2;
                        }
                    }
                }
                //返回数据
                result = Json(new { status = status, message = Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = 0, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

    }
}
