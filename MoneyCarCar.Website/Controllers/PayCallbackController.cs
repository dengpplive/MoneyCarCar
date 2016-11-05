using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Models.YeePay;
using MoneyCarCar.Models.YeePay.YeePayEnum;
using MoneyCarCar.Website.Controllers.CommHelper;

namespace MoneyCarCar.Website.Controllers
{
    public class PayCallbackController : Controller
    {
        YeePayConfig yeePayConfig = new YeePayConfig();
        DataFornat dataFornat = new DataFornat();
        public PayCallbackController()
        {
            ViewBag.ResultStatu = false;
            ViewBag.Message = "";
        }

        /// <summary>
        /// 注册回调
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RegCallBack(YeePayCallBack id)
        {
            MoneyCarCar.Models.YeePay.response _response = id.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();

            if (_response.code.Equals("1") && _response.service.Equals(EnumServiceType.toRegister.ToEnumDesc()))
            {
                ViewBag.ResultStatu = true;
                ViewBag.Message = "恭喜你，身份验证成功。";
            }
            else
            {
                ViewBag.Message = "对不起，身份验证失败。";
            }
            return View("Index");
        }

        /// <summary>
        /// 投标成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TenderBack(YeePayCallBack id)
        {
            MoneyCarCar.Models.YeePay.response _response = id.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();

            if (_response.code.Equals("1") && _response.service.Equals(EnumServiceType.toCpTransaction.ToEnumDesc()))
            {
                ViewBag.ResultStatu = true;
                ViewBag.Message = "恭喜你，投标成功。";
            }
            else
            {
                ViewBag.Message = "对不起，投标失败。";
            }
            return View("Index");
        }

        /// <summary>
        /// 绑卡回调
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BindBankCardCallBack(YeePayCallBack id)
        {
            MoneyCarCar.Models.YeePay.response _response = id.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();

            if (_response.code.Equals("1") && _response.service.Equals(EnumServiceType.toBindBankCard.ToEnumDesc()))
            {
                ViewBag.ResultStatu = true;
                ViewBag.Message = "恭喜你，银行卡绑定成功。";
            }
            else
            {
                ViewBag.Message = "对不起，银行卡绑定失败。";
            }
            return View("Index");
        }

        /// <summary>
        /// 取消绑卡回调
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UnbindBankCardCallBack(YeePayCallBack id)
        {
            StringBuilder postData = new StringBuilder();
            postData.Append("req=" + dataFornat.UrlEncode(id.resp)); //编码
            postData.Append("&sign=" + dataFornat.UrlEncode(id.sign)); //编码

            MoneyCarCar.Models.YeePay.response _response = id.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();

            if (_response.code.Equals("1") && _response.service.Equals(EnumServiceType.toUnbindBankCard.ToEnumDesc()))
            {
                string strHttpPost = HttpHelper.CreatHelper().HttpPost(yeePayConfig._verifyUrl, postData.ToString());
                // 2. 验证签名
                if (strHttpPost.Contains("SUCCESS"))
                {
                    SystemUsers userInfo = (SystemUsers)Session["UserInfo"];
                    HttpHelper.CreatHelper().DoGetObject<BaseResultDto<bool>>(ApplicationPropertys.WEBAPI_URL + "/User/UnBindBank/" + userInfo.ID);
                    ViewBag.ResultStatu = true;
                    ViewBag.Message = "恭喜你，取消绑卡成功。";
                }
                else
                {
                    ViewBag.Message = "对不起,签名验证失败。";
                }

            }
            else
            {
                ViewBag.Message = "对不起,取消绑卡失败。";
            }
            return View("Index");
        }

        /// <summary>
        /// 充值回调
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Rechare(YeePayCallBack id)
        {
            MoneyCarCar.Models.YeePay.response _response = id.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();

            if (_response.code.Equals("1") && _response.service.Equals(EnumServiceType.toRecharge.ToEnumDesc()))
            {
                ViewBag.ResultStatu = true;
                ViewBag.Message = "恭喜你，充值成功。";
            }
            else
            {
                ViewBag.Message = "对不起，充值失败。";
            }
            return View("Index");
        }

        /// <summary>
        /// 提款回调
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Withdraw(YeePayCallBack id)
        {
            MoneyCarCar.Models.YeePay.response _response = id.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();

            if (_response.code.Equals("1") && _response.service.Equals(EnumServiceType.toWithdraw.ToEnumDesc()))
            {
                ViewBag.ResultStatu = true;
                ViewBag.Message = "恭喜你，提款成功。";
            }
            else
            {
                ViewBag.Message = "对不起，提款失败。";
            }
            return View("Index");
        }
    }
}
