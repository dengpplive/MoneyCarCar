using MoneyCarCar.Commons;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    /// <summary>
    /// 业务推广
    /// </summary>
    public class BusinessPopController : BaseController
    {
        /// <summary>
        /// 赠送虚拟本金首页
        /// </summary>
        /// <returns></returns>
        public ActionResult GiveVirtualMoney()
        {
            try
            {
                BaseResultDto<RQUserListDto> resultDto = HttpHelper.CreatHelper().DoGetObject<BaseResultDto<RQUserListDto>>(string.Format("{0}User/GetUserList?userType=1", this.WebApiUrl), this.WebApiTimeOut);
                ViewBag.user = null;
                if (resultDto != null)
                    ViewBag.user = resultDto.Tag.UserList;
            }
            catch
            {
            }
            return View();
        }
        /// <summary>
        ///  赠送虚拟本金
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PostGiveVirtualMoney(FormCollection form)
        {
            JsonResult result = null;
            try
            {
                GiveVirtualMoneyDto model = new GiveVirtualMoneyDto();
                if (form["isAllUser"] != null && form["isAllUser"].ToString() != "")
                {
                    int isAllUser = int.Parse(form["isAllUser"].ToString());
                    model.IsAllUser = isAllUser;
                }
                if (form["ids"] != null && form["ids"].ToString() != "")
                {
                    string ids = form["ids"].ToString();
                    model.Ids = ids;
                }
                if (form["money"] != null && form["money"].ToString() != "" && int.Parse(form["money"].ToString()) > 0)
                {
                    int money = int.Parse(form["money"].ToString());
                    model.GiveMoney = money;
                }
                string strMessage = string.Empty;
                int status = 0;
                if (model.IsAllUser == 0)
                {
                    string[] strArr = model.Ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArr.Length == 0)
                    {
                        strMessage = "请选择投资用户";
                    }
                }
                if (model.GiveMoney <= 0)
                {
                    strMessage = "请输入有效的金额";
                }
                if (string.IsNullOrEmpty(strMessage))
                {
                    model.OverTime = DateTime.Now.AddMonths(3);
                    model.OperatorUserId = this.UserInfo.ID;
                    model.OperatorUserName = this.UserInfo.RealName;
                    model.BountyRes = 3;
                    model.BountyType = 1;
                    model.IP = this.RequestIP;
                    BaseResultDto<string> resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}User/GiveVirtualMoney", this.WebApiUrl), model, this.WebApiTimeOut);
                    status = resultDto.ErrorCode;
                    strMessage = resultDto.ErrorMsg;
                }
                result = Json(new { status = status, message = strMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}
