using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Models;
using MoneyCarCar.DAL;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.YeePay;
using MoneyCarCar.Models.Statisticals.Return;
using MoneyCarCar.Models.Statisticals.Parameter;

namespace MoneyCarCar.DataApi.Controllers
{
    public class UserController : ApiController
    {
        SystemUsersOper oper = new SystemUsersOper();
        BaseHelper helper = new BaseHelper();
        YeePay yeepay = new YeePay();

        //添加用户
        [HttpPost]
        public int Add(UserReg model)
        {
            string errorMsg = "";
            SystemUsers user = new SystemUsers();
            user.UserName = model.UserName;
            user.UserPassword = model.UserPwd;
            user.UserEmail = model.UserEmail;
            user.CellPhone = model.UserPhone;
            user.CellPahoneIsAuthenticate = true;
            user.UserState = 1;
            user.UserType = model.UserTpye;
            user.RegTime = DateTime.Now.ToString(1);
            user.Recommended = model.Recommended;
            return oper.SystemUsers_Registered(user, out errorMsg);
        }


        //账号重复检测
        [HttpPost]
        public int CheckAccount(PhoneOrNameCheck model)
        {
            return oper.Exists(" UserName ='" + model.Infos + "' and UserType =" + model.UserType.ToString());
        }

        //手机重复检测
        [HttpPost]
        public int CheckPhone(PhoneOrNameCheck model)
        {
            return oper.Exists(" CellPhone ='" + model.Infos + "' and UserType =" + model.UserType.ToString());
        }

        //账号或手机号登陆
        [HttpPost]
        public BaseResultDto<SystemUsers> UserLogin(UserLogin model)
        {
            return oper.Landing(model.UserNameOrPhone, model.UserPassword, model.UserIP, model.UserType);
        }

        // 修改密码
        [HttpPost]
        public BaseResultDto<string> UpdatePwd(RQPwdDto model)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            List<string> keyVal = new List<string>();
            keyVal.Add(string.Format("UserPassword='{0}'", model.NewPwd));
            string sqlWhere = string.Format("UserPassword='{0}' and ID={1}", model.OriPwd, model.UserId);
            if (helper.Exists<SystemUsers>(sqlWhere) > 0)
            {
                if (oper.Update(keyVal, sqlWhere))
                {
                    result.ErrorCode = 1;
                    result.ErrorMsg = "成功";
                }
                else
                {
                    result.ErrorCode = -2;
                    result.ErrorMsg = "修改失败";
                }
            }
            else
            {
                result.ErrorCode = -1;
                result.ErrorMsg = "原密码错误";
            }
            return result;
        }

        //更新具体的部分数据
        [HttpPost]
        public bool UpdatePartal(RQUpdate<SystemClaims> model)
        {
            return helper.Update<SystemClaims>(model.Tag, model.UpdateFileds);
        }

