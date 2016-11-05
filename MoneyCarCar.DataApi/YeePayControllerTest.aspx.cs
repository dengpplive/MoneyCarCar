using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.YeePay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoneyCarCar.DAL;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using System.Data.SqlClient;
using System.Data;
using MoneyCarCar.Models.YeePay.RequestModel;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;
using MoneyCarCar.Models.YeePay.Response.RECONCILIATION;
using System.Xml.Linq;



namespace MoneyCarCar.DataApi
{
    public partial class YeePayControllerTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            platformUserNo = TextBox1.Text.Trim();
        }
        /// <summary>
        /// 生成数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(platformUserNo))
            {
                Response.Write("<script>alert('请输入唯一商户号！')</script>");
                return;
            }

            if (string.IsNullOrEmpty(TextBox2.Text.Trim()))
            {
            }
            else
            {
                requestNo = TextBox2.Text.Trim();
            }


            string str = "";

            //21 注册      o
            //22 充值      o
            //23 提现      o
            //24 绑卡      o
            //25 取消绑卡  o
            //26 企业用户注册 o
            //27 转账授权 o
            //28 自动投标授权 o
            //29 自动还款授权  o

            //31 账户查询 o
            //32 资金冻结 o
            //33 资金解冻 o
            //34 直接转账 o
            //35 自动转账授权
            //36 单笔业务查询 o
            //37 转账确认


            if (rblPayType.SelectedValue == "21") //21 注册   o  
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToRegister toRegister = new MoneyCarCar.Models.YeePay.RequestModel.ToRegister();

                toRegister.email = "261065527@qq.com";
                toRegister.idCardNo = "510722198611227657";
                toRegister.mobile = "13438377161";
                toRegister.nickName = "nidongde";
                toRegister.realName = "jh";
                toRegister.platformUserNo = platformUserNo;
                toRegister.requestNo = requestNo;

                baseResultDto = yeepay.ToRegister(toRegister);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "22") //22 充值 
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToRecharge toRecharge = new MoneyCarCar.Models.YeePay.RequestModel.ToRecharge();
                toRecharge.requestNo = requestNo;
                toRecharge.platformUserNo = platformUserNo;
                toRecharge._amount = "";
                baseResultDto = yeepay.ToRecharge(toRecharge);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "23") //23 提现      o
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToWithdraw toWithdraw = new MoneyCarCar.Models.YeePay.RequestModel.ToWithdraw();

                toWithdraw.platformUserNo = platformUserNo;
                toWithdraw.requestNo = requestNo;
                toWithdraw._amount = "";


                baseResultDto = yeepay.ToWithdraw(toWithdraw);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "24")  //24 绑卡      o
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToBindBankCard toBindBankCard = new MoneyCarCar.Models.YeePay.RequestModel.ToBindBankCard();

                toBindBankCard.platformUserNo = platformUserNo;
                toBindBankCard.requestNo = requestNo;

                baseResultDto = yeepay.ToBindBankCard(toBindBankCard);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "25")//25 取消绑卡  o
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToUnbindBankCard toUnbindBankCard = new MoneyCarCar.Models.YeePay.RequestModel.ToUnbindBankCard();

                toUnbindBankCard.platformUserNo = platformUserNo;
                toUnbindBankCard.requestNo = requestNo;

                baseResultDto = yeepay.ToUnbindBankCard(toUnbindBankCard);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "26")  //26 企业用户注册 o
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToEnterpriseRegister toEnterpriseRegister = new MoneyCarCar.Models.YeePay.RequestModel.ToEnterpriseRegister();

                toEnterpriseRegister.platformUserNo = platformUserNo;
                toEnterpriseRegister.requestNo = requestNo;
                toEnterpriseRegister.bankLicense = "123456";
                toEnterpriseRegister.businessLicense = "123456";
                toEnterpriseRegister.contact = "张三";
                toEnterpriseRegister.contactPhone = "13888888888";
                toEnterpriseRegister.email = "123456@qq.com";
                toEnterpriseRegister.enterpriseName = "企业名称";
                toEnterpriseRegister.legal = "法人姓名";
                toEnterpriseRegister.legalIdNo = "510722";
                toEnterpriseRegister.orgNo = "123456";
                toEnterpriseRegister.taxNo = "123456";

                baseResultDto = yeepay.ToEnterpriseRegister(toEnterpriseRegister);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "271")   //27 （1）转账［TRANSFER］
            {

                MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_TRANSFER toCpTransaction = new MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_TRANSFER();

                toCpTransaction.platformUserNo = platformUserNo; // 商户号
                toCpTransaction.requestNo = requestNo; // 请求流水号


                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();

                //ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                //paydetail.amount = "100.00";
                //paydetail.targetPlatformUserNo = "60000";
                //paydetail.targetUserType = EnumUserType.MERCHANT.ToEnumDesc(); // 用户类型
                //paydetail.bizType = EnumBizType.TRANSFER.ToEnumDesc();//转账
                //details.Add(paydetail);

                ToCpTransactionDetail paydetail2 = new ToCpTransactionDetail();
                paydetail2.amount = "100.00";
                paydetail2.targetPlatformUserNo = "60000";
                paydetail2.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail2.bizType = EnumBizType.TRANSFER.ToEnumDesc();//转账
                details.Add(paydetail2);


                toCpTransaction.details = details;

                baseResultDto = yeepay.ToCpTransaction_TRANSFER(toCpTransaction);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "272")//（2）投标［TENDER］
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_TENDER toCpTransaction = new MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_TENDER();

                toCpTransaction.platformUserNo = platformUserNo;
                toCpTransaction.requestNo = requestNo;

                //（2）投标［TENDER］

                toCpTransaction.tenderOrderNo = "0001";
                toCpTransaction.tenderName = "好项目";
                toCpTransaction.tenderAmount = "110.00";
                toCpTransaction.tenderDescription = "恩很好";
                toCpTransaction.borrowerPlatformUserNo = "1233211234567";


                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                paydetail.amount = "100.00";
                paydetail.targetPlatformUserNo = "10040011137";
                paydetail.targetUserType = EnumUserType.MERCHANT.ToEnumDesc(); // 用户类型
                paydetail.bizType = EnumBizType.TENDER.ToEnumDesc();//转账

                details.Add(paydetail);


                ToCpTransactionDetail paydetail2 = new ToCpTransactionDetail();
                paydetail2.amount = "300.00";
                paydetail2.targetPlatformUserNo = "1212";
                paydetail2.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail2.bizType = EnumBizType.TRANSFER.ToEnumDesc();//转账
                details.Add(paydetail2);

                toCpTransaction.details = details;


                baseResultDto = yeepay.ToCpTransaction_TENDER(toCpTransaction);

                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "273")//（3）还款［REPAYMENT］
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_REPAYMENT toCpTransaction = new MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_REPAYMENT();

                toCpTransaction.platformUserNo = platformUserNo;
                toCpTransaction.requestNo = requestNo;

                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                paydetail.amount = "100.00";
                paydetail.targetPlatformUserNo = "60000";
                paydetail.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail.bizType = EnumBizType.REPAYMENT.ToEnumDesc();//转账
                details.Add(paydetail);
                toCpTransaction.details = details;

                //ToCpTransactionDetail paydetail2 = new ToCpTransactionDetail();
                //paydetail2.amount = "200.00"; // 平台收入
                //paydetail2.targetPlatformUserNo = "10040011137";
                //paydetail2.targetUserType = EnumUserType.MERCHANT.ToEnumDesc();
                //paydetail2.bizType = EnumBizType.COMMISSION.ToEnumDesc();//还款,平台分账
                //details.Add(paydetail2);
                //toCpTransaction.details = details;

                toCpTransaction.tenderOrderNo = "1";

                baseResultDto = yeepay.ToCpTransaction_REPAYMENT(toCpTransaction);

                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "274")//（4）债权转让［CREDIT_ASSIGNMENT］
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_CREDIT_ASSIGNMENT toCpTransaction = new MoneyCarCar.Models.YeePay.RequestModel.ToCpTransaction_CREDIT_ASSIGNMENT();

                toCpTransaction.platformUserNo = platformUserNo;
                toCpTransaction.requestNo = requestNo;

                //（4）债权转让［CREDIT_ASSIGNMENT］
                toCpTransaction.tenderOrderNo = "0001";
                toCpTransaction.originalRequestNo = "";
                toCpTransaction.creditorPlatformUserNo = "";

                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                paydetail.amount = "100.00";
                paydetail.targetPlatformUserNo = "1212";
                paydetail.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail.bizType = EnumBizType.CREDIT_ASSIGNMENT.ToEnumDesc();//债权转让
                details.Add(paydetail);

                ToCpTransactionDetail paydetail2 = new ToCpTransactionDetail();
                paydetail2.amount = "10.00";
                paydetail2.targetPlatformUserNo = "10040011137";
                paydetail2.targetUserType = EnumUserType.MERCHANT.ToEnumDesc(); // 用户类型
                paydetail2.bizType = EnumBizType.COMMISSION.ToEnumDesc();//债权转让,平台分账
                details.Add(paydetail2);

                toCpTransaction.details = details;

                baseResultDto = yeepay.ToCpTransaction_CREDIT_ASSIGNMENT(toCpTransaction);

                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "28")   //28 自动投标授权 o
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToAuthorizeAutoTransfer toAuthorizeAutoTransfer = new MoneyCarCar.Models.YeePay.RequestModel.ToAuthorizeAutoTransfer();

                toAuthorizeAutoTransfer.platformUserNo = platformUserNo;
                toAuthorizeAutoTransfer.requestNo = requestNo;
                baseResultDto = yeepay.ToAuthorizeAutoTransfer(toAuthorizeAutoTransfer);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "29")  //29 自动还款授权  o
            {
                MoneyCarCar.Models.YeePay.RequestModel.ToAuthorizeAutoRepayment toAuthorizeAutoRepayment = new MoneyCarCar.Models.YeePay.RequestModel.ToAuthorizeAutoRepayment();
                toAuthorizeAutoRepayment.platformUserNo = platformUserNo;
                toAuthorizeAutoRepayment.requestNo = DateTime.Now.Ticks.ToString();
                toAuthorizeAutoRepayment.orderNo = requestNo;

                baseResultDto = yeepay.ToAuthorizeAutoRepayment(toAuthorizeAutoRepayment);

                //组合表单数据
                str = ActionFrom(baseResultDto.ErrorMsg, baseResultDto.Tag.req, baseResultDto.Tag.sign);
            }
            else if (rblPayType.SelectedValue == "31") //31 账户查询 o
            {
                MoneyCarCar.Models.YeePay.RequestModel.Account_Info account_Info = new MoneyCarCar.Models.YeePay.RequestModel.Account_Info();
                account_Info.platformUserNo = platformUserNo;

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>();
                baseResultDtoResponse = yeepay.ACCOUNT_INFO(account_Info);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据


                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>();

                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }
            else if (rblPayType.SelectedValue == "32")  //32 资金冻结 o
            {
                MoneyCarCar.Models.YeePay.RequestModel.Freeze freeze = new MoneyCarCar.Models.YeePay.RequestModel.Freeze();
                freeze.platformUserNo = platformUserNo;
                freeze.requestNo = requestNo;
                freeze.amount = "500.00";
                freeze.expired = "2015-01-05 12:12:12";

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.FREEZE.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.FREEZE.response>();

                baseResultDtoResponse = yeepay.FREEZE(freeze);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.FREEZE.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.FREEZE.response>();

                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion

            }
            else if (rblPayType.SelectedValue == "33") //33 资金解冻 o
            {
                MoneyCarCar.Models.YeePay.RequestModel.UnFreeze unFreeze = new MoneyCarCar.Models.YeePay.RequestModel.UnFreeze();
                unFreeze.platformUserNo = platformUserNo;
                unFreeze.freezeRequestNo = requestNo;  // 冻结时流水号

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.UNFREEZE.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.UNFREEZE.response>();

                baseResultDtoResponse = yeepay.UNFREEZE(unFreeze);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.UNFREEZE.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.UNFREEZE.response>();

                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }
            else if (rblPayType.SelectedValue == "34") //34 直接转账
            {
                MoneyCarCar.Models.YeePay.RequestModel.Direct_Transaction direct_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Direct_Transaction();

                direct_Transaction.platformUserNo = platformUserNo;
                direct_Transaction.requestNo = requestNo;

                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();

                //ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                //paydetail.amount = "100.00";
                //paydetail.targetPlatformUserNo = platformUserNo;
                //paydetail.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                //paydetail.bizType = direct_Transaction._bizType;
                //details.Add(paydetail);

                ToCpTransactionDetail paydetail2 = new ToCpTransactionDetail();
                paydetail2.amount = "10.00";
                paydetail2.targetPlatformUserNo = "60000";
                paydetail2.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail2.bizType = direct_Transaction._bizType;
                details.Add(paydetail2);

                direct_Transaction.details = details;

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response>();
                baseResultDtoResponse = yeepay.DIRECT_TRANSACTION(direct_Transaction);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response>();



                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }
            else if (rblPayType.SelectedValue == "35") //35 自动转账(授权) 自动还款
            {


                MoneyCarCar.Models.YeePay.RequestModel.Auto_Transaction auto_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Auto_Transaction();

                auto_Transaction.requestNo = requestNo;
                auto_Transaction.platformUserNo = "60002";
                auto_Transaction._userType = EnumUserType.MEMBER.ToEnumDesc(); //出款人用户类型
                auto_Transaction._bizType  = EnumBizType.REPAYMENT.ToEnumDesc();
                auto_Transaction.tenderOrderNo = "2";//标的号

                List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                paydetail.amount = "100.00";
                paydetail.targetPlatformUserNo = "60000";
                paydetail.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                paydetail.bizType = auto_Transaction._bizType;
                details.Add(paydetail);

                auto_Transaction.details = details;

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response>();
                baseResultDtoResponse = yeepay.AUTO_TRANSACTION(auto_Transaction);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                

                //MoneyCarCar.Models.YeePay.RequestModel.Auto_Transaction auto_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Auto_Transaction();
               
                //auto_Transaction.requestNo = requestNo;
                //auto_Transaction.platformUserNo = platformUserNo;

                //List<ToCpTransactionDetail> details = new List<ToCpTransactionDetail>();
                //ToCpTransactionDetail paydetail = new ToCpTransactionDetail();
                //paydetail.amount = "100.00";
                //paydetail.targetPlatformUserNo = "60000";
                //paydetail.targetUserType = EnumUserType.MEMBER.ToEnumDesc(); // 用户类型
                //paydetail.bizType = auto_Transaction._bizType;
                //details.Add(paydetail);

                //auto_Transaction.details = details;

                //BaseResultDto<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response>();
                //baseResultDtoResponse = yeepay.AUTO_TRANSACTION(auto_Transaction);

                //str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response>();

                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }
            else if (rblPayType.SelectedValue == "36") //36 单笔业务查询
            {
                MoneyCarCar.Models.YeePay.RequestModel.Query query = new MoneyCarCar.Models.YeePay.RequestModel.Query();

                // 转款记录 1
                // 提现记录 //WITHDRAW_RECORD = 2,
                // 充值记录 //RECHARGE_RECORD = 3,

                string strs = RadioButtonList1.SelectedValue;

                if (strs == "1")
                {
                    query.mode = EnumMode.CP_TRANSACTION.ToEnumDesc(); // 转款记录
                }
                else if (strs == "2")
                {
                    query.mode = EnumMode.WITHDRAW_RECORD.ToEnumDesc(); // 转款记录
                }
                else if (strs == "3")
                {
                    query.mode = EnumMode.RECHARGE_RECORD.ToEnumDesc(); // 转款记录
                }

                query.requestNo = requestNo;

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.QUERY.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.QUERY.response>();
                baseResultDtoResponse = yeepay.QUERY<MoneyCarCar.Models.YeePay.Response.QUERY.response>(query);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.QUERY.CP_TRANSACTION.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.QUERY.CP_TRANSACTION.response>();

                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }
            else if (rblPayType.SelectedValue == "37") //37 转账确认
            {
                //str = yeepay.COMPLETE_TRANSACTION();

                MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction complete_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction();

                // complete_Transaction.platformUserNo = "1233211234567";

                complete_Transaction.platformUserNo = platformUserNo;
                complete_Transaction.requestNo = requestNo;
                complete_Transaction.mode = EnumModeCOMPLETETRANSACTION.CONFIRM.ToString();

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response>();
                baseResultDtoResponse = yeepay.COMPLETE_TRANSACTION(complete_Transaction);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response>();

                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }
            else if (rblPayType.SelectedValue == "310") //310 对账
            {
                MoneyCarCar.Models.YeePay.RequestModel.Reconciliation reconciliation = new MoneyCarCar.Models.YeePay.RequestModel.Reconciliation();
                reconciliation.date = "2015-01-18";

                BaseResultDto<MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response>();
                baseResultDtoResponse = yeepay.RECONCILIATION(reconciliation);

                str = baseResultDtoResponse.ErrorMsg;//XML 数据

                #region 测试 反序列化

                MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response>();

                // 获取 XML 节点下的数据

                str = ""
               + "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
                 + " <response platformNo=\"platformNo\">"
                + " <code>1</code>"
                + " <description>操作成功</description>"
                 + "   <records>"
                 + "<record bizType=\"PAYMENT\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>"
                   + "<record bizType=\"REPAYMENT\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>"
                  + "<record bizType=\"WITHDRAW\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>"
                   + "<record bizType=\"RECHARGE\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>"
                   + " </records>"
                  + " </response>";

                List<Record> ListRecords = new List<Record>();

                Record record = null;

                XElement xmlRoot = XElement.Parse(str);

                dynamic dxml = new DynamicXml(str);

                foreach (var item in dxml.records[0].record)
                {
                     record = new Record();
                     record.bizType = item["bizType"];
                     record.fee = item["fee"];
                     record.balance = item["balance"];
                     record.amount = item["amount"];
                     record.time = item["time"];
                     record.requestNo = item["requestNo"];
                     record.platformNo = item["platformNo"];

                     ListRecords.Add(record);
                }

                //foreach (XElement xe in xmlRoot.Elements("records").Elements("record"))
                //{
                //    record = new Record();
                //    foreach (var item in xe.Attributes())
                //    {
                //        if (item.Name == "bizType")
                //            record.bizType = item.Value;
                //        if (item.Name == "fee")
                //            record.fee = item.Value;
                //        if (item.Name == "balance")
                //            record.balance = item.Value;
                //        if (item.Name == "amount")
                //            record.amount = item.Value;
                //        if (item.Name == "time")
                //            record.time = item.Value;
                //        if (item.Name == "requestNo")
                //            record.requestNo = item.Value;
                //        if (item.Name == "platformNo")
                //            record.platformNo = item.Value;
                //    }
                //    ListRecords.Add(record);
                //}

                _response.records = ListRecords;


                string strXML = ExpandHelper.Deserialize(_response); // 实体转 xml
                str += " : 反序列化 : " + strXML;

                #endregion
            }

            int typeValue = Int16.Parse(rblPayType.SelectedValue);

            if ((typeValue > 30 && typeValue < 40) || typeValue == 310)
            {
                txtReturnValue.Text = str;
            }
            else
            {

                //  RecordLog("str", "str:" + str, true);

                Response.Write(str);
            }
        }

        /// <summary>
        /// Notify 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(platformUserNo))
            {
                Response.Write("<script>alert('请输入唯一商户号！')</script>");
                return;
            }

            if (string.IsNullOrEmpty(TextBox2.Text.Trim()))
            {
            }
            else
            {
                requestNo = TextBox2.Text.Trim();
            }

            string str = "";

            if (rblPayType.SelectedValue == "21") //21 注册   o  
            {
                SystemUsersOper systemUsersOper = new SystemUsersOper();
                str = systemUsersOper.UpdateById("djy", 1) ? "注册成功" : "注册失败";
            }
            else if (rblPayType.SelectedValue == "22") //22 充值  o
            {
                BaseHelper baseHelper = new BaseHelper();
                SqlParameter[] parameters = {
                       new SqlParameter("@Uid", SqlDbType.Int),
                       new SqlParameter("@PayNo", SqlDbType.VarChar,50)
                            };
                parameters[0].Value = 5;
                parameters[1].Value = 00007;
                // exec [pro_Recharge] 5,'0004','0004'，'icbc'
                try
                {
                    str = baseHelper.sqlhelper.ExecByProc("pro_Recharge", parameters) + "";
                }
                catch (Exception)
                {
                    str = "catch";
                }
            }
            else if (rblPayType.SelectedValue == "23") //23 提现      o
            {
                BaseHelper baseHelper = new BaseHelper();
                SqlParameter[] parameters = {
                       new SqlParameter("@Uid", SqlDbType.Int),
                       new SqlParameter("@PayNo", SqlDbType.VarChar,50)
                            };
                parameters[0].Value = 5;
                parameters[1].Value = 00007;
                // exec [pro_Withdraw] 5,'00007'
                try
                {
                    str = baseHelper.sqlhelper.ExecByProc("pro_Withdraw", parameters) + "";
                }
                catch (Exception)
                {
                    str = "catch";
                }
            }
            else if (rblPayType.SelectedValue == "24")  //24 绑卡      o
            {
                SystemBankCardOper systemBankCardOper = new SystemBankCardOper();

                SystemBankCard systemBankCard = new SystemBankCard();

                systemBankCard.BankCardNumber = "123456789";
                systemBankCard.OpenAnAccountBankCard = "中国银行";
                systemBankCard.UserId = int.Parse("12");
                systemBankCard.IsDefault = true;

                str = systemBankCardOper.Add(systemBankCard) == 1 ? "绑卡成功" : "绑卡失败";
            }
            else if (rblPayType.SelectedValue == "25")//25 取消绑卡  o
            {
                // 同步返回处理
                SystemBankCardOper systemBankCardOper = new SystemBankCardOper();

                string UserId = "12";
                str = systemBankCardOper.UpdateByUserId(UserId) ? "取消绑卡成功" : "取消绑卡失败";

            }
            else if (rblPayType.SelectedValue == "26")  //26 企业用户注册 o
            {

            }
            else if (rblPayType.SelectedValue == "271")   //27 
            { 
            
            }
            else if (rblPayType.SelectedValue == "272")   //27 
            {

                BaseHelper baseHelper = new BaseHelper();
                // 调用存储过程 ：业务处理

                string errorMsg = "";

                string splatformUserNo = platformUserNo;
                string srequestNo = requestNo;

                YeePayOper yeePayOper = new YeePayOper();
                bool reuslt = yeePayOper.ToCpTransaction(srequestNo, out errorMsg);

                if (reuslt)
                {

                    MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction complete_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction();

                    complete_Transaction.platformUserNo = platformUserNo;
                    complete_Transaction.requestNo = srequestNo;
                    complete_Transaction.mode = EnumModeCOMPLETETRANSACTION.CONFIRM.ToString();

                    BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response>();
                    YeePay yeepay = new YeePay();
                    baseResultDtoResponse = yeepay.COMPLETE_TRANSACTION(complete_Transaction);
                    string strs = baseResultDtoResponse.ErrorMsg;//XML 数据
                }

            }
            else if (rblPayType.SelectedValue == "273")   //27 
            {

            }
            else if (rblPayType.SelectedValue == "274")   //27 
            {

            }
            else if (rblPayType.SelectedValue == "28")   //28 自动投标授权 o
            {

            }
            else if (rblPayType.SelectedValue == "29")  //29 自动还款授权  o
            {

            }
            else if (rblPayType.SelectedValue == "31") //31 账户查询 o
            {

            }
            else if (rblPayType.SelectedValue == "32")  //32 资金冻结 o
            {

            }
            else if (rblPayType.SelectedValue == "33") //33 资金解冻 o
            {

            }
            else if (rblPayType.SelectedValue == "34") //34 直接转账
            {

            }
            else if (rblPayType.SelectedValue == "35") //35 自动转账授权
            {

            }
            else if (rblPayType.SelectedValue == "36") //36 单笔业务查询
            {

            }
            else if (rblPayType.SelectedValue == "37") //37 转账确认
            {

            }

            txtReturnValue.Text = str;
        }

        BaseResultDto<PostBaseYeePayPar> baseResultDto = new BaseResultDto<PostBaseYeePayPar>();
        PostBaseYeePayPar postBaseYeePayPar = new PostBaseYeePayPar();
        YeePay yeepay = new YeePay();

        // private string platformUserNo = "1233211234567";

        // private string platformUserNo = "1233211234567";

        private string platformUserNo;
        private string requestNo = DateTime.Now.Ticks.ToString();

        /// <summary>
        /// 组合表单数据
        /// </summary>
        /// <param name="service"></param>
        /// <param name="req"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private string ServiceFrom(string service, string req, string sign)
        {
            string serviceStr = "";

            try
            {
                serviceStr = appendParam(serviceStr, "service", service);
                serviceStr = appendParam(serviceStr, "req", req);
                serviceStr = appendParam(serviceStr, "sign", sign);
            }
            catch (Exception ex)
            {

            }
            return serviceStr;
        }

        /// <summary>
        /// 组合表单数据
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="req"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private string ActionFrom(string actionUrl, string req, string sign)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                #region 组合
                sb.Append("<form name=\"yeepaysubmit\" method=\"post\" action=\"" + actionUrl + "\">");

                sb.Append("<input type=\"hidden\" id=\"req\"  name=\"req\" value=\"\">");
                sb.Append("<input type=\"hidden\" id=\"sign\"  name=\"sign\" value=\"" + sign + "\">");

                sb.Append("</form>");
                sb.Append("<script>");

                //sb.Append("document.getElementById(\"req\").value=escape('" + req + "');"); // 转码
                sb.Append("document.getElementById(\"req\").value='" + req + "';");

                sb.Append("document.yeepaysubmit.submit();");
                sb.Append("</script>");

                #endregion
            }
            catch (Exception ex)
            {

            }
            return sb.ToString();
        }


        /// <summary>
        /// PostXML数据到服务器及获取返回的xml值
        /// </summary>
        public string HttpPost(string url, string data)
        {
            string res = "";
            string postData = data;	//xml数据
            string Web = url;	//网关地址

            try
            {
                //将数据提交到快钱服务器
                WebRequest myWebRequest = WebRequest.Create(url);
                myWebRequest.Method = "POST";
                myWebRequest.ContentType = "application/x-www-form-urlencoded";
                Stream streamReq = myWebRequest.GetRequestStream();
                byte[] byteArray = Encoding.GetEncoding("utf-8").GetBytes(postData);
                streamReq.Write(byteArray, 0, byteArray.Length);
                streamReq.Close();

                //获取服务器返回的XML数据
                WebResponse myWebResponse = myWebRequest.GetResponse();
                StreamReader sr = new StreamReader(myWebResponse.GetResponseStream());
                res = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {

            }
            return res; //返回数据
        }

        /// <summary>
        /// 构造参数字符串
        /// </summary>
        /// <param name="returnStr">源字符串</param>
        /// <param name="paramId">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <returns></returns>
        public String appendParam(String returnStr, String paramId, String paramValue)
        {
            try
            {
                if (returnStr != "")
                {
                    if (paramValue != "")
                        returnStr += "&" + paramId + "=" + paramValue;
                }
                else
                {
                    if (paramValue != "")
                        returnStr = paramId + "=" + paramValue;
                }

            }
            catch (Exception ex)
            {

            }

            return returnStr;
        }

        private static object RootLock = new object();

        /// <summary>
        /// 记录日志(记事本)
        /// </summary>
        /// <param name="PageName">页面名</param>
        /// <param name="logContents">日志内容</param>
        /// <param name="bRecordRequest">是否记录参数</param>
        public static void RecordLog(string PageName, string logContents, bool bRecordRequest)
        {
            lock (RootLock)
            {
                StreamWriter fs = null;
                StringBuilder sb = new StringBuilder();
                try
                {
                    #region 记录文本日志

                    sb.AppendFormat("记录时间：" + DateTime.Now.ToString() + "\r\n");
                    sb.AppendFormat("内    容: " + logContents + "\r\n");

                    if (HttpContext.Current != null && HttpContext.Current.Request != null)
                    {
                        sb.AppendFormat("      IP：" + System.Web.HttpContext.Current.Request.UserHostAddress + "\r\n");
                        sb.AppendFormat("  Request.HttpMethod:" + HttpContext.Current.Request.HttpMethod + "\r\n");

                        if (bRecordRequest)
                        {
                            #region 记录 Request 参数
                            try
                            {

                                if (HttpContext.Current.Request.HttpMethod == "POST")
                                {
                                    #region POST 提交
                                    if (HttpContext.Current.Request.Form.Count != 0)
                                    {
                                        //__VIEWSTATE
                                        //__EVENTVALIDATION 
                                        System.Collections.Specialized.NameValueCollection nv = HttpContext.Current.Request.Form;
                                        if (nv != null && nv.Keys.Count > 0)
                                        {
                                            foreach (string key in nv.Keys)
                                            {
                                                if (key == "__VIEWSTATE" || key == "__EVENTVALIDATION")
                                                {
                                                    continue;
                                                }
                                                sb.AppendFormat("{0} ={1} \r\n", key, (nv[key] != null ? nv[key].ToString() : ""));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sb.AppendFormat(" HttpContext.Current.Request.Form.Count = 0 \r\n");
                                    }

                                    #endregion
                                }
                                else if (HttpContext.Current.Request.HttpMethod == "GET")
                                {
                                    #region GET 提交

                                    if (HttpContext.Current.Request.QueryString.Count != 0)
                                    {
                                        System.Collections.Specialized.NameValueCollection nv = HttpContext.Current.Request.QueryString;
                                        if (nv != null && nv.Keys.Count > 0)
                                        {
                                            foreach (string key in nv.Keys)
                                            {
                                                sb.AppendFormat("{0}={1} \r\n", key, nv[key]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sb.AppendFormat(" HttpContext.Current.QueryString.Form.Count = 0 \r\n");
                                    }

                                    #endregion
                                }
                                else
                                {

                                }

                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("  异常内容: " + ex + "\r\n");
                                sb.AppendFormat("----------------------------------------------------------------------------------------------------\r\n\r\n");
                                AgainWrite(sb, PageName);
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        sb.AppendFormat("  HttpContext.Current.Request=null \r\n");
                    }

                    sb.AppendFormat("----------------------------------------------------------------------------------------------------\r\n\r\n");

                    string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Logs\\" + PageName + "\\";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    fs = new StreamWriter(dir + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true, System.Text.Encoding.Default);
                    fs.WriteLine(sb.ToString());

                    #endregion
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("catch(Exception ex): " + ex.ToString() + "\r\n");
                    AgainWrite(sb, PageName);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 再次记录
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="PageName"></param>
        private static void AgainWrite(StringBuilder sb, string PageName)
        {
            StreamWriter fs = null;
            try
            {
                string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Logs\\" + PageName + "\\";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                fs = new StreamWriter(dir + System.DateTime.Now.ToString("yyyy-MM-dd") + "again" + ".txt", true, System.Text.Encoding.Default);
                fs.Write(sb.ToString());
            }
            catch (Exception)
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }
}