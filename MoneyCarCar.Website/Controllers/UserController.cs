using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.Statisticals.Return;
using MoneyCarCar.Models.Statisticals.Parameter;
using System.Data;

namespace MoneyCarCar.Website.Controllers
{
    public class UserController : Controller
    {
        HttpHelper http = HttpHelper.CreatHelper();
        public UserController()
        {
            ViewBag.OnIndex = 1;
            ViewBag.MenuIndex = 1;
            ViewBag.ApiUrl = ApplicationPropertys.WEBAPI_URL;
            ViewBag.AdminSiteUrl = ApplicationPropertys.ADMINWebSITE_URL;
            ViewBag.ControllerName = "用户管理";
            ViewBag.PageName = "账户概况";
        }
        //1用户中心首页
        public ActionResult Index()
        {
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UserName = user.UserName;
            ViewBag.UserBalance = user.Balance;
            ViewBag.UserFreeze = user.Freeze;
            ViewBag.UserIDNumberIsAuthenticate = user.IDNumberIsAuthenticate;
            ViewBag.UserCellPhone = "*******" + user.CellPhone.Substring(user.CellPhone.Length - 4, 4);
            ViewBag.UserCellPahoneIsAuthenticate = user.CellPahoneIsAuthenticate;
            if (user.IDNumberIsAuthenticate)
            {
                ViewBag.UserIDNumber = "***************" + user.IDNumber.Substring(user.IDNumber.Length - 3, 3);
                ViewBag.UserRealName = user.RealName.Substring(0, 1) + "**";
            }
            return View();
        }
        //2个人信息
        public ActionResult UserInfo()
        {
            ViewBag.MenuIndex = 2;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.PageName = "个人信息";
            ViewBag.User = user;
            return View();
        }
        //3用户银行信息
        public ActionResult UserBank()
        {
            ViewBag.MenuIndex = 3;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            SystemBankCard bank = http.DoGetObject<SystemBankCard>(ApplicationPropertys.WEBAPI_URL + "/User/GetBindBank/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.PageName = "银行管理";
            ViewBag.Bank = bank;
            return View();
        }
        //4系统消息
        public ActionResult UserMessage()
        {
            ViewBag.MenuIndex = 4;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.PageName = "系统消息";
            return View();
        }
        //5登录日志
        public ActionResult UserRecord(int id = 1)
        {
            ViewBag.MenuIndex = 5;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "登录日志";

            RQPagerDto pag = new RQPagerDto();
            pag.PageSize = 15;
            pag.PageIndex = id;
            pag.QueryFileds = " [Id],[OperatorUserId],[OperatorUserName],[OperatorType],[BusinessType],[OperatorTime],[OperatorContent],[OperatorIP] ";
            pag.Where = " BusinessType = '登陆' AND OperatorUserId = " + user.ID + " AND DATEDIFF(day,OperatorTime,getdate()) <= 10 ";
            pag.OrderBy = " ID DESC";
            ModelByCount<SystemLog> model = http.DoPostObject<ModelByCount<SystemLog>>(ApplicationPropertys.WEBAPI_URL + "/SystemLog/GetList/", pag);
            ViewBag.Datas = model;
            return View();
        }
        //6投资统计(收益统计)
        public ActionResult Totalrevenue()
        {
            ViewBag.MenuIndex = 6;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "投资统计";

            Earnings_Parameter par = new Earnings_Parameter();
            par.UserID = user.ID;
            par.SearchWay = 1;
            par.Datas = 0;

            Earnings_Return result = http.DoPostObject<Earnings_Return>(ApplicationPropertys.WEBAPI_URL + "/User/Totalrevenue", par);
            ViewBag.Datas = result;

            return View();
        }
        //6投资统计(收益明细)
        public ActionResult TotalrevenueDetails(int id = 1)
        {
            ViewBag.MenuIndex = 6;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "投资统计";

            Earnings_Parameter par = new Earnings_Parameter();
            par.UserID = user.ID;
            par.SearchWay = 1;
            par.Datas = 0;
            //查询统计总数
            Earnings_Return result = http.DoPostObject<Earnings_Return>(ApplicationPropertys.WEBAPI_URL + "/User/Totalrevenue", par);
            TransactionRecord_Parameter pag = new TransactionRecord_Parameter();
            pag.PageSize = 15;
            pag.PageIndex = id;
            pag.UserId = user.ID;
            //交易明细
            ModelByCount<TransactionRecord_Return> model = http.DoPostObject<ModelByCount<TransactionRecord_Return>>(ApplicationPropertys.WEBAPI_URL + "/User/TransactionRecord/", pag);

            ViewBag.Result = result;
            ViewBag.Record = model;
            return View();
        }
        //6投资统计(投资统计)
        public ActionResult TotalInvestment()
        {
            ViewBag.MenuIndex = 6;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "投资统计";

            Earnings_Parameter par = new Earnings_Parameter();
            par.UserID = user.ID;
            par.SearchWay = 1;
            par.Datas = 0;
            //查询统计总数
            Earnings_Return result = http.DoPostObject<Earnings_Return>(ApplicationPropertys.WEBAPI_URL + "/User/Totalrevenue", par);
            //已购买的债权
            DataTable[] model = http.DoGetObject<DataTable[]>(ApplicationPropertys.WEBAPI_URL + "/Claims/GetInvestment_Ing/" + user.ID);

            ViewBag.Result = result;
            ViewBag.Record = model;
            return View();
        }
        //7已投产品
        public ActionResult BuiedClaims()
        {
            ViewBag.MenuIndex = 7;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "已投产品";
            //已购买的债权
            DataTable model = http.DoGetObject<DataTable>(ApplicationPropertys.WEBAPI_URL + "/Claims/GetInvestment_All/" + user.ID);

            ViewBag.Record = model;
            return View();
        }
        //8过期产品
        public ActionResult ExpireClaims()
        {
            ViewBag.MenuIndex = 8;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "过期产品";
            //已过期的债权
            DataTable model = http.DoGetObject<DataTable>(ApplicationPropertys.WEBAPI_URL + "/Claims/GetInvestment_Ed/" + user.ID);

            ViewBag.Record = model;
            return View();
        }
        //11交易记录
        public ActionResult TransactionRecord(int id = 1)
        {
            ViewBag.MenuIndex = 11;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "交易记录";

            TransactionRecord_Parameter pag = new TransactionRecord_Parameter();
            pag.PageSize = 15;
            pag.PageIndex = id;
            pag.UserId = user.ID;
            ModelByCount<TransactionRecord_Return> model = http.DoPostObject<ModelByCount<TransactionRecord_Return>>(ApplicationPropertys.WEBAPI_URL + "/User/TransactionRecord/", pag);
            ViewBag.Datas = model;
            return View();
        }
        //14提现记录
        public ActionResult WithdrawRecord(int id = 1)
        {
            ViewBag.MenuIndex = 14;
            SystemUsers user = Session["UserInfo"] as SystemUsers;
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            user = http.DoGetObject<SystemUsers>(ApplicationPropertys.WEBAPI_URL + "/User/GetUserInfo/" + user.ID);
            if (null == user)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["UserInfo"] = user;
            ViewBag.PageName = "提现记录";

            TransactionRecord_Parameter pag = new TransactionRecord_Parameter();
            pag.PageSize = 15;
            pag.PageIndex = id;
            pag.UserId = user.ID;
            ModelByCount<SystemRequestRecord> model = http.DoPostObject<ModelByCount<SystemRequestRecord>>(ApplicationPropertys.WEBAPI_URL + "/User/GetWithdrawRecord/", pag);
            ViewBag.Datas = model;
            return View();
        }
        //15申请借贷
        public ActionResult ApplyBorrower()
        {
            ViewBag.MenuIndex = 15;
            return View();
        }
        //16借贷记录
        public ActionResult ApplyRecord()
        {
            ViewBag.MenuIndex = 16;
            return View();
        }

    }

}