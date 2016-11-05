using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;


namespace MoneyCarCar.Models.YeePay
{
    /// <summary>
    /// 基础配置文件
    /// </summary>
    public class YeePayConfig
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public YeePayConfig()
        {
            #region YeePayConfig 默认构造函数

            XmlDocument xmldoc = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\YeePayConfig.xml";
            xmldoc.Load(path);
            XElement xmlRoot = XElement.Parse(xmldoc.InnerXml);

            foreach (XElement xe in xmlRoot.Elements("payConfig"))
            {
                if (xe.Element("platformNo") != null)
                    _platformNo = xe.Element("platformNo").Value;
                if (xe.Element("actionUrl") != null)
                    _actionUrl = xe.Element("actionUrl").Value;
                if (xe.Element("serviceUrl") != null)
                    _serviceUrl = xe.Element("serviceUrl").Value;
                if (xe.Element("input_charset") != null)
                    _input_charset = xe.Element("input_charset").Value;
                if (xe.Element("serviceUrl") != null)
                    _serviceUrl = xe.Element("serviceUrl").Value;
                if (xe.Element("notifyUrl") != null)
                    _notifyUrl = xe.Element("notifyUrl").Value;
                if (xe.Element("signUrl") != null)
                    _signUrl = xe.Element("signUrl").Value;
                if (xe.Element("verifyUrl") != null)
                    _verifyUrl = xe.Element("verifyUrl").Value;
                if (xe.Element("key") != null)
                    _key = xe.Element("key").Value;
            }

            CallbackUrl callbacks = new CallbackUrl(_callbackUrl);

            foreach (XElement xe in xmlRoot.Elements("callbackUrls"))
            {
                if (xe.Element("toRegister") != null)
                    callbacks.toRegister = xe.Element("toRegister").Value;
                if (xe.Element("toRecharge") != null)
                    callbacks.toRecharge = xe.Element("toRecharge").Value;
                if (xe.Element("toWithdraw") != null)
                    callbacks.toWithdraw = xe.Element("toWithdraw").Value;
                if (xe.Element("toBindBankCard") != null)
                    callbacks.toBindBankCard = xe.Element("toBindBankCard").Value;
                if (xe.Element("toUnbindBankCard") != null)
                    callbacks.toUnbindBankCard = xe.Element("toUnbindBankCard").Value;
                if (xe.Element("toEnterpriseRegister") != null)
                    callbacks.toEnterpriseRegister = xe.Element("toEnterpriseRegister").Value;
                if (xe.Element("toCpTransaction_TRANSFER") != null)
                    callbacks.toCpTransaction_TRANSFER = xe.Element("toCpTransaction_TRANSFER").Value;
                if (xe.Element("toCpTransaction_TENDER") != null)
                    callbacks.toCpTransaction_TENDER = xe.Element("toCpTransaction_TENDER").Value;
                if (xe.Element("toCpTransaction_REPAYMENT") != null)
                    callbacks.toCpTransaction_REPAYMENT = xe.Element("toCpTransaction_REPAYMENT").Value;
                if (xe.Element("toCpTransaction_CREDIT_ASSIGNMENT") != null)
                    callbacks.toCpTransaction_CREDIT_ASSIGNMENT = xe.Element("toCpTransaction_CREDIT_ASSIGNMENT").Value;
                if (xe.Element("toAuthorizeAutoTransfer") != null)
                    callbacks.toAuthorizeAutoTransfer = xe.Element("toAuthorizeAutoTransfer").Value;
                if (xe.Element("toAuthorizeAutoRepayment") != null)
                    callbacks.toAuthorizeAutoRepayment = xe.Element("toAuthorizeAutoRepayment").Value;
                if (xe.Element("account_info") != null)
                    callbacks.account_info = xe.Element("account_info").Value;
                if (xe.Element("freeze") != null)
                    callbacks.freeze = xe.Element("freeze").Value;
                if (xe.Element("unFreeze") != null)
                    callbacks.unFreeze = xe.Element("unFreeze").Value;
                if (xe.Element("direct_Transaction") != null)
                    callbacks.direct_Transaction = xe.Element("direct_Transaction").Value;
                if (xe.Element("direct_Transaction") != null)
                    callbacks.direct_Transaction = xe.Element("direct_Transaction").Value;
                if (xe.Element("auto_Transaction") != null)
                    callbacks.auto_Transaction = xe.Element("auto_Transaction").Value;
                if (xe.Element("query") != null)
                    callbacks.query = xe.Element("query").Value;
                if (xe.Element("complete_Transaction") != null)
                    callbacks.complete_Transaction = xe.Element("complete_Transaction").Value;
            }
            callbackUrls = callbacks;



            #endregion

            _userType = EnumUserType.MEMBER.ToEnumDesc();
        }

        /// <summary>
        /// Y 签名url
        /// </summary>
        public string _signUrl { get; set; }

        /// <summary>
        /// Y 验证签名url
        /// </summary>
        public string _verifyUrl { get; set; }

        /// <summary>
        /// Y key
        /// </summary>
        public string _key { get; set; }

        /// <summary>
        /// Y 商户平台会员标识,会员在商户平台唯一标识
        /// </summary>
        public string platformUserNo { get; set; }
        /// <summary>
        /// Y 请求流水号
        /// </summary>
        public string requestNo { get; set; }

        /// <summary>
        /// 同步请求地址集合
        /// </summary>
        public CallbackUrl callbackUrls { get; set; }

        /// <summary>
        /// Y 商户编号,商户在易宝唯一标识
        /// </summary>
        public string _platformNo { get; set; }
        /// <summary>
        /// 网关接口
        /// </summary>
        public string _actionUrl { get; set; }
        /// <summary>
        /// 直接接口
        /// </summary>
        public string _serviceUrl { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string _input_charset { get; set; }
        /// <summary>
        ///  N 同步请求：页面回跳URL
        /// </summary>
        public string _callbackUrl { get; set; }
        /// <summary>
        /// 异步请求：服务器通知URL
        /// </summary>
        public string _notifyUrl { get; set; }
        /// <summary>
        ///  Y 根据业务的不同，需要传入不同的值，见【业务类型】： TENDER 投标、REPAYMENT 还款、CREDIT_ASSIGNMENT 债权转让、TRANSFER 转账、COMMISSION 分润，仅在资金转账明细中使用
        /// </summary>
        public string _bizType { get; set; }
        /// <summary>
        /// Y 用户类型 ：MEMBER 个人会员、MERCHANT 商户, 默认：MEMBER
        /// </summary>
        public string _userType { get; set; }
    }
}