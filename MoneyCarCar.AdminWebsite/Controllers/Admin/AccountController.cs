using MoneyCarCar.Commons;
using MoneyCarCar.Commons.Enum;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            var model = new
            {
                //查询的表单实体
                searchForm = new
                {
                    startDate = DateTime.Now.AddMonths(-1),
                    endDate = DateTime.Now,
                    loginAccount = string.Empty,
                    realName = string.Empty,
                    idNumber = string.Empty,
                    userEmail = string.Empty,
                    userState = string.Empty
                },
                urls = new
                {
                    search = "/Account/SearchUsers",
                    setInfo = "/Account/setInfo"
                }
            };
            return View(model);
        }
        public ActionResult SearchUsers(FormCollection form, int page = 1, int rows = 10)
        {
            JsonResult result = null;
            try
            {
                //查询投资借贷人
                StringBuilder sbCon = new StringBuilder();
                if (form["userType"] != null && !string.IsNullOrEmpty(form["userType"]))
                {
                    if (this.UserInfo.UserType == 2)
                        sbCon.AppendFormat(" UserType in(2,3) ");
                    else
                        sbCon.AppendFormat(" UserType = {0} ", this.UserInfo.UserType);
                }
                else
                {
                    sbCon.Append(" UserType=1 ");
                }
                //登录账号
                if (form["loginAccount"] != null && !string.IsNullOrEmpty(form["loginAccount"]))
                {
                    string loginAccount = form["loginAccount"].ToString();
                    sbCon.AppendFormat(" and (UserName like '%{0}%' or CellPhone like '%{0}%' )", loginAccount.Replace("'", ""));
                }
                //真实姓名
                if (form["realName"] != null && !string.IsNullOrEmpty(form["realName"]))
                {
                    string realName = form["realName"].ToString();
                    sbCon.AppendFormat(" and RealName like '%{0}%' ", realName.Replace("'", ""));
                }
                //身份证号
                if (form["idNumber"] != null && !string.IsNullOrEmpty(form["idNumber"]))
                {
                    string idNumber = form["idNumber"].ToString();
                    sbCon.AppendFormat(" and IDNumber like '%{0}%'", idNumber.Replace("'", ""));
                }
                //邮箱
                if (form["userEmail"] != null && !string.IsNullOrEmpty(form["userEmail"]))
                {
                    string userEmail = form["userEmail"].ToString();
                    sbCon.AppendFormat(" and UserEmail like '%{0}%'", userEmail.Replace("'", ""));
                }
                //账号状态
                if (form["userState"] != null && !string.IsNullOrEmpty(form["userState"]))
                {
                    string userState = form["userState"].ToString();
                    sbCon.AppendFormat(" and UserState ={0} ", userState.Replace("'", ""));
                }
                //表单的实体             
                if (form["startDate"] != null && form["endDate"] != null)
                {
                    string startDate = DateTime.Parse(form["startDate"].ToString()).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Parse(form["endDate"].ToString()).ToString("yyyy-MM-dd");
                    sbCon.AppendFormat(" and RegTime between '{0} 00:00:00' and '{1} 23:59:59' ", startDate, endDate);
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
                ModelByCount<SystemUsers> userData = HttpHelper.CreatHelper().DoPostObject<ModelByCount<SystemUsers>>(string.Format("{0}User/GetList", this.WebApiUrl), pager);
                result = Json(new { total = userData.AllCount, rows = userData.ListAll }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        public JsonResult setInfo(SystemUsers userModel, string flag)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                if (flag == "pwd")
                {
                    //初始密码
                    RQPwdDto model = new RQPwdDto();
                    model.UserId = userModel.ID;
                    model.OriPwd = userModel.UserPassword;
                    model.NewPwd = EnumDictionary.UserResetPwd.GetMd5Code();
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}User/UpdatePwd", this.WebApiUrl), model);
                }
                else if (flag == "userstate")
                {
                    RQUserStateDto model = new RQUserStateDto();
                    if (userModel.UserState == 0)
                    {
                        model.UserState = true;
                    }
                    else if (userModel.UserState == 1)
                    {
                        model.UserState = false;
                    }
                    model.UserId = userModel.ID;
                    resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}User/UpdateUserState", this.WebApiUrl), model);
                }
                else
                {
                    resultDto.ErrorCode = -1;
                    resultDto.ErrorMsg = "操作非法";
                }
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public ActionResult AddEditUser(FormCollection form)
        {
            JsonResult result = null;
            try
            {
                //先验证
                //....
                //再添加
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                if (form["UserName"] != null && form["UserName"].ToString() != "")
                {
                    int userId = int.Parse(form["ID"].ToString());//可以根据Id来判断是添加还是修改
                    string userName = form["UserName"].ToString();
                    string userpwd = form["UserPassword"].ToString();
                    string reUserPwd = form["reUserPwd"].ToString();
                    string mobile = form["CellPhone"].ToString();
                    string realName = form["RealName"].ToString();
                    string cardID = form["IDNumber"].ToString();
                    string email = form["UserEmail"].ToString();
                    string address = form["UserAddress"].ToString();
                    string userType = form["UserType"].ToString();
                    RQSalesmanDto model = new RQSalesmanDto();
                    model.UserName = userName;
                    model.UserPwd = userId > 0 ? userpwd : userpwd.GetMd5Code();
                    model.Mobile = mobile;
                    model.RealName = realName;
                    model.IDNumber = cardID;
                    model.Email = email;
                    model.Address = address;
                    model.UserType = userType;//业务员类型
                    model.RegTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (userId > 0)
                    {
                        RQUpdate<SystemUsers> modelUpdate = new RQUpdate<SystemUsers>();
                        SystemUsers systemUsers = new SystemUsers();
                        systemUsers.ID = userId;//更新条件
                        modelUpdate.Tag = systemUsers;
                        systemUsers.CellPhone = model.Mobile;
                        systemUsers.RealName = model.RealName;
                        systemUsers.IDNumber = model.IDNumber;
                        systemUsers.UserEmail = model.Email;
                        systemUsers.UserAddress = model.Address;
                        modelUpdate.UpdateFileds.AddRange(new string[]{
                            "CellPhone","RealName","IDNumber","UserEmail","UserAddress"
                        });
                        //编辑
                        resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}User/EditSalesman", this.WebApiUrl), modelUpdate);
                    }
                    else
                    {
                        //成功返回Id
                        resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}User/AddSalesman", this.WebApiUrl), model);
                    }
                }
                else
                {
                    resultDto.ErrorCode = -1;
                    resultDto.ErrorMsg = "请求出错";
                }
                result = Json(new { status = resultDto.ErrorCode, message = resultDto.ErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = -1, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        public JsonResult CurrPwdUpdate(FormCollection form)
        {
            JsonResult result = null;
            try
            {
                BaseResultDto<string> resultDto = new BaseResultDto<string>();
                if (form.Count > 0 && this.UserInfo != null)
                {
                    string stropwd = form["opwd"];
                    string strnpwd = form["npwd"];
                    string strrpwd = form["rpwd"];
                    if (strnpwd.Equals(strrpwd))
                    {
                        RQPwdDto model = new RQPwdDto();
                        model.UserId = this.UserInfo.ID;
                        model.OriPwd = stropwd.GetMd5Code();
                        model.NewPwd = strnpwd.GetMd5Code();
                        resultDto = HttpHelper.CreatHelper().DoPostObject<BaseResultDto<string>>(string.Format("{0}User/UpdatePwd", this.WebApiUrl), model);
                        if (resultDto.ErrorCode == 1)
                        {
                            resultDto.ErrorMsg = strnpwd;
                        }
                    }
                    else
                    {
                        resultDto.ErrorMsg = "新密码和确认密码不一致";
                        resultDto.ErrorCode = -3;
                    }
                }
                else
                {
                    resultDto.ErrorMsg = "修改失败";
                    resultDto.ErrorCode = -2;
                }
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
