using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.YeePay;
using System.Text;
using MoneyCarCar.Models.SMS;
using MoneyCarCar.Models.Statisticals.Parameter;
using MoneyCarCar.Models.Statisticals.Return;

namespace MoneyCarCar.Website.Controllers
{
    public class SubmitController : BaseController
    {
        HttpHelper http = HttpHelper.CreatHelper();
        //检查用户名是否重复
        public ActionResult CheckAccount(PhoneOrNameCheck model)
        {
            return Content(http.DoPost(ApplicationPropertys.WEBAPI_URL + "/User/CheckAccount/", model));
        }
        //检查手机是否重复
        public ActionResult CheckPhone(PhoneOrNameCheck model)
        {
            return Content(http.DoPost(ApplicationPropertys.WEBAPI_URL + "/User/CheckPhone/", model));
        }
        //注册验证码对比
        public ActionResult RegUser(UserReg model)
        {
            if (!model.Vcode.ToUpper().Equals(Session["ValidateCode"].ToString()))
            {
                return Content("-1");//验证码不对
            }
            Session["ValidateCode"] = null;
            if (!model.UserPwd.Equals(model.UserPwdRe))
            {
                return Content("-2");//密码输入不一样
            }
            model.UserPwd = model.UserPwd.GetMd5Code();
            model.UserPwdRe = model.UserPwdRe.GetMd5Code();
            Session["RegUserInfo"] = model;
            return Content("1");
        }
        //注册数据存入数据库
        public ActionResult RegUserToDatabase(UserReg model)
        {
            if (null == Session["RegUserInfo"])
            {
                return RedirectToAction("Reg");
            }

            if (!model.Vcode.ToUpper().Equals(Session["ValidateCode"].ToString()))
            {
                return Content("-1");//验证码不对
            }
            Session["ValidateCode"] = null;
            if (!model.PhoneVcode.ToUpper().Equals(Session["PhoneValidateCode"].ToString()))
            {
                return Content("-2");//手机验证码错误
            }
            UserReg regInfo = Session["RegUserInfo"] as UserReg;
            string result = http.DoPost(ApplicationPropertys.WEBAPI_URL + "/User/Add/", regInfo);
            if (int.Parse(result) > 0)
            {
                Session["PhoneValidateReg"] = null;
                Session["RegUserInfo"] = null;
            }
            return Content(result);
        }
        //发送手机验证码
        public ActionResult SendPhoneValidate(String userPhone)
        {
            if (!userPhone.Split('-')[0].ToUpper().Equals(Session["ValidateCode"].ToString()))
            {
                return Content("-2");//验证码不对
            }
            Session["ValidateCode"] = null;
            userPhone = userPhone.Split('-')[1];


            UserReg regInfo = Session["RegUserInfo"] as UserReg;
            if (regInfo != null)
            {
                regInfo.UserPhone = regInfo.UserPhone.Equals(userPhone) ? regInfo.UserPhone : userPhone;
            }

            DateTime now = DateTime.Now;
            if (null != Session["PhoneValidateReg"])
            {
                DateTime parTime = (DateTime)Session["PhoneValidateReg"];
                int seconds = (int)(now - parTime).TotalSeconds;
                if (seconds < 59)
                {
                    return Content("" + (60 - seconds));//等待时间发送
                }
            }
            BaseResultDto<string> re = SMS.SendRegisterSMS(userPhone);
            if (re.IsSeccess)
            {
                Session["PhoneValidateCode"] = re.Tag.ToUpper();
                Session["PhoneValidateReg"] = now;
                return Content("60");//发送成功
            }
            else
            {
                Session["PhoneValidateReg"] = null;
                return Content("-1");//发送失败
            }
        }
        //登录
        public ActionResult Login(UserLogin model)
        {
            if (!model.Vcode.ToUpper().Equals(Session["ValidateCode"].ToString()))
            {
                return Content("-1");//验证码不对
            }
            Session["ValidateCode"] = null;
            model.UserIP = Commons.IpHelper.GetUserIp;
            model.UserPassword = model.UserPassword.GetMd5Code();
            BaseResultDto<SystemUsers> result = http.DoPostObject<BaseResultDto<SystemUsers>>(ApplicationPropertys.WEBAPI_URL + "/User/UserLogin/", model);
            if (result.IsSeccess)
            {
                Session["UserInfo"] = (SystemUsers)result.Tag;
                return Content("1");//登录失败(用户名或密码错误)
            }
            else
            {
                return Content(result.ErrorMsg);//登录失败(用户名或密码错误)
            }
        }
        //登出
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                return Content("1");
            }
            catch
            {
                return Content("0");
            }
        }
        //身份证认证
        public ActionResult AuthenticateIDCard(RQIDCardAuthenticate model)
        {
            BaseResultDto<PostBaseYeePayPar> resu = null;
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                user.RealName = model.RealName;
                user.IDNumber = model.IDCard;
                user.UserAddress = model.Address;
                resu = http.DoPostObject<BaseResultDto<PostBaseYeePayPar>>(ApplicationPropertys.WEBAPI_URL + "/User/AuthenticateIDCard/", user);

            }
            else
            {
                resu = new BaseResultDto<PostBaseYeePayPar>();
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }
            return Content(resu.ToJsonString());
        }
        //提交订单信息到数据库
        public ActionResult SubmitOrderToDatabase(RQSubmitOrder model)
        {
            BaseResultDto<PostBaseYeePayPar> resu = null;
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                model.UserID = user.ID;
                //提交
                resu = http.DoPostObject<BaseResultDto<PostBaseYeePayPar>>(ApplicationPropertys.WEBAPI_URL + "/Claims/AddSystemClaimsDetails", model);
            }
            else
            {
                resu = new BaseResultDto<PostBaseYeePayPar>();
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }
            return Content(resu.ToJsonString());
        }
        //提交申请借贷
        [HttpPost]
        public ActionResult ApplyBorrower(SystemBorrowerApply model)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                if (model.LoanAmount > 0)
                {
                    SystemUsers currUser = Session["UserInfo"] as SystemUsers;
                    if (currUser != null)
                    {
                        model.BorrowerID = currUser.ID;
                        model.BorrowerTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        model.BorrowerType = 1;
                        resultDto = http.DoPostObject<BaseResultDto<string>>(ApplicationPropertys.WEBAPI_URL + "/ClaimsApplay/ApplyBorrower/", model);
                    }
                    else
                    {
                        resultDto.ErrorCode = -1;
                        resultDto.ErrorMsg = "请重新登录,账户已过期";
                    }
                }
                else
                {
                    resultDto.ErrorCode = -1;
                    resultDto.ErrorMsg = "申请金额不能小于零";
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
        /// 查询债权申请
        /// </summary>       
        public JsonResult GetApplyRecord(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                StringBuilder sbCon = new StringBuilder(" 1=1 ");
                //表单的实体    

                //借款人姓名
                if (form["borrowerName"] != null && !string.IsNullOrEmpty(form["borrowerName"]))
                {
                    string borrowerName = form["borrowerName"].ToString();
                    sbCon.AppendFormat(" and BorrowerName = '{0}'", borrowerName.Replace("'", ""));
                }
                //申请状态
                if (form["borrowerType"] != null && !string.IsNullOrEmpty(form["borrowerType"]))
                {
                    string borrowerType = form["borrowerType"].ToString();
                    sbCon.AppendFormat(" and BorrowerType = {0} ", borrowerType.Replace("'", ""));
                }
                //申请日期         
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and BorrowerTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                ModelByCount<SystemBorrowerApply> applyData = http.DoPostObject<ModelByCount<SystemBorrowerApply>>(string.Format("{0}/ClaimsApplay/GetList", ApplicationPropertys.WEBAPI_URL), pager);
                result = Json(new { total = applyData.AllCount, rows = applyData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// 绑定银行卡
        /// </summary>
        /// <returns></returns>
        public ActionResult BindBankCard()
        {
            BaseResultDto<PostBaseYeePayPar> resu = null;
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                resu = http.DoGetObject<BaseResultDto<PostBaseYeePayPar>>(ApplicationPropertys.WEBAPI_URL + "/User/BindBankCard/" + user.ID);
            }
            else
            {
                resu = new BaseResultDto<PostBaseYeePayPar>();
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }
            return Content(resu.ToJsonString());
        }
        /// <summary>
        /// 解绑银行卡
        /// </summary>
        /// <returns></returns>
        public ActionResult UnBindBankCard()
        {
            BaseResultDto<PostBaseYeePayPar> resu = null;
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                resu = http.DoGetObject<BaseResultDto<PostBaseYeePayPar>>(ApplicationPropertys.WEBAPI_URL + "/User/UnBindBankRequest/" + user.ID);
            }
            else
            {
                resu = new BaseResultDto<PostBaseYeePayPar>();
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }
            return Content(resu.ToJsonString());
        }
        /// <summary>
        /// 用户充值
        /// </summary>
        /// <returns></returns>
        public ActionResult Rechare(SystemUsers model)
        {
            BaseResultDto<PostBaseYeePayPar> resu = null;
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                if (!user.IDNumberIsAuthenticate)
                {
                    resu = new BaseResultDto<PostBaseYeePayPar>();
                    resu.IsSeccess = false;
                    resu.ErrorMsg = "请先进行实名认证！";
                    resu.ErrorCode = -3; //用户未登录
                    return Content(resu.ToJsonString());
                }
                model.ID = user.ID;
                resu = http.DoPostObject<BaseResultDto<PostBaseYeePayPar>>(ApplicationPropertys.WEBAPI_URL + "/User/Rechare/", model);
            }
            else
            {
                resu = new BaseResultDto<PostBaseYeePayPar>();
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }
            return Content(resu.ToJsonString());
        }

        /// <summary>
        /// 用户提款
        /// </summary>
        /// <returns></returns>
        public ActionResult Withdraw(SystemUsers model)
        {
            BaseResultDto<PostBaseYeePayPar> resu = null;
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                model.ID = user.ID;
                resu = http.DoPostObject<BaseResultDto<PostBaseYeePayPar>>(ApplicationPropertys.WEBAPI_URL + "/User/Withdraw/", model);
            }
            else
            {
                resu = new BaseResultDto<PostBaseYeePayPar>();
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }
            return Content(resu.ToJsonString());
        }

        //投资统计(包含收益明细)
        public ActionResult Totalrevenue(Earnings_Parameter model)
        {
            BaseResultDto<Earnings_Return> resu = new BaseResultDto<Earnings_Return>();
            if (null != Session["UserInfo"])
            {
                SystemUsers user = (SystemUsers)Session["UserInfo"];
                model.UserID = user.ID;
                Earnings_Return result = http.DoPostObject<Earnings_Return>(ApplicationPropertys.WEBAPI_URL + "/User/Totalrevenue/", model);
                resu.IsSeccess = true;
                resu.Tag = result;
            }
            else
            {
                resu.IsSeccess = false;
                resu.ErrorMsg = "用户未登录或登录超时";
                resu.ErrorCode = -2; //用户未登录
            }

            return Content(resu.ToJsonString());
        }
    }
}
