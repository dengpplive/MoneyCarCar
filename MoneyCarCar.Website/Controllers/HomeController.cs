using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.Propertys;
using System.Data;

namespace MoneyCarCar.Website.Controllers
{
    public class HomeController : Controller
    {
        HttpHelper http = HttpHelper.CreatHelper();
        public HomeController()
        {
            ViewBag.OnIndex = 1;
            ViewBag.ApiUrl = ApplicationPropertys.WEBAPI_URL;
            ViewBag.AdminSiteUrl = ApplicationPropertys.ADMINWebSITE_URL;
        }
        //验证码
        public ActionResult ValidateCode()
        {
            string code = VerificationCode.GetCheckCode(5);
            Session["ValidateCode"] = code.ToUpper();
            byte[] bytes = VerificationCode.CreateImage(code);
            return File(bytes, @"image/png");
        }
        //首页
        public ActionResult Index()
        {
            DataTable[] datas = http.DoGetObject<DataTable[]>(ApplicationPropertys.WEBAPI_URL + "/WebSiteService/GetIndexDatas");
            if (datas == null)
            {
                return Content("数据异常");
            }
            List<SystemNews> news = datas[0].CreateDataReader().ReaderToList<SystemNews>();
            List<SystemHelp> helpes = datas[1].CreateDataReader().ReaderToList<SystemHelp>();
            List<SystemClaims> claims = datas[2].CreateDataReader().ReaderToList<SystemClaims>();
            List<SystemNotice> notices = datas[3].CreateDataReader().ReaderToList<SystemNotice>();
            ViewBag.News = news;
            ViewBag.Helpers = helpes;
            ViewBag.Claims = claims;
            ViewBag.Notices = notices;
            return View();
        }
        //注册页面
        public ActionResult Reg()
        {
            //ViewBag.OnIndex = 1;
            return View();
        }
        //注册-手机验证
        public ActionResult PhoneValidate()
        {
            //ViewBag.OnIndex = 1;
            if (Session["RegUserInfo"] != null)
            {
                UserReg reg = Session["RegUserInfo"] as UserReg;
                ViewBag.UserName = reg.UserName;
                ViewBag.PhoneNumber = reg.UserPhone;
                return View();
            }
            else
            {
                return RedirectToAction("Reg");
            }
        }
        //登录页面
        public ActionResult Login()
        {
            //ViewBag.OnIndex = 1;
            return View();
        }
        //安全页面
        public ActionResult Safe()
        {
            //ViewBag.OnIndex = 1;
            return View();
        }
        //投资列表页面
        public ActionResult Investment(int id = 1)
        {
            ViewBag.OnIndex = 2;
            RQPagerDto pag = new RQPagerDto();
            pag.PageSize = 4;
            pag.PageIndex = id;
            pag.OrderBy = " PublishTime DESC";
            ModelByCount<SystemClaims> model = http.DoPostObject<ModelByCount<SystemClaims>>(ApplicationPropertys.WEBAPI_URL + "/Claims/GetList/", pag);
            ViewBag.Datas = model;
            return View();
        }
        //投资详情
        public ActionResult InvestmentDetail(int id)
        {
            ViewBag.OnIndex = 2;
            SystemClaims model = http.DoGetObject<SystemClaims>(ApplicationPropertys.WEBAPI_URL + "/Claims/GetClaims/" + id);
            if (null == model)
            {
                return RedirectToAction("Investment", "Home");
            }
            ViewBag.Model = model;
            ViewBag.Already = model.AlreadyAmount * 100 / model.LoanAmount;
            ViewBag.IsFull = model.AlreadyAmount >= model.LoanAmount;
            ViewBag.CanBuy = (int)((model.LoanAmount - model.AlreadyAmount) / model.SingleAmount);
            ViewBag.AllCanBuy = (int)(model.LoanAmount / model.SingleAmount);
            ViewBag.HaveMoney = 0;
            ViewBag.HaveVirtualMoney = 0.00;
            ViewBag.Days = (model.EarningsStartTime.ToDateTime().AddMonths(model.LoanPeriod) - model.EarningsStartTime.ToDateTime()).TotalDays;
            ViewBag.IsLogin = false;
            if (null != Session["UserInfo"])
            {
                SystemUsers userInfo = (SystemUsers)Session["UserInfo"];
                ViewBag.IsLogin = true;
                BaseResultDto<decimal> result = http.DoGetObject<BaseResultDto<decimal>>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserMoney/" + userInfo.ID);
                if (null != result)
                {
                    userInfo.Balance = result.Tag;
                }
                ViewBag.HaveMoney = userInfo.Balance;
                result = http.DoGetObject<BaseResultDto<decimal>>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserVirtualMoney/" + userInfo.ID);
                if (null != result)
                {
                    ViewBag.HaveVirtualMoney = result.Tag;
                }
            }
            return View();
        }
        //订单确认页面
        public ActionResult TransactionAct(RQSubmitOrder id)
        {
            ViewBag.OnIndex = 2;
            SystemClaims model = http.DoGetObject<SystemClaims>(ApplicationPropertys.WEBAPI_URL + "/Claims/GetClaims/" + id.InvestorsID);
            if (null == model)
            {
                return RedirectToAction("Investment", "Home");
            }
            ViewBag.SystemClaims = model;
            ViewBag.BuyCount = id.BuyCount;
            ViewBag.IsUserBounty = id.IsUserBounty;
            ViewBag.BountyCount = id.BountyCount;

            ViewBag.Already = model.AlreadyAmount * 100 / model.LoanAmount;//完成百分比
            ViewBag.IsFull = model.AlreadyAmount >= model.LoanAmount;//是否满标
            ViewBag.CanBuy = (int)((model.LoanAmount - model.AlreadyAmount) / model.SingleAmount);//剩余份数
            ViewBag.AllCanBuy = (int)(model.LoanAmount / model.SingleAmount);//所有份数
            ViewBag.HaveMoney = 0;
            ViewBag.HaveVirtualMoney = 0.00;
            //借款时间-天
            ViewBag.Days = (model.EarningsStartTime.ToDateTime().AddMonths(model.LoanPeriod) - model.EarningsStartTime.ToDateTime()).TotalDays;
            ViewBag.IsLogin = false;//是否登录
            if (null != Session["UserInfo"])
            {
                SystemUsers userInfo = (SystemUsers)Session["UserInfo"];
                ViewBag.IsLogin = true;
                BaseResultDto<decimal> result = http.DoGetObject<BaseResultDto<decimal>>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserMoney/" + userInfo.ID);
                if (null != result)
                {
                    userInfo.Balance = result.Tag;
                }
                ViewBag.HaveMoney = userInfo.Balance;
                result = http.DoGetObject<BaseResultDto<decimal>>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserVirtualMoney/" + userInfo.ID);
                if (null != result)
                {
                    ViewBag.HaveVirtualMoney = result.Tag;
                }

            }
            return View();
        }
        //关于我们
        public ActionResult About()
        {
            ViewBag.OnIndex = 3;
            return View();
        }
        //新手指引
        public ActionResult NoviceGuide()
        {
            ViewBag.OnIndex = 4;
            return View();
        }
        //新手指引
        public ActionResult Help(string key = "", int pageIndex = 1, int pageSize = 20)
        {
            ViewBag.OnIndex = 5;
            return View();
        }
    }
}