        //账号启用和禁用
        [HttpPost]
        public BaseResultDto<string> UpdateUserState(RQUserStateDto model)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            List<string> keyVal = new List<string>();
            keyVal.Add(string.Format("UserState={0}", (model.UserState ? 1 : 0)));
            string sqlWhere = string.Format(" ID={0}", model.UserId);
            if (oper.Update(keyVal, sqlWhere))
            {
                result.ErrorCode = 1;
                result.ErrorMsg = "成功";
            }
            else
            {
                result.ErrorCode = -1;
                result.ErrorMsg = "失败";
            }
            return result;
        }

        //添加业务员
        [HttpPost]
        public BaseResultDto<string> AddSalesman(RQSalesmanDto model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            if (!helper.IsExists<SystemUsers>(string.Format(" UserName ='{0}' and UserType ={1} ", model.UserName, model.UserType)))
            {
                if (!helper.IsExists<SystemUsers>(string.Format(" CellPhone ='{0}' and UserType ={1} ", model.Mobile, model.UserType)))
                {
                    if (!helper.IsExists<SystemUsers>(string.Format(" IDNumber ='{0}' and UserType ={1} ", model.IDNumber, model.UserType)))
                    {
                        SystemUsers systemUsers = new SystemUsers();
                        systemUsers.UserName = model.UserName;
                        systemUsers.UserPassword = model.UserName;
                        systemUsers.CellPhone = model.Mobile;
                        systemUsers.RealName = model.RealName;
                        systemUsers.IDNumber = model.IDNumber;
                        systemUsers.UserEmail = model.Email;
                        systemUsers.UserAddress = model.Address;
                        systemUsers.UserType = int.Parse(model.UserType);
                        systemUsers.RegTime = model.RegTime;
                        systemUsers.CellPahoneIsAuthenticate = true;
                        systemUsers.IDNumberIsAuthenticate = true;

                        int Id = helper.Add<SystemUsers>(systemUsers);
                        resultDto.ErrorCode = Id;
                        if (Id > 0)
                        {
                            resultDto.ErrorMsg = "注册成功";
                        }
                        else
                        {
                            resultDto.ErrorCode = -1;
                            resultDto.ErrorMsg = "注册失败";
                        }
                    }
                    else
                    {
                        resultDto.ErrorCode = -2;
                        resultDto.ErrorMsg = string.Format("身份证{0}已注册", model.IDNumber);
                    }
                }
                else
                {
                    resultDto.ErrorCode = -3;
                    resultDto.ErrorMsg = string.Format("手机号{0}已注册", model.Mobile);
                }
            }
            else
            {
                resultDto.ErrorCode = -4;
                resultDto.ErrorMsg = string.Format("用户名{0}已注册", model.UserName);
            }
            return resultDto;
        }

        //编辑业务员
        [HttpPost]
        public BaseResultDto<string> EditSalesman(RQUpdate<SystemUsers> model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            SystemUsers systemUsers = model.Tag;
            if (!helper.IsExists<SystemUsers>(string.Format(" UserName ='{0}' and UserType ={1} ", systemUsers.UserName, systemUsers.UserType)))
            {
                if (!helper.IsExists<SystemUsers>(string.Format(" CellPhone ='{0}' and UserType ={1} ", systemUsers.CellPhone, systemUsers.UserType)))
                {
                    if (!helper.IsExists<SystemUsers>(string.Format(" IDNumber ='{0}' and UserType ={1} ", systemUsers.IDNumber, systemUsers.UserType)))
                    {
                        if (helper.Update<SystemUsers>(systemUsers, model.UpdateFileds))
                        {
                            resultDto.IsSeccess = true;
                            resultDto.ErrorCode = 1;
                            resultDto.ErrorMsg = "编辑成功";
                        }
                        else
                        {
                            resultDto.ErrorCode = -1;
                            resultDto.ErrorMsg = "编辑失败";
                        }
                    }
                    else
                    {
                        resultDto.ErrorCode = -2;
                        resultDto.ErrorMsg = string.Format("身份证{0}已存在", systemUsers.IDNumber);
                    }
                }
                else
                {
                    resultDto.ErrorCode = -3;
                    resultDto.ErrorMsg = string.Format("手机号{0}已存在", systemUsers.CellPhone);
                }
            }
            else
            {
                resultDto.ErrorCode = -4;
                resultDto.ErrorMsg = string.Format("用户名{0}已存在", systemUsers.UserName);
            }
            return resultDto;
        }

        //分页
        [HttpPost]
        public ModelByCount<SystemUsers> GetList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemUsers> list = helper.GetPagerList<SystemUsers>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemUsers> mc = new ModelByCount<SystemUsers>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }

        //身份证验证
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> AuthenticateIDCard(SystemUsers model)
        {
            BaseResultDto<PostBaseYeePayPar> result = null;

            BaseResultDto<bool> re = oper.AuthenticateIDCard(model);
            if (re.IsSeccess)
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToRegister toRegister = new MoneyCarCar.Models.YeePay.RequestModel.ToRegister();

                toRegister.email = model.UserEmail;
                toRegister.idCardNo = model.IDNumber;
                toRegister.mobile = model.CellPhone;
                toRegister.nickName = model.UserName;
                toRegister.realName = model.RealName;
                toRegister.platformUserNo = model.ID + "";
                toRegister.requestNo = DateTime.Now.Ticks.ToString();
                result = yeepay.ToRegister(toRegister);
            }
            else
            {
                result = new BaseResultDto<PostBaseYeePayPar>();
                result.IsSeccess = false;
                result.ErrorMsg = re.ErrorMsg;
            }

            return result;
        }

        //获取用户资金
        [HttpGet]
        public BaseResultDto<decimal> GetUserMoney(int value)
        {
            return oper.GetUserMoney(value);
        }

        //获取用户虚拟本金余额
        [HttpGet]
        public BaseResultDto<decimal> GetUserVirtualMoney(int value)
        {
            return oper.GetUserVirtualMoney(value);
        }

        //获取用户信息(更新使用)
        [HttpGet]
        public SystemUsers GetUserInfo(int value)
        {
            return oper.GetUserInfo(value);
        }
        //获取特定用户列表
        [HttpGet]
        public BaseResultDto<RQUserListDto> GetUserList(int userType)
        {
            BaseResultDto<RQUserListDto> resultDto = new BaseResultDto<RQUserListDto>();
            try
            {
                RQProcParam proc = new RQProcParam();
                proc.ProcName = "Proc_GetUserList";
                proc.DicParam.Add("userType", userType);
                RQUserListDto userListDto = new RQUserListDto();
                userListDto.UserList = helper.GetDataByProc<UserRow>(proc);
                resultDto.Tag = userListDto;
                if (userListDto != null && userListDto.UserList.Count > 0)
                {
                    resultDto.ErrorMsg = "成功";
                    resultDto.ErrorCode = 1;
                    resultDto.IsSeccess = true;
                }
                else
                {
                    resultDto.ErrorMsg = "失败";
                    resultDto.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                resultDto.ErrorMsg = ex.Message;
                resultDto.ErrorCode = -1;
            }
            return resultDto;
        }
        //赠送虚拟本金
        [HttpPost]
        public BaseResultDto<string> GiveVirtualMoney(GiveVirtualMoneyDto model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                RQProcParam proc = new RQProcParam();
                proc.ProcName = "Proc_GiveVirtualMoney";
                proc.DicParam.Add("Ids", model.Ids);
                proc.DicParam.Add("IsAllUser", model.IsAllUser);
                proc.DicParam.Add("GiveMoney", model.GiveMoney);
                proc.DicParam.Add("BountyType", model.BountyType);
                proc.DicParam.Add("BountyRes", model.BountyRes);
                proc.DicParam.Add("OverTime", model.OverTime);
                proc.DicParam.Add("OperatorUserId", model.OperatorUserId);
                proc.DicParam.Add("OperatorUserName", model.OperatorUserName);
                proc.DicParam.Add("IP", model.IP);
                int status = helper.ExecReturnByProc(proc).ToInt();
                if (status == 0)
                {
                    resultDto.ErrorMsg = "赠送成功";
                    resultDto.ErrorCode = 1;
                    resultDto.IsSeccess = true;
                }
                else
                {
                    resultDto.ErrorMsg = "赠送失败";
                    resultDto.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                resultDto.ErrorMsg = ex.Message;
                resultDto.ErrorCode = -1;
            }
            return resultDto;
        }

        //绑定银行卡
        [HttpGet]
        public BaseResultDto<PostBaseYeePayPar> BindBankCard(int value)
        {
            BaseResultDto<PostBaseYeePayPar> result = new BaseResultDto<PostBaseYeePayPar>();
            string errorMsg = "";
            int reID = oper.BindBankCard(value, out errorMsg);
            if (reID <= 0)
            {
                result.IsSeccess = false;
                result.ErrorMsg = errorMsg;
                return result;
            }
            MoneyCarCar.Models.YeePay.RequestModel.ToBindBankCard toBindBankCard = new MoneyCarCar.Models.YeePay.RequestModel.ToBindBankCard();

            toBindBankCard.platformUserNo = value + "";
            toBindBankCard.requestNo = reID + "";

            return yeepay.ToBindBankCard(toBindBankCard);
        }
        //获取绑定卡的信息
        [HttpGet]
        public SystemBankCard GetBindBank(int value)
        {
            return oper.GetBindCard(value);
        }
        //解除银行卡绑定
        [HttpGet]
        public BaseResultDto<PostBaseYeePayPar> UnBindBankRequest(int value)
        {
            MoneyCarCar.Models.YeePay.RequestModel.ToUnbindBankCard toUnbindBankCard = new MoneyCarCar.Models.YeePay.RequestModel.ToUnbindBankCard();

            toUnbindBankCard.platformUserNo = value + "";

            return yeepay.ToUnbindBankCard(toUnbindBankCard);
        }
        //解除银行卡绑定
        [HttpGet]
        public BaseResultDto<bool> UnBindBank(int value)
        {
            return oper.UnBankCard(value);
        }
        //充值
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> Rechare(SystemUsers model)
        {
            BaseResultDto<PostBaseYeePayPar> result = new BaseResultDto<PostBaseYeePayPar>();
            string errorMsg = "";
            int reID = oper.Rechare(model.ID, model.Balance, out errorMsg);
            if (reID <= 0)
            {
                result.IsSeccess = false;
                result.ErrorMsg = errorMsg;
                return result;
            }
            MoneyCarCar.Models.YeePay.RequestModel.ToRecharge torechare = new MoneyCarCar.Models.YeePay.RequestModel.ToRecharge();

            torechare.platformUserNo = model.ID + "";
            torechare.requestNo = reID + "";
            torechare._amount = model.Balance.ToString("0.00");

            return yeepay.ToRecharge(torechare);
        }

        //提款
        [HttpPost]
        public BaseResultDto<PostBaseYeePayPar> Withdraw(SystemUsers model)
        {
            BaseResultDto<PostBaseYeePayPar> result = new BaseResultDto<PostBaseYeePayPar>();
            string errorMsg = "";
            int reID = oper.Withdraw(model.ID, model.Balance, out errorMsg);
            if (reID <= 0)
            {
                result.IsSeccess = false;
                result.ErrorMsg = errorMsg;
                return result;
            }
            MoneyCarCar.Models.YeePay.RequestModel.ToWithdraw towithdraw = new MoneyCarCar.Models.YeePay.RequestModel.ToWithdraw();

            towithdraw.platformUserNo = model.ID + "";
            towithdraw.requestNo = reID + "";
            towithdraw._amount = model.Balance.ToString("0.00");

            return yeepay.ToWithdraw(towithdraw);
        }

        #region 数据统计
        //统计总收益和总投资
        [HttpPost]
        public Earnings_Return Totalrevenue(Earnings_Parameter model)
        {
            return oper.Totalrevenue(model);
        }
        [HttpPost]
        //资金流水
        public ModelByCount<TransactionRecord_Return> TransactionRecord(TransactionRecord_Parameter model)
        {
            return oper.TransactionRecord(model);
        }
        [HttpPost]
        //提现记录
        public ModelByCount<SystemRequestRecord> GetWithdrawRecord(TransactionRecord_Parameter model)
        {
            return oper.GetWithdrawRecord(model);
        }
        #endregion
        
    }
}
