using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.YeePay;
using MoneyCarCar.Models.YeePay.RequestModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using MoneyCarCar.Commons;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;



namespace MoneyCarCar.DAL
{
    /// <summary>
    /// V2.0 版本
    /// </summary>
    public class YeePay
    {
        BaseResultDto<PostBaseYeePayPar> baseResultDto = new BaseResultDto<PostBaseYeePayPar>();
        PostBaseYeePayPar postBaseYeePayPar = new PostBaseYeePayPar();
        DataFornat dataFornat = new DataFornat();

        #region 2.网关接口

        /// <summary>
        /// 2.1 注册（测试）: 在易宝托管账户平台注册新用户 1.商户平台会员标识若重复，认为是该用户重新申请注册，最后注册信息以最后一次注册为准，若实名信息相同直接返回已注册成功 2.注册流程含有实名认证流程
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToRegister(ToRegister toRegister)
        {
            try
            {
                #region 参数
                string Action = EnumActionRequest.toRegister.ToEnumDesc();
                string actionUrl = toRegister._actionUrl + Action;

                #region 组合XML

                StringBuilder sbxml = new StringBuilder();
                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toRegister._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toRegister.platformUserNo + "</platformUserNo>");//Y 商户平台会员标识,会员在商户平台唯一标识
                sbxml.Append("<nickName>" + toRegister.nickName + "</nickName>");//N 昵称,交易查询时希望显示的会员名称，若不写则与会员标识一致
                sbxml.Append("<realName>" + toRegister.realName + "</realName>");// Y会员真实姓名,会员真实姓名
                sbxml.Append("<idCardType>" + toRegister._idCardType + "</idCardType>");// Y身份证类型,【见身份证类型】
                sbxml.Append("<idCardNo>" + toRegister.idCardNo + "</idCardNo>");//Y 身份证号,会员身份证号
                sbxml.Append("<mobile>" + toRegister.mobile + "</mobile>");//Y 手机号,接收短信验证码的手机号
                sbxml.Append("<email>" + toRegister.email + "</email>"); // Y 邮箱，邮箱
                sbxml.Append("<notifyUrl>" + toRegister._notifyUrl + "</notifyUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<callbackUrl>" + toRegister.callbackUrls.toRegister + "</callbackUrl>");//Y 服务器通知URL，服务器通知URL
                sbxml.Append("</request>");

                #endregion

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toRegister._signUrl, "req=" + sbxml.ToString());

                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                //<request platformNo="10040011137">
                //<platformUserNo>10040011137</platformUserNo><nickName>chenxing</nickName>
                //<realName>gaofushuai</realName>
                //<idCardType>G2_IDCARD</idCardType>
                //<idCardNo>10040011137</idCardNo>
                //<mobile>18688888888</mobile>
                //<email>test@hotmail.com</email>
                //<notifyUrl>http://www.baidu.com</notifyUrl>
                //<callbackUrl>http://www.baidu.com</callbackUrl>
                //</request>

                #endregion

                //接口输出（注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息


                // 回调通知（注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string bizType = "";// N 业务名称,固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//Y 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号

                RequestLog("toRegister(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("toRegister(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.2 充值
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToRecharge(ToRecharge toRecharge)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toRecharge.ToEnumDesc();
                string actionUrl = toRecharge._actionUrl + Action;

                #region 组合XML

                StringBuilder sbxml = new StringBuilder();
                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toRecharge._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toRecharge.platformUserNo + "</platformUserNo>");
                sbxml.Append("<requestNo>" + toRecharge.requestNo + "</requestNo>");
                sbxml.Append("<amount>" + toRecharge._amount + "</amount>");
                sbxml.Append("<feeMode>" + toRecharge._feeMode + "</feeMode>");
                sbxml.Append("<callbackUrl>" + toRecharge.callbackUrls.toRecharge + "</callbackUrl>");
                sbxml.Append("<notifyUrl>" + toRecharge._notifyUrl + "</notifyUrl>");
                sbxml.Append("</request>");

                #endregion

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toRecharge._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                RequestLog("toRecharge(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("toRecharge(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.3 提现: 将用户的账户余额提现至绑定的银行卡。用户当天冲的值，当天不计入可提现金额。
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToWithdraw(ToWithdraw toWithdraw)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toWithdraw.ToEnumDesc();
                string actionUrl = toWithdraw._actionUrl + Action;

                #region 组合XML

                StringBuilder sbxml = new StringBuilder();
                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toWithdraw._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toWithdraw.platformUserNo + "</platformUserNo>");
                sbxml.Append("<requestNo>" + toWithdraw.requestNo + "</requestNo>");
                sbxml.Append("<feeMode>" + toWithdraw._feeMode + "</feeMode>");
                sbxml.Append("<callbackUrl>" + toWithdraw.callbackUrls.toWithdraw + "</callbackUrl>");
                sbxml.Append("<notifyUrl>" + toWithdraw._notifyUrl + "</notifyUrl>");
                sbxml.Append("<amount>" + toWithdraw._amount + "</amount>");
                sbxml.Append("</request>");

                #endregion

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toWithdraw._signUrl, "req=" + sbxml.ToString());

                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（提现）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string service = "";//Y 服务名称，固定值WITHDRAW
                //string requestNo = "";// N 请求流水号,注册不传入请求流水号，返回无流水号
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息


                // 回调通知（提现）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值WITHDRAW
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string requestNo = "";// Y 请求流水号
                //string platformUserNo = "";//Y 平台的用户编号,用户编号
                //string cardNo = "";//Y 卡号,绑定的卡号
                //string bank = "";//Y 卡的开户行,【见银行代码】

                RequestLog("ToWithdraw(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToWithdraw(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.4 绑卡: 在资金托管平台提现前,须进行绑卡寿仔卡会进行实名认证只能绑定用户本人的卡。实名认证需要较长时间，因此此接口返回成功只代表绑卡受理成功，不代表绑卡认证成功。
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToBindBankCard(ToBindBankCard toBindBankCard)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toBindBankCard.ToEnumDesc();
                string actionUrl = toBindBankCard._actionUrl + Action;

                #region 组合XML

                StringBuilder sbxml = new StringBuilder();
                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toBindBankCard._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toBindBankCard.platformUserNo + "</platformUserNo>");//Y 商户编号,商户在易宝唯一标识
                sbxml.Append("<requestNo>" + toBindBankCard.requestNo + "</requestNo>");///Y 请求流水号
                sbxml.Append("<callbackUrl>" + toBindBankCard.callbackUrls.toBindBankCard + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toBindBankCard._notifyUrl + "</notifyUrl>");//Y 服务器通知URL，服务器通知URL
                sbxml.Append("</request>");


                #endregion

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toBindBankCard._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（绑卡）
                //string requestNo = "";// N 请求流水号
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string service = "";//Y 服务名称，固定值 BIND_BANK_CARD
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                // 回调通知（绑卡）
                //string requestNo = "";// Y 请求流水号
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值BIND_BANK_CARD
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号,用户编号
                //string bankCardNo = "";//Y 卡号,绑定的卡号
                //string cardStatus = "";//Y 卡的状态,【见绑卡状态】
                //string bank = "";//Y 卡的开户行,【见银行代码】

                RequestLog("ToBindBankCard(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToBindBankCard(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.5 取消绑卡
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToUnbindBankCard(ToUnbindBankCard toUnbindBankCard)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toUnbindBankCard.ToEnumDesc();
                string actionUrl = toUnbindBankCard._actionUrl + Action;

                #region 组合XML

                StringBuilder sbxml = new StringBuilder();
                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toUnbindBankCard._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toUnbindBankCard.platformUserNo + "</platformUserNo>");
                sbxml.Append("<requestNo>" + toUnbindBankCard.requestNo + "</requestNo>");
                sbxml.Append("<callbackUrl>" + toUnbindBankCard.callbackUrls.toUnbindBankCard + "</callbackUrl>");
                //sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
                sbxml.Append("</request>");

                #endregion

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toUnbindBankCard._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                ////接口输出（取消绑卡）  

                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// N 请求流水号
                //string service = "";//Y 服务名称，固定值 UNBIND_BANK_CARD
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                RequestLog("ToUnbindBankCard(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToUnbindBankCard(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.6 企业用户注册
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToEnterpriseRegister(ToEnterpriseRegister toEnterpriseRegister)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toEnterpriseRegister.ToEnumDesc();
                string actionUrl = toEnterpriseRegister._actionUrl + Action;

                #region 组合XML

                StringBuilder sbxml = new StringBuilder();
                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toEnterpriseRegister._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toEnterpriseRegister.platformUserNo + "</platformUserNo>");//Y 平台用户编号,用户编号
                sbxml.Append("<requestNo>" + toEnterpriseRegister.requestNo + "</requestNo>");//Y 请求流水号,

                sbxml.Append("<enterpriseName>" + toEnterpriseRegister.enterpriseName + "</enterpriseName>");//Y 企业名称，企业名称
                sbxml.Append("<bankLicense>" + toEnterpriseRegister.bankLicense + "</bankLicense>");// Y 开户银行许可证
                sbxml.Append("<orgNo>" + toEnterpriseRegister.orgNo + "</orgNo>");// Y 组织机构代码
                sbxml.Append("<businessLicense>" + toEnterpriseRegister.businessLicense + "</businessLicense>");//Y 营业执照编号
                sbxml.Append("<taxNo>" + toEnterpriseRegister.taxNo + "</taxNo>");//Y 税务登记号
                sbxml.Append("<legal>" + toEnterpriseRegister.legal + "</legal>");//Y 法人姓名
                sbxml.Append("<legalIdNo>" + toEnterpriseRegister.legalIdNo + "</legalIdNo>");//Y 法人身份证号
                sbxml.Append("<contact>" + toEnterpriseRegister.contact + "</contact>");//Y 企业联系人
                sbxml.Append("<contactPhone>" + toEnterpriseRegister.contactPhone + "</contactPhone>");//Y 联系人手机号
                sbxml.Append("<email>" + toEnterpriseRegister.email + "</email>");//Y 联系人邮箱
                sbxml.Append("<memberClassType>" + toEnterpriseRegister._memberClassType + "</memberClassType>");//Y 会员类型, ENTERPRISE：企业借款人 、 GUARANTEE_CORP：担保公司

                sbxml.Append("<callbackUrl>" + toEnterpriseRegister.callbackUrls.toEnterpriseRegister + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toEnterpriseRegister._notifyUrl + "</notifyUrl>");//Y 服务器通知URL
                sbxml.Append("</request>");

                #endregion

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toEnterpriseRegister._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                // 回调通知（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号


                RequestLog("ToEnterpriseRegister(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToEnterpriseRegister(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        #region 转账授权

        /// <summary>
        /// 2.7  （1）转账［TRANSFER］
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToCpTransaction_TRANSFER(ToCpTransaction_TRANSFER toCpTransaction)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toCpTransaction.ToEnumDesc();
                string actionUrl = toCpTransaction._actionUrl + Action;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toCpTransaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toCpTransaction.platformUserNo + "</platformUserNo>");//Y 平台用户编号,用户编号
                sbxml.Append("<requestNo>" + toCpTransaction.requestNo + "</requestNo>");//Y 请求流水号,
                sbxml.Append("<userType>" + toCpTransaction._userType + "</userType>");// Y出款人用户类型，目前只支持传入 MEMBER
                sbxml.Append("<bizType>" + toCpTransaction._bizType + "</bizType>");//Y 根据业务的不同，需要传入不同的值，见【业务类型】。并参考下面的详细信息
                sbxml.Append("<expired>" + toCpTransaction._expired + "</expired>");//Y 超过此时间即不允许提交订单
                sbxml.Append("<details>");//Y 资金明细记录
                foreach (var item in toCpTransaction.details)
                {
                    sbxml.Append("<detail>");//Y 资金明细记录
                    sbxml.Append("<amount>" + item.amount + "</amount>");//Y 转入金额
                    sbxml.Append("<targetUserType>" + item.targetUserType + "</targetUserType>");//用户类型, 见【用户类型】
                    sbxml.Append("<targetPlatformUserNo>" + item.targetPlatformUserNo + "</targetPlatformUserNo>");//Y 平台用户编号(转入用户编号)
                    sbxml.Append("<bizType>" + item.bizType + "</bizType>");//Y bizType 资金明细业务类型。根据业务的不同，需要传入不同的值，见【业务类型】
                    sbxml.Append("</detail>");
                }
                sbxml.Append("</details>");
                sbxml.Append("<callbackUrl>" + toCpTransaction.callbackUrls.toCpTransaction_TRANSFER + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toCpTransaction._notifyUrl + "</notifyUrl>");//Y 服务器通知URL
                sbxml.Append("</request>");

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toCpTransaction._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息


                // 回调通知（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号

                RequestLog("ToCpTransaction_TRANSFER(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToCpTransaction_TRANSFER(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.7  （2）投标［TENDER］
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToCpTransaction_TENDER(ToCpTransaction_TENDER toCpTransaction)
        {
            try
            {

                #region 参数

                string Action = EnumActionRequest.toCpTransaction.ToEnumDesc();
                string actionUrl = toCpTransaction._actionUrl + Action;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toCpTransaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toCpTransaction.platformUserNo + "</platformUserNo>");//Y 平台用户编号,用户编号
                sbxml.Append("<requestNo>" + toCpTransaction.requestNo + "</requestNo>");//Y 请求流水号,
                sbxml.Append("<userType>" + toCpTransaction._userType + "</userType>");// Y出款人用户类型，目前只支持传入 MEMBER
                sbxml.Append("<bizType>" + toCpTransaction._bizType + "</bizType>");//Y 根据业务的不同，需要传入不同的值，见【业务类型】。并参考下面的详细信息
                sbxml.Append("<details>");//Y 资金明细记录
                foreach (var item in toCpTransaction.details)
                {
                    sbxml.Append("<detail>");//Y 资金明细记录
                    sbxml.Append("<amount>" + item.amount + "</amount>");//Y 转入金额
                    sbxml.Append("<targetUserType>" + item.targetUserType + "</targetUserType>");//用户类型, 见【用户类型】
                    sbxml.Append("<targetPlatformUserNo>" + item.targetPlatformUserNo + "</targetPlatformUserNo>");//Y 平台用户编号(转入用户编号)
                    sbxml.Append("<bizType>" + item.bizType + "</bizType>");//Y bizType 资金明细业务类型。根据业务的不同，需要传入不同的值，见【业务类型】
                    sbxml.Append("</detail>");
                }
                sbxml.Append("</details>");
                sbxml.Append("<extend>");
                sbxml.Append("<property name=\"tenderOrderNo\" value=\"" + toCpTransaction.tenderOrderNo + "\" />");
                sbxml.Append("<property name=\"tenderName\" value=\"" + toCpTransaction.tenderName + "\" />");
                sbxml.Append("<property name=\"tenderAmount\" value=\"" + toCpTransaction.tenderAmount + "\" />");
                sbxml.Append("<property name=\"tenderDescription\" value=\"" + toCpTransaction.tenderDescription + "\" />");
                sbxml.Append("<property name=\"borrowerPlatformUserNo\" value=\"" + toCpTransaction.borrowerPlatformUserNo + "\" />");
                sbxml.Append("</extend>");
                sbxml.Append("<callbackUrl>" + toCpTransaction.callbackUrls.toCpTransaction_TENDER + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toCpTransaction._notifyUrl + "</notifyUrl>");//Y 服务器通知URL
                sbxml.Append("</request>");

                postBaseYeePayPar.req = sbxml.ToString();

                postBaseYeePayPar.sign = HttpPost(toCpTransaction._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                // 回调通知（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号

                RequestLog("ToCpTransaction_TENDER(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToCpTransaction_TENDER(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.7 （3）还款［REPAYMENT］
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToCpTransaction_REPAYMENT(ToCpTransaction_REPAYMENT toCpTransaction)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toCpTransaction.ToEnumDesc();
                string actionUrl = toCpTransaction._actionUrl + Action;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toCpTransaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toCpTransaction.platformUserNo + "</platformUserNo>");//Y 平台用户编号,用户编号
                sbxml.Append("<requestNo>" + toCpTransaction.requestNo + "</requestNo>");//Y 请求流水号,
                sbxml.Append("<userType>" + toCpTransaction._userType + "</userType>");// Y出款人用户类型，目前只支持传入 MEMBER
                sbxml.Append("<bizType>" + toCpTransaction._bizType + "</bizType>");//Y 根据业务的不同，需要传入不同的值，见【业务类型】。并参考下面的详细信息
                sbxml.Append("<details>");//Y 资金明细记录
                foreach (var item in toCpTransaction.details)
                {
                    sbxml.Append("<detail>");//Y 资金明细记录
                    sbxml.Append("<amount>" + item.amount + "</amount>");//Y 转入金额
                    sbxml.Append("<targetUserType>" + item.targetUserType + "</targetUserType>");//用户类型, 见【用户类型】
                    sbxml.Append("<targetPlatformUserNo>" + item.targetPlatformUserNo + "</targetPlatformUserNo>");//Y 平台用户编号(转入用户编号)
                    sbxml.Append("<bizType>" + item.bizType + "</bizType>");//Y bizType 资金明细业务类型。根据业务的不同，需要传入不同的值，见【业务类型】
                    sbxml.Append("</detail>");
                }

                sbxml.Append("</details>");
                sbxml.Append("<extend>");
                sbxml.Append("<property name=\"tenderOrderNo\" value=\"" + toCpTransaction.tenderOrderNo + "\" />");
                sbxml.Append("</extend>");
                sbxml.Append("<callbackUrl>" + toCpTransaction.callbackUrls.toCpTransaction_REPAYMENT + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toCpTransaction._notifyUrl + "</notifyUrl>");//Y 服务器通知URL
                sbxml.Append("</request>");

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toCpTransaction._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                // 回调通知（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号

                RequestLog("ToCpTransaction_REPAYMENT(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToCpTransaction_REPAYMENT(request)(catch)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        /// 2.7  （4）债权转让［CREDIT_ASSIGNMENT］
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToCpTransaction_CREDIT_ASSIGNMENT(ToCpTransaction_CREDIT_ASSIGNMENT toCpTransaction)
        {
            try
            {

                #region 参数

                string Action = EnumActionRequest.toCpTransaction.ToEnumDesc();
                string actionUrl = toCpTransaction._actionUrl + Action;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toCpTransaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toCpTransaction.platformUserNo + "</platformUserNo>");//Y 平台用户编号,用户编号
                sbxml.Append("<requestNo>" + toCpTransaction.requestNo + "</requestNo>");//Y 请求流水号,
                sbxml.Append("<userType>" + toCpTransaction._userType + "</userType>");// Y 出款人用户类型，目前只支持传入 MEMBER
                sbxml.Append("<bizType>" + toCpTransaction._bizType + "</bizType>");//Y 根据业务的不同，需要传入不同的值，见【业务类型】。并参考下面的详细信息
                sbxml.Append("<details>");//Y 资金明细记录
                foreach (var item in toCpTransaction.details)
                {
                    sbxml.Append("<detail>");//Y 资金明细记录
                    sbxml.Append("<amount>" + item.amount + "</amount>");//Y 转入金额
                    sbxml.Append("<targetUserType>" + item.targetUserType + "</targetUserType>");//用户类型, 见【用户类型】
                    sbxml.Append("<targetPlatformUserNo>" + item.targetPlatformUserNo + "</targetPlatformUserNo>");//Y 平台用户编号(转入用户编号)
                    sbxml.Append("<bizType>" + item.bizType + "</bizType>");//Y bizType 资金明细业务类型。根据业务的不同，需要传入不同的值，见【业务类型】
                    sbxml.Append("</detail>");
                }
                sbxml.Append("</details>");
                sbxml.Append("<extend>");
                sbxml.Append("<property name=\"tenderOrderNo\" value=\"" + toCpTransaction.tenderOrderNo + "\" />");
                sbxml.Append("<property name=\"creditorPlatformUserNo\" value=\"" + toCpTransaction.creditorPlatformUserNo + "\" />");
                sbxml.Append("<property name=\"originalRequestNo\" value=\"" + toCpTransaction.originalRequestNo + "\" />");
                sbxml.Append("</extend>");
                sbxml.Append("<callbackUrl>" + toCpTransaction.callbackUrls.toCpTransaction_CREDIT_ASSIGNMENT + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toCpTransaction._notifyUrl + "</notifyUrl>");//Y 服务器通知URL
                sbxml.Append("</request>");

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toCpTransaction._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息


                // 回调通知（企业用户注册）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值REGISTER
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号

                RequestLog("ToCpTransaction_CREDIT_ASSIGNMENT(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToCpTransaction_CREDIT_ASSIGNMENT(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        #endregion

        /// <summary>
        /// 2.8 自动投标授权
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToAuthorizeAutoTransfer(ToAuthorizeAutoTransfer toAuthorizeAutoTransfer)
        {
            try
            {

                #region 参数

                string Action = EnumActionRequest.toAuthorizeAutoTransfer.ToEnumDesc();//支持 properties , Y 
                string actionUrl = toAuthorizeAutoTransfer._actionUrl + Action;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toAuthorizeAutoTransfer._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toAuthorizeAutoTransfer.platformUserNo + "</platformUserNo>");//Y 商户平台会员标识,会员在商户平台唯一标识
                sbxml.Append("<requestNo>" + toAuthorizeAutoTransfer.requestNo + "</requestNo>");//Y 请求流水号,
                sbxml.Append("<callbackUrl>" + toAuthorizeAutoTransfer.callbackUrls.toAuthorizeAutoTransfer + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toAuthorizeAutoTransfer._notifyUrl + "</notifyUrl>");//Y 服务器通知URL，服务器通知URL
                sbxml.Append("</request>");

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toAuthorizeAutoTransfer._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（自动投标授权）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// N 请求流水号
                //string service = "";//Y 服务名称，固定值 AUTHORIZE_AUTO_TRANSFER
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                // 回调通知（自动投标授权）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值BIND_BANK_CARD
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号,用户编号

                RequestLog("ToAuthorizeAutoTransfer(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToAuthorizeAutoTransfer(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        /// <summary>
        /// 2.9 自动还款授权
        /// </summary>
        public BaseResultDto<PostBaseYeePayPar> ToAuthorizeAutoRepayment(ToAuthorizeAutoRepayment toAuthorizeAutoRepayment)
        {
            try
            {
                #region 参数

                string Action = EnumActionRequest.toAuthorizeAutoRepayment.ToEnumDesc();//支持 properties , Y 
                string actionUrl = toAuthorizeAutoRepayment._actionUrl + Action;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + toAuthorizeAutoRepayment._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + toAuthorizeAutoRepayment.platformUserNo + "</platformUserNo>");//Y 商户平台会员标识,会员在商户平台唯一标识
                sbxml.Append("<requestNo>" + toAuthorizeAutoRepayment.requestNo + "</requestNo>");//Y 请求流水号,
                sbxml.Append("<orderNo>" + toAuthorizeAutoRepayment.orderNo + "</orderNo>");//Y 标的号，标识要自动还款的标的号
                sbxml.Append("<callbackUrl>" + toAuthorizeAutoRepayment.callbackUrls.toAuthorizeAutoRepayment + "</callbackUrl>");//Y 页面回跳URL,页面回跳URL
                sbxml.Append("<notifyUrl>" + toAuthorizeAutoRepayment._notifyUrl + "</notifyUrl>");//Y 服务器通知URL，服务器通知URL
                sbxml.Append("</request>");

                postBaseYeePayPar.req = sbxml.ToString();
                postBaseYeePayPar.sign = HttpPost(toAuthorizeAutoRepayment._signUrl, "req=" + sbxml.ToString());
                baseResultDto.Tag = postBaseYeePayPar;
                baseResultDto.ErrorMsg = actionUrl;
                baseResultDto.IsSeccess = true;

                #endregion

                //接口输出（自动还款授权）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string requestNo = "";// Y 请求流水号
                //string service = "";//Y 服务名称，固定值 AUTHORIZE_AUTO_REPAYMENT
                //string code = "";//Y 返回码,【见返回码】
                //string description = "";//N 描述，描述异常信息

                // 回调通知（自动还款授权）
                //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
                //string bizType = "";// Y 业务名称,固定值AUTHORIZE_AUTO_REPAYMENT
                //string code = "";//Y 返回码,【见返回码】
                //string message = "";//N 描述，描述异常信息
                //string platformUserNo = "";//Y 平台的用户编号,用户编号

                RequestLog("ToAuthorizeAutoRepayment(request)" + sbxml.ToString() + ",actionUrl:" + actionUrl, false);
            }
            catch (Exception ex)
            {
                RequestLog("ToAuthorizeAutoRepayment(request)(catch)" + ex.ToString(), false);
            }

            return baseResultDto;
        }

        #endregion

        #region 2.网关接口（V2.0 版本 没有的方法）

        ///// <summary>
        ///// v1.0
        ///// 2.7 资金冻结（投标）:投资人发起投标，输入交易密码成功后，投资人账户内指定的资金会被冻结，直到放款或者取消投标
        ///// </summary>
        //public string toTransfer()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        string Action = "toTransfer";//支持 properties , Y 

        //        string platformNo = _platformNo;//Y 商户编号,商户在易宝唯一标识
        //        string platformUserNo = "";//Y 投资人台会员编号,会员在商户平台唯一标识
        //        string requestNo = "";//Y 请求流水号,
        //        string orderNo = "";//Y 标的号，标的号
        //        string transferAmount = "";//Y 标的金额
        //        string targetPlatformUserNo = "";//Y 借款人会员编号
        //        string paymentAmount = "";//Y 冻结金额,至少 1 元
        //        string expired = "";//Y 投标过期时间,超过此时间点，用户即会投标失败
        //        string callbackUrl = _callbackUrl;//Y 页面回跳URL,页面回跳URL
        //        string notifyUrl = _notifyUrl;//Y 服务器通知URL，服务器通知URL

        //        string req = "";
        //        string sign = "";

        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");
        //        sbxml.Append("<transferAmount>" + transferAmount + "</transferAmount>");
        //        sbxml.Append("<targetPlatformUserNo>" + targetPlatformUserNo + "</targetPlatformUserNo>");
        //        sbxml.Append("<paymentAmount>" + paymentAmount + "</paymentAmount>");
        //        sbxml.Append("<expired>" + expired + "</expired>");
        //        sbxml.Append("<callbackUrl>" + callbackUrl + "</callbackUrl>");
        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");
        //        req = sbxml.ToString();

        //        string actionUrl = _actionUrl + Action;

        //        string actionFrom = ActionFrom(actionUrl, req, sign);//组合

        //        sb.Append(actionFrom);

        //        //接口输出（资金冻结（投标））

        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string service = "";//Y 服务名称，固定值 TRANSFER
        //        //string requestNo = "";// N 请求流水号,注册不传入请求流水号，返回无流水号
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string description = "";//N 描述，描述异常信息


        //        // 回调通知（资金冻结（投标））

        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string bizType = "";// Y 业务名称,固定值TRANSFER
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string message = "";//N 描述，描述异常信息
        //        //string requestNo = "";// Y 请求流水号,

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        ///// v1.0
        ///// 2.8 还款
        ///// </summary>
        //public string toRepayment()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        string Action = "toRepayment";//支持 properties , Y 

        //        string platformNo = _platformNo;//Y 商户编号,商户在易宝唯一标识
        //        string platformUserNo = "";//Y 商户平台会员标识,会员在商户平台唯一标识
        //        string requestNo = "";//Y 请求流水号,
        //        string orderNo = "";//Y 标的号，标的号
        //        string paymentRequestNo = "";//Y 转账请求流水号
        //        string targetUserNo = "";//Y 投资人会员编号
        //        string amount = "";//Y 还款金额,投资人到账金额=还款金额-还款平台提成至少 1 元
        //        string fee = "";//Y 还款平台提成
        //        string callbackUrl = _callbackUrl;//Y 页面回跳URL,页面回跳URL
        //        string notifyUrl = _notifyUrl;//Y 服务器通知URL，服务器通知URL

        //        string req = "";
        //        string sign = "";


        //        //XML 参数示例：
        //        //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        //        //    <request platformNo="platformNo">
        //        //    <platformUserNo>platformUserNo</platformUserNo>
        //        //    <requestNo>requestNo</requestNo>
        //        //    <orderNo>orderNo</orderNo>
        //        //    <properties>
        //        //        <property name="name" value="value" />
        //        //        </properties>
        //        //        <repayments>
        //        //        <repayment>
        //        //        <paymentRequestNo>paymentRequestNo</paymentRequestNo>
        //        //        <targetUserNo>targetUserNo</targetUserNo>
        //        //        <amount>amount</amount>
        //        //        <fee>fee</fee>
        //        //        </repayment>
        //        //    </repayments>
        //        //    <notifyUrl>notifyUrl</notifyUrl>
        //        //</request>

        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");

        //        sbxml.Append("<properties>");
        //        sbxml.Append("<property name=\"name\" value=\"value\" />");
        //        sbxml.Append("</properties>");

        //        sbxml.Append(" <repayments>");
        //        sbxml.Append(" <repayment>");

        //        sbxml.Append("<paymentRequestNo>" + paymentRequestNo + "</paymentRequestNo>");
        //        sbxml.Append("<targetUserNo>" + targetUserNo + "</targetUserNo>");
        //        sbxml.Append("<amount>" + amount + "</amount>");
        //        sbxml.Append("<fee>" + fee + "</fee>");

        //        sbxml.Append(" </repayment>");
        //        sbxml.Append(" </repayments>");

        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");
        //        req = sbxml.ToString();

        //        string actionUrl = _actionUrl + Action;

        //        string actionFrom = ActionFrom(actionUrl, req, sign);//组合

        //        sb.Append(actionFrom);

        //        //接口输出（还款）

        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string service = "";//Y 服务名称，固定值 REPAYMENT
        //        //string requestNo = "";// N 请求流水号,注册不传入请求流水号，返回无流水号
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string description = "";//N 描述，描述异常信息


        //        // 回调通知（还款）

        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string bizType = "";// Y 业务名称,固定值REPAYMENT
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string message = "";//N 描述，描述异常信息
        //        //string requestNo = "";// Y 请求流水号
        //        //string orderNo = "";// Y 标的号

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        ///// v1.0
        ///// 2.9 债权转让: 将投资人的债权转卖给其他人。转让成功后，原有的投标请求流水号失效，不能再进行还款。本接口新传入的 requestNo 作为新的投标请求流水号使用
        ///// </summary>
        //public string toTransferClaims()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        string Action = "toTransferClaims";//支持 properties , Y 

        //        string platformNo = _platformNo;//Y 商户编号,商户在易宝唯一标识
        //        string platformUserNo = "";//Y 商户平台会员标识,会员在商户平台唯一标识
        //        string requestNo = "";//Y 请求流水号,
        //        string orderNo = "";//Y 标的号，标识一笔标的的标的号
        //        string paymentRequestNo = "";//Y 转账请求流水号,被转让的投标请求
        //        string amount = "";//Y 债权购买人出资的金额
        //        //string callbackUrl = _callbackUrl;//Y 页面回跳URL,页面回跳URL
        //        string notifyUrl = _notifyUrl;//Y 服务器通知URL，服务器通知URL

        //        string req = "";
        //        string sign = "";

        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");
        //        sbxml.Append("<paymentRequestNo>" + paymentRequestNo + "</paymentRequestNo>");
        //        sbxml.Append("<amount>" + amount + "</amount>");
        //        //sbxml.Append("<callbackUrl>" + callbackUrl + "</callbackUrl>");
        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");
        //        req = sbxml.ToString();

        //        string actionUrl = _actionUrl + Action;

        //        string actionFrom = ActionFrom(actionUrl, req, sign);//组合

        //        sb.Append(actionFrom);


        //        // 回调通知（债权转让）

        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string bizType = "";// Y 业务名称,固定值TRANSFER_CLAIMS
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string message = "";//N 描述，描述异常信息
        //        //string requestNo = "";// Y 请求流水号
        //        //string orderNo = "";// Y 标的号

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        ///// v1.0
        ///// 2.12 担保公司代偿
        ///// </summary>
        //public string toCompensatory()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string Action = "toCompensatory";//支持 properties , N 

        //        string requestNo = "";//Y 请求流水号,
        //        string platformNo = _platformNo;//Y 商户编号,商户在易宝唯一标识
        //        string platformUserNo = "";//Y 担保公司用户编号（平台用户编号,用户编号）
        //        string orderNo = "";// Y 标的号，标识一笔标的的标的号
        //        string transfer = "";//Y
        //        string amount = "";//Y 代偿金额
        //        string targetUserType = "";//Y 投资人用户类型,【见用户类型】
        //        string targetPlatformUserNo = "";//Y 投资人用户编号
        //        string callbackUrl = _callbackUrl;//Y 页面回跳URL,页面回跳URL
        //        string notifyUrl = _notifyUrl;//Y 服务器通知URL

        //        string _Action = _actionUrl + Action;

        //        #endregion

        //        string req = "";
        //        string sign = "";


        //        #region 组合

        //        //输入示例：
        //        //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        //        //    <request platformNo="10040011137">
        //        //        <requestNo>req1234567</requestNo>
        //        //        <platformUserNo>bhatest0004</platformUserNo>
        //        //        <orderNo>order21943534</orderNo>
        //        //        <transfers>
        //        //            <transfer>
        //        //                <targetUserType>MEMBER</targetUserType>
        //        //                <targetPlatformUserNo>bhatest0002</targetPlatformUserNo>
        //        //                <amount>1</amount>
        //        //            </transfer>
        //        //        </transfers>
        //        //        <notifyUrl>http://www.xxx.com/notify</notifyUrl>
        //        //        <callbackUrl>http://www.xxx.com/callback</callbackUrl>
        //        //    </request>

        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");

        //        sbxml.Append("<transfers>");
        //        sbxml.Append("<transfer>");

        //        sbxml.Append("<targetUserType>" + targetUserType + "</targetUserType>");
        //        sbxml.Append("<targetPlatformUserNo>" + targetPlatformUserNo + "</targetPlatformUserNo>");
        //        sbxml.Append("<amount>" + amount + "</amount>");


        //        sbxml.Append("</transfer>");
        //        sbxml.Append("</transfers>");

        //        sbxml.Append("<callbackUrl>" + callbackUrl + "</callbackUrl>");
        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");
        //        req = sbxml.ToString();

        //        string actionUrl = _actionUrl + Action;



        //        string actionFrom = ActionFrom(actionUrl, req, sign);//组合

        //        sb.Append(actionFrom);


        //        #endregion


        //        //接口输出（担保公司代偿）
        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string requestNo = "";// N 请求流水号
        //        //string service = "";//Y 服务名称，固定值COMPENSATORY
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string description = "";//N 描述，描述异常信息


        //        // 回调通知（担保公司代偿）
        //        //string platformNo = "";// Y 商户编号,商户在易宝唯一标识
        //        //string requestNo = "";// Y 请求流水号
        //        //string bizType = "";// Y 业务名称,固定值COMPENSATORY
        //        //string code = "";//Y 返回码,【见返回码】
        //        //string message = "";//N 描述，描述异常信息

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        #endregion

        #region 3.直连接口

        /// <summary>
        /// 3.1账户查询 
        /// </summary>
        /// <param name="_platformUserNo">商户平台会员标识,会员在商户平台唯一标识</param>
        /// <returns></returns>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response> ACCOUNT_INFO(Account_Info account_info)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.ACCOUNT_INFO.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.ACCOUNT_INFO.ToEnumDesc();
                string req = "";
                if (string.IsNullOrEmpty(account_info.platformUserNo))
                    account_info.platformUserNo = account_info._platformNo;

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + account_info._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<platformUserNo>" + account_info.platformUserNo + "</platformUserNo>");// 商户平台会员标识,会员在商户平台唯一标识
                sbxml.Append("</request>");

                req = sbxml.ToString();
                string sign = dataFornat.UrlEncode(HttpPost(account_info._signUrl, "req=" + req));

                //string postData = string.Format("url={0}&type=post&req={1}", account_info._signUrl, http.UrlEncode(http.HtmlEncode(req)));
                //string TestUrl = "http://211.149.204.89:81/TransData.aspx";
                //string sign = http.UrlEncode(http.HttpPost(TestUrl, postData));

                string serviceUrl = account_info._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                //返回
                string serviceXML = HttpPost(serviceUrl, signMsgVal);

                RequestLog("ACCOUNT_INFO(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("ACCOUNT_INFO(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(账户查询)
                // string platformNo="";// 商户编号 Y 商户编号
                // string code="";//  返回码 Y 【见返回码】
                // string description="";//  描述 N 描述异常信息
                // string memberType="";//  会员类型 Y 【见会员类型】
                // string activeStatus="";//  会员激活状态 Y 【见会员激活状态】
                // string balance="";//  账户余额 Y
                // string availableAmount="";//  可用余额 Y
                // string freezeAmount="";// 冻结金额 Y
                // string cardNo="";//  卡号 N 绑定的卡号
                // string cardStatus ="";// 卡的状态 N 【见绑卡状态】
                // string bank="";//  卡的开户行 N 【见银行代码】
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("ACCOUNT_INFO(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        /// 3.2资金冻结
        /// </summary>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.FREEZE.response> FREEZE(Freeze freeze)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.FREEZE.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.FREEZE.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.FREEZE.ToEnumDesc();
                string req = "";


                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + freeze._platformNo + "\">");
                sbxml.Append("<platformUserNo>" + freeze.platformUserNo + "</platformUserNo>");
                sbxml.Append("<requestNo>" + freeze.requestNo + "</requestNo>");
                sbxml.Append("<amount>" + freeze.amount + "</amount>");
                sbxml.Append("<expired>" + freeze.expired + "</expired>");
                sbxml.Append("</request>");


                req = sbxml.ToString();

                //string sign = HttpPost(freeze._signUrl, "req=" + req);
                string sign = dataFornat.UrlEncode(HttpPost(freeze._signUrl, "req=" + req));
               

                string serviceUrl = freeze._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                //返回
                string serviceXML = HttpPost(serviceUrl, signMsgVal);

                RequestLog("FREEZE(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("FREEZE(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.FREEZE.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.FREEZE.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(冻结)
                //string platformNo = "";// 商户编号 Y 商户编号
                //string code = "";// 返回码 Y 【见返回码】
                //string description = "";// 描述 N 描述异常信息
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("FREEZE(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        /// 3.3.资金解冻
        /// </summary>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.UNFREEZE.response> UNFREEZE(UnFreeze unFreeze)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.UNFREEZE.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.UNFREEZE.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.UNFREEZE.ToEnumDesc();
                string req = "";


                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + unFreeze._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<freezeRequestNo>" + unFreeze.freezeRequestNo + "</freezeRequestNo>");
                sbxml.Append("</request>");


                req = sbxml.ToString();

                //string sign = HttpPost(unFreeze._signUrl, "req=" + req);
                string sign = dataFornat.UrlEncode(HttpPost(unFreeze._signUrl, "req=" + req));

                string serviceUrl = unFreeze._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                //返回
                string serviceXML = HttpPost(serviceUrl, signMsgVal);

                RequestLog("UNFREEZE(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("UNFREEZE(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.UNFREEZE.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.UNFREEZE.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(解冻)
                //string platformNo = "";//商户编号 Y 商户编号
                //string code= "";// 返回码 Y 【见返回码】
                //string description= "";// 描述 N 描述异常信息
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();

                RequestLog("UNFREEZE(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }
        /// <summary>
        /// 3.4.直接转账
        /// </summary>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response> DIRECT_TRANSACTION(Direct_Transaction direct_Transaction)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.DIRECT_TRANSACTION.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.DIRECT_TRANSACTION.ToEnumDesc();
                string req = "";

                if (string.IsNullOrEmpty(direct_Transaction.platformUserNo))
                    direct_Transaction.platformUserNo = direct_Transaction._platformNo;
               
                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + direct_Transaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识               
                sbxml.Append("<requestNo>" + direct_Transaction.requestNo + "</requestNo>");// Y 请求流水号
                sbxml.Append("<platformUserNo>" + direct_Transaction.platformUserNo + "</platformUserNo>");// Y 出款人用户编号，目前只支持传入平台商户编号
                sbxml.Append("<userType>" + direct_Transaction._userType + "</userType>");// Y 出款人用户类型，目前只支持传入 MERCHANT
                sbxml.Append("<bizType>" + direct_Transaction._bizType + "</bizType>");// Y 目前只支持传入 TRANSFER

                sbxml.Append("<details>");

                //sbxml.Append("<detail>");
                //sbxml.Append("<amount>" + direct_Transaction.amount + "</amount>");// Y 转入金额
                //sbxml.Append("<targetUserType>" + direct_Transaction._targetUserType + "</targetUserType>");// Y 用户类型, 见【用户类型】
                //sbxml.Append("<targetPlatformUserNo>" + direct_Transaction.targetPlatformUserNo + "</targetPlatformUserNo>");// Y 平台用户编号 (转入商户)
                //sbxml.Append("<bizType>" + direct_Transaction._bizType + "</bizType>");// Y 资金明细业务类型。目前只支持传入 TRANSFER
                //sbxml.Append("</detail>");

                foreach (var item in direct_Transaction.details)
                {
                    sbxml.Append("<detail>");//Y 资金明细记录
                    sbxml.Append("<amount>" + item.amount + "</amount>");//Y 转入金额
                    sbxml.Append("<targetUserType>" + item.targetUserType + "</targetUserType>");//用户类型, 见【用户类型】
                    sbxml.Append("<targetPlatformUserNo>" + item.targetPlatformUserNo + "</targetPlatformUserNo>");//Y 平台用户编号(转入用户编号)
                    sbxml.Append("<bizType>" + item.bizType + "</bizType>");//Y bizType 资金明细业务类型。目前只支持传入 TRANSFER
                    sbxml.Append("</detail>");
                }



                sbxml.Append("</details>");

                sbxml.Append("<notifyUrl>" + direct_Transaction._notifyUrl + "</notifyUrl>");// Y 服务器通知 URL
                sbxml.Append("<callbackUrl>" + direct_Transaction._callbackUrl + "</callbackUrl>");// Y 页面回跳 URL

                sbxml.Append("</request>");

                req = sbxml.ToString();
                //string sign = HttpPost(direct_Transaction._signUrl, "req=" + req);
                string sign = dataFornat.UrlEncode(HttpPost(direct_Transaction._signUrl, "req=" + req));

                string serviceUrl = direct_Transaction._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                //返回
                string serviceXML = HttpPost(serviceUrl, signMsgVal);

                RequestLog("DIRECT_TRANSACTION(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("DIRECT_TRANSACTION(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.DIRECT_TRANSACTION.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(直接转账)
                //string platformNo = "";//商户编号 Y 商户编号
                //string code= "";// 返回码 Y 【见返回码】
                //string description= "";// 描述 N 描述异常信息
                // 回调通知(直接转账)
                //string platformNo= "";//Y 商户编号
                //string bizType= "TRANSACTION";// Y 固定值 TRANSACTION
                //string code= "";// Y 【见返回码】
                //string message= "";// N 描述信息
                //string status= "";// Y 固定值：DIRECT
                //string requestNo = "";// Y 请求流水号
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("DIRECT_TRANSACTION(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        /// 3.5.自动转账
        /// </summary>
        /// <returns></returns>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response> AUTO_TRANSACTION(Auto_Transaction auto_Transaction)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.AUTO_TRANSACTION.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.AUTO_TRANSACTION.ToEnumDesc();
                string req = "";

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + auto_Transaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<requestNo>" + auto_Transaction.requestNo + "</requestNo>");
                sbxml.Append("<platformUserNo>" + auto_Transaction.platformUserNo + "</platformUserNo>");
                sbxml.Append("<userType>" + auto_Transaction._userType + "</userType>");
                sbxml.Append("<bizType>" + auto_Transaction._bizType + "</bizType>");

                sbxml.Append("<details>");

                foreach (var item in auto_Transaction.details)
                {
                    sbxml.Append("<detail>");//Y 资金明细记录
                    sbxml.Append("<amount>" + item.amount + "</amount>");//Y 转入金额
                    sbxml.Append("<targetUserType>" + item.targetUserType + "</targetUserType>");//用户类型, 见【用户类型】
                    sbxml.Append("<targetPlatformUserNo>" + item.targetPlatformUserNo + "</targetPlatformUserNo>");//Y 平台用户编号(转入用户编号)
                    sbxml.Append("<bizType>" + item.bizType + "</bizType>");//Y bizType 资金明细业务类型。目前只支持传入 TRANSFER
                    sbxml.Append("</detail>");
                }


                sbxml.Append("</details>");

                sbxml.Append("<extend>");
                sbxml.Append("<property name=\"tenderOrderNo\" value=\"" + auto_Transaction.tenderOrderNo + "\" />");
                sbxml.Append("</extend>");

                sbxml.Append("<notifyUrl>" + auto_Transaction._notifyUrl + "</notifyUrl>");
                sbxml.Append("<callbackUrl>" + auto_Transaction._callbackUrl + "</callbackUrl>");

                sbxml.Append("</request>");

                req = sbxml.ToString();
                //string sign = HttpPost(auto_Transaction._signUrl, "req=" + req);

                string sign = dataFornat.UrlEncode(HttpPost(auto_Transaction._signUrl, "req=" + req));

                string serviceUrl = auto_Transaction._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                //返回
                string serviceXML = HttpPost(serviceUrl, signMsgVal);

                Log.RecordLog("YeePay", " DIRECT_TRANSACTION(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                Log.RecordLog("YeePay", " DIRECT_TRANSACTION(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.AUTO_TRANSACTION.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(直接转账)
                //string platformNo = "";//商户编号 Y 商户编号
                //string code= "";// 返回码 Y 【见返回码】
                //string description= "";// 描述 N 描述异常信息
                // 回调通知(直接转账)
                //string platformNo= "";//Y 商户编号
                //string bizType= "TRANSACTION";// Y 固定值 TRANSACTION
                //string code= "";// Y 【见返回码】
                //string message= "";// N 描述信息
                //string status= "DIRECT";// Y 固定值：DIRECT
                //string requestNo = "";// Y 请求流水号
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("DIRECT_TRANSACTION(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        /// 3.6.单笔业务查询
        /// </summary>
        public BaseResultDto<T> QUERY<T>(Query query) where T : class,new()
        {
            BaseResultDto<T> baseResultDto = new BaseResultDto<T>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.QUERY.ToEnumDesc();
                string req = "";
                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + query._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<requestNo>" + query.requestNo + "</requestNo>");
                sbxml.Append("<mode>" + query.mode + "</mode>");
                sbxml.Append("</request>");

                req = sbxml.ToString();
                //string sign = HttpPost(query._signUrl, "req=" + req);
                string sign = dataFornat.UrlEncode(HttpPost(query._signUrl, "req=" + req));

                string serviceUrl = query._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                //返回
                string serviceXML = HttpPost(serviceUrl, signMsgVal);

                RequestLog("DIRECT_TRANSACTION(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("DIRECT_TRANSACTION(request_return)" + serviceXML, false);//返回日志

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = serviceXML.XmlDeserialize<T>(); ;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(单笔业务查询)

                //string platformNo = "";// 商户编号 Y 商户编号
                //string code = "";// 返回码  Y 【见返回码】
                //string description = "";// 描述 N 描述异常信息
                //string records  = "";//记录列表 Y 记录列表
                //string record = "";// 记录项 N 记录项内部信息不同的业务内容不同

                //投资放款记录：
                //string paymentAmount = "";// 投资金额 Y
                //string sourceUserNo = "";// 投资人 Y 投资人的标识
                //string createTime = "";// 创建时间 Y
                //string loanTime = "";// 放款时间 N
                //string status = "";// 状态 Y 状态：FREEZED,CANCEL,LOANED

                //还款记录：
                //string repaymentAmount= "";// 还款金额 Y
                //string targetUserNo= "";// 原投资人 Y 原投资人的标识
                //string createTime= "";// 还款时间 Y
                //string status= "";// 状态 Y 还款状态：INIT,SUCCESS

                //充值记录：
                //string amount= "";// 充值金额 Y
                //string userNo= "";// 充值用户 Y
                //string createTime= "";// 充值时间 Y
                //string status= "";// 状态 Y 充值状态：INIT,SUCCESS

                //提现记录：
                //string amount= "";// 提现金额 Y
                //string userNo= "";// 提现用户 Y
                //string createTime= "";// 提现时间 Y
                //string status= "";// 状态 Y 充值状态：INIT,SUCCESS
                //remitStatus= "";// 打款状态 N REMIT_SUCCESS 打款成功、REMIT_FAILURE 打款失败、REMITING 打款中

                //输出示例
                //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                //    <response platformNo="platformNo">
                //    <code>1</code>
                //    <description>操作成功</description>
                //        <records>
                //            <record>
                //                <amount>100.21</amount>
                //                <userNo>sourceUserNo</UserNo>
                //                <createTime>2013-12-12 12:34:56</createTime>
                //                <status>LOANED</status>
                //            </record>
                //        </records>
                //    </response>
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("DIRECT_TRANSACTION(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        /// 3.7.转账确认
        /// </summary>
        /// <returns></returns>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response> COMPLETE_TRANSACTION(Complete_Transaction complete_Transaction)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.COMPLETE_TRANSACTION.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.COMPLETE_TRANSACTION.ToEnumDesc();
                string req = "";

                string notifyUrl = complete_Transaction._notifyUrl.Replace("Notify", "Notify_Complete_Transaction");

                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + complete_Transaction._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<requestNo>" + complete_Transaction.requestNo + "</requestNo>");
                sbxml.Append("<mode>" + complete_Transaction.mode + "</mode>");
                sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
                sbxml.Append("</request>");

                req = sbxml.ToString();

                //string sign = HttpPost(complete_Transaction._signUrl, "req=" + req);
                string sign = dataFornat.UrlEncode(HttpPost(complete_Transaction._signUrl, "req=" + req));

                string serviceUrl = complete_Transaction._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                string serviceXML = HttpPost(serviceUrl, signMsgVal);//返回

                RequestLog("DIRECT_TRANSACTION(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("DIRECT_TRANSACTION(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(直接转账)
                //string platformNo = "";//商户编号 Y 商户编号
                //string code= "";// 返回码 Y 【见返回码】
                //string description= "";// 描述 N 描述异常信息
                // 回调通知(直接转账)
                //string platformNo= "";//Y 商户编号
                //string bizType= "TRANSACTION";// Y 固定值 TRANSACTION
                //string code= "";// Y 【见返回码】
                //string message= "";// N 描述信息
                //string status= "CONFIRM";// Y 固定值：CONFIRM 或者 CANCEL
                //string requestNo = "";// Y 请求流水号
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("DIRECT_TRANSACTION(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }

        /// <summary>
        ///  V1.0
        /// 3.10.对账
        /// </summary>
        public BaseResultDto<MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response> RECONCILIATION(Reconciliation reconciliation)
        {
            BaseResultDto<MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response> baseResultDto = new BaseResultDto<Models.YeePay.Response.RECONCILIATION.response>();
            try
            {
                #region 参数

                string service = EnumServiceRequest.RECONCILIATION.ToEnumDesc();
                string req = "";
                StringBuilder sbxml = new StringBuilder();

                sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sbxml.Append("<request platformNo=\"" + reconciliation._platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
                sbxml.Append("<date>" + reconciliation.date + "</date>");
                sbxml.Append("</request>");

                req = sbxml.ToString();

                //string sign = HttpPost(complete_Transaction._signUrl, "req=" + req);
                string sign = dataFornat.UrlEncode(HttpPost(reconciliation._signUrl, "req=" + req));

                string serviceUrl = reconciliation._serviceUrl;

                #endregion

                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "service", service);
                signMsgVal = appendParam(signMsgVal, "req", req);
                signMsgVal = appendParam(signMsgVal, "sign", sign);

                string serviceXML = HttpPost(serviceUrl, signMsgVal);//返回

                RequestLog("RECONCILIATION(request)" + signMsgVal + ":serviceUrl" + serviceUrl, false);// 请求日志
                RequestLog("RECONCILIATION(request_return)" + serviceXML, false);//返回日志

                MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response response = serviceXML.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.RECONCILIATION.response>();

                baseResultDto.IsSeccess = true;
                baseResultDto.Tag = response;
                baseResultDto.ErrorMsg = serviceXML;

                // 接口输出(对账)
                //string platformNo= "";//  商户编号 Y 商户编号
                //string code= "";//  返回码 Y 【见返回码】
                //string description= "";//  描述 N 描述异常信息
                //string ecords= "";//  对账明细 Y 记录列表
                //string bizType= "";//  业务类型 Y 枚举值:PAYMENT、REPAYMENT、WITHDRAW、RECHARGE
                //string fee= "";//  易宝收取手续费 Y
                //string balance= "";//  商户平台收取分润 Y
                //string amount = "";//  业务金额 Y

                //输出示例:
                //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                //    <response platformNo="platformNo">
                //    <code>1</code>
                //    <description>操作成功</description>
                //        <records>
                //            <record bizType="PAYMENT" fee="0" balance="1.00" amount="5.00"time="2014-01-15 14:17:39" requestNo="xfe13901246549" platformNo="10040008878"/>
                //            <record bizType="REPAYMENT" fee="0" balance="1.00" amount="5.00" time="2014-01-15 14:17:39" requestNo="xfe13901246549" platformNo="10040008878"/>
                //            <record bizType="WITHDRAW" fee="0" balance="1.00" amount="5.00" time="2014-01-15 14:17:39" requestNo="xfe13901246549" platformNo="10040008878"/>
                //            <record bizType="RECHARGE" fee="0" balance="1.00" amount="5.00" time="2014-01-15 14:17:39" requestNo="xfe13901246549" platformNo="10040008878"/>
                //        </records>
                //    </response>
            }
            catch (Exception ex)
            {
                baseResultDto.IsSeccess = false;
                baseResultDto.ErrorMsg = ex.ToString();
                RequestLog("RECONCILIATION(request)(catch:ex)" + ex.ToString(), false);
            }
            return baseResultDto;
        }
        #endregion

        #region 3.直连接口（V2.0 版本 没有的方法）

        ///// <summary>
        ///// V1.0
        ///// 3.4.自动投标
        ///// </summary>
        //public string AUTO_TRANSFER()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string service = "AUTO_TRANSFER";
        //        string req = "";
        //        string sign = "";

        //        string platformNo = _platformNo;// 商户编号 Y 商户在易宝唯一标识
        //        string platformUserNo = _platformNo;// 商户平台会员标识 Y 会员在商户平台唯一标识
        //        string requestNo = "";// 请求流水号 Y
        //        string orderNo = "";// 标的号 Y 标识要自动还款的标的号
        //        string transferAmount = "";// 标的金额 Y
        //        string targetPlatformUserNo = "";// 目标会员编号 Y
        //        string paymentAmount = "";// 冻结金额 Y
        //        string notifyUrl = "";// 服务器通知 URL Y 服务器通知 URL

        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");
        //        sbxml.Append("<transferAmount>" + transferAmount + "</transferAmount>");
        //        sbxml.Append("<targetPlatformUserNo>" + targetPlatformUserNo + "</targetPlatformUserNo>");
        //        sbxml.Append("<paymentAmount>" + paymentAmount + "</paymentAmount>");
        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");

        //        req = sbxml.ToString();

        //        string serviceUrl = _serviceUrl;

        //        #endregion

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(自动投标)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code = "";//返回码 Y 【见返回码】
        //        //string description = "";//描述 N 描述异常信息

        //        // 回调通知(自动投标)

        //        //string platformNo = "";// 商户编号 Y 商户编号
        //        //string bizType = "";// 业务名称 Y 固定值 FREEZE
        //        //string code = "";// 返回码 Y 【见返回码】
        //        //string message = "";// 描述 N 描述异常信息
        //        //string requestNo = "";//请求流水号 Y
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        /////  V1.0
        ///// 3.5.自动还款
        ///// </summary>
        //public string AUTO_REPAYMENT(string _platformUserNo)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string service = "AUTO_REPAYMENT";
        //        string req = "";
        //        string sign = "";//

        //        string platformNo = _platformNo;// 商户编号 Y 商户在易宝唯一标识
        //        string platformUserNo = _platformUserNo;// 商户平台会员标识 Y 会员在商户平台唯一标识
        //        string requestNo = "";// 还款请求流水号 Y
        //        string orderNo = "";// 标的号 Y 标识一笔要自动还款的标的的标的号
        //        string paymentRequestNo = "";// 转账请求流水号 Y
        //        string targetUserNo = "";// 投资人会员编号 Y
        //        string amount = "";// 还款金额 Y 投资人到账金额=还款金额-还款平台提成
        //        string fee = "";// 还款平台提成 Y
        //        string notifyUrl = "";// 服务器通知 URL Y 服务器通知 URL


        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");

        //        sbxml.Append("<repayments><repayment>");

        //        sbxml.Append("<paymentRequestNo>" + paymentRequestNo + "</paymentRequestNo>");
        //        sbxml.Append("<targetUserNo>" + targetUserNo + "</targetUserNo>");
        //        sbxml.Append("<amount>" + amount + "</amount>");
        //        sbxml.Append("<fee>" + fee + "</fee>");

        //        sbxml.Append("</repayment></repayments>");

        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");

        //        req = sbxml.ToString();

        //        //XML 参数示例：
        //        //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        //        //    <request platformNo="platformNo">
        //        //        <platformUserNo>platformUserNo</platformUserNo>
        //        //        <requestNo>requestNo</requestNo>
        //        //        <orderNo>orderNo</orderNo>
        //        //        <repayments>
        //        //            <repayment>
        //        //                <paymentRequestNo>paymentRequestNo</paymentRequestNo>
        //        //                <targetUserNo>targetUserNo</targetUserNo>
        //        //                <amount>amount</amount>
        //        //                <fee>fee</fee>
        //        //            </repayment>
        //        //        </repayments>
        //        //        <notifyUrl>notifyUrl</notifyUrl>
        //        //    </request>

        //        string serviceUrl = _serviceUrl;

        //        #endregion

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(自动还款)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string description= "";// 描述 N 描述异常信息

        //        // 回调通知(自动还款)

        //        //string platformNo = "";// 商户编号 Y 商户编号
        //        //string bizType = "";// 业务名称 Y 固定值 REPAYMENT
        //        //string code = "";// 返回码 Y 【见返回码】
        //        //string message = "";// 描述 N 描述异常信息
        //        //string requestNo = "";// 请求流水号 Y
        //        //string orderNo = "";// 标的号 Y


        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        /////  V1.0
        ///// 3.6.放款:将投资人已投标并冻结的金额转入借款人账户内。
        ///// </summary>
        //public string LOAN()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string service = "LOAN";
        //        string req = "";
        //        string sign = "";


        //        string platformNo = _platformNo;//  商户编号 Y 商户在易宝唯一标识
        //        string orderNo = "";// 标的号 Y 标识一笔标的的标的号
        //        string requestNo = "";//  请求流水号 Y
        //        string fee = "";//  平台方收取费用 N
        //        string transfer = "";//  Y
        //        string requestNoOld = "";// 投标请求流水号 Y 之前投标的请求流水号
        //        string transferAmount = "";//  转账请求转账金额 Y
        //        string sourceUserType = "";//  投资人会员类型 Y 【见用户类型】
        //        string sourcePlatformUserNo = "";//  投资人会员编号 Y
        //        string targetUserType = "";//  借款人会员类型 Y 【见用户类型】
        //        string targetPlatformUserNo = "";//  借款人会员编号 Y
        //        string notifyUrl = "";//  服务器通知 URL Y 服务器通知 URL


        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<fee>" + fee + "</fee>");

        //        sbxml.Append("<transfers><transfer>");

        //        sbxml.Append("<requestNo>" + requestNoOld + "</requestNo>");
        //        sbxml.Append("<transferAmount>" + transferAmount + "</transferAmount>");
        //        sbxml.Append("<sourceUserType>" + sourceUserType + "</sourceUserType>");
        //        sbxml.Append("<sourcePlatformUserNo>" + sourcePlatformUserNo + "</sourcePlatformUserNo>");
        //        sbxml.Append("<targetUserType>" + targetUserType + "</targetUserType>");
        //        sbxml.Append("<targetPlatformUserNo>" + targetPlatformUserNo + "</targetPlatformUserNo>");

        //        sbxml.Append("</transfer></transfers>");
        //        sbxml.Append("<notifyUrl>" + notifyUrl + "</notifyUrl>");
        //        sbxml.Append("</request>");

        //        //输入示例：
        //        //<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        //        //    <request platformNo="platformNo">
        //        //    <requestNo>requestNo</requestNo>
        //        //    <orderNo>orderNo</orderNo>
        //        //    <fee>fee</fee>
        //        //        <transfers>
        //        //            <transfer>
        //        //            <requestNo>requestNo</requestNo>
        //        //            <transferAmount>transferAmount</transferAmount>
        //        //            <sourceUserType>sourceUserType</sourceUserType>
        //        //            <sourcePlatformUserNo>sourcePlatformUserNo</sourcePlatformUserNo>
        //        //            <targetUserType>targetUserType</targetUserType>
        //        //            <targetPlatformUserNo>targetPlatformUserNo</targetPlatformUserNo>
        //        //            </transfer>
        //        //        </transfers>
        //        //    <notifyUrl>notifyUrl</notifyUrl>
        //        //    </request>


        //        req = sbxml.ToString();

        //        string serviceUrl = _serviceUrl;

        //        #endregion

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(放款)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string description= "";// 描述 N 描述异常信息

        //        // 回调通知(放款)
        //        //string platformNo= "";// 商户编号 Y 商户编号
        //        //string bizType= "";// 业务名称 Y 固定值 TRANSFER
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string message= "";// 描述 N 描述异常信息
        //        //string requestNo= "";// 请求流水号 Y ;号分隔的多个记录
        //        //string orderNo= "";// 标的号 Y

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        /////  V1.0
        ///// 3.7.取消投标:如果投资人投的标还没有放款，可以通过此接口取消，并解冻资金
        ///// </summary>
        //public string REVOCATION_TRANSFER()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string service = "REVOCATION_TRANSFER";
        //        string req = "";
        //        string sign = "";//

        //        string platformNo = _platformNo;//商户编号 Y 商户在易宝唯一标识
        //        string requestNo = "";//之前投标的请求流水号 Y
        //        string platformUserNo = "";//用户编号 Y


        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("</request>");

        //        req = sbxml.ToString();

        //        string serviceUrl = _serviceUrl;

        //        #endregion

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(取消投标)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string description= "";// 描述 N 描述异常信息

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        /////  V1.0
        ///// 3.8.平台划款
        ///// </summary>
        //public string PLATFORM_TRANSFER()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string service = "PLATFORM_TRANSFER";
        //        string req = "";
        //        string sign = "";//

        //        string platformNo = _platformNo;// 商户编号 Y 商户在易宝唯一标识
        //        string requestNo = "";// 请求流水号 Y
        //        string sourceUserType = "";// 出款人类型 Y 【 见 用 户 类 型 】， 现 在 只 支 持 MERCHANT
        //        string sourcePlatformUserNo = "";// 出款人编号 Y 如果是 MERCHANT 类型，可以填写平台下属的各个商编号
        //        string amount = "";// 划款金额 Y
        //        string targetUserType = "";// 收款人类型 Y 【见用户类型】
        //        string targetPlatformUserNo = "";// 收款人编号 Y


        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<sourceUserType>" + sourceUserType + "</sourceUserType>");
        //        sbxml.Append("<sourcePlatformUserNo>" + sourcePlatformUserNo + "</sourcePlatformUserNo>");
        //        sbxml.Append("<amount>" + amount + "</amount>");
        //        sbxml.Append("<targetUserType>" + targetUserType + "</targetUserType>");
        //        sbxml.Append("<targetPlatformUserNo>" + targetPlatformUserNo + "</targetPlatformUserNo>");
        //        sbxml.Append("</request>");

        //        req = sbxml.ToString();

        //        string serviceUrl = _serviceUrl;

        //        #endregion

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(平台划款)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string description= "";// 描述 N 描述异常信息

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}



        ///// <summary>
        /////  V1.0
        ///// 3.11.取消自动投标授权
        ///// </summary>
        //public string CANCEL_AUTHORIZE_AUTO_TRANSFER()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region 参数

        //        string service = "CANCEL_AUTHORIZE_AUTO_TRANSFER";
        //        string req = "";
        //        string sign = "";

        //        string platformNo = _platformNo;// 商户编号 Y 商户在易宝唯一标识
        //        string platformUserNo = "";// 会员编号 Y
        //        string requestNo = "";//请求流水号 Y

        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("</request>");

        //        req = sbxml.ToString();

        //        string serviceUrl = _serviceUrl;

        //        #endregion

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(取消自动投标授权)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string description= "";// 描述 N 描述异常信息

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        /////  V1.0
        ///// 3.12.取消自动还款授权 
        ///// </summary>
        //public string CANCEL_AUTHORIZE_AUTO_REPAYMENT()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        string service = "CANCEL_AUTHORIZE_AUTO_REPAYMENT";
        //        string req = "";
        //        string sign = "";

        //        string platformNo = _platformNo;// 商户编号 Y 商户在易宝唯一标识
        //        string platformUserNo = "";//会员编号 Y
        //        string requestNo = "";//请求流水号 Y
        //        string orderNo = "";//投标流水号 Y
        //        StringBuilder sbxml = new StringBuilder();

        //        sbxml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        //        sbxml.Append("<request platformNo=\"" + platformNo + "\">"); // 商户编号 Y 商户在易宝唯一标识
        //        sbxml.Append("<platformUserNo>" + platformUserNo + "</platformUserNo>");
        //        sbxml.Append("<requestNo>" + requestNo + "</requestNo>");
        //        sbxml.Append("<orderNo>" + orderNo + "</orderNo>");
        //        sbxml.Append("</request>");

        //        req = sbxml.ToString();

        //        string serviceFrom = ServiceFrom(service, req, sign);//组合

        //        // 接口输出(取消自动还款授权)
        //        //string platformNo = "";//商户编号 Y 商户编号
        //        //string code= "";// 返回码 Y 【见返回码】
        //        //string description= "";// 描述 N 描述异常信息
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return sb.ToString();
        //}

        #endregion V2.0 没有的方法

        #region 4.附录V1.0

        //4.1. 服务名称
        //枚举值 枚举描述
        //REGISTER 注册
        //RECHARGE 充值
        //WITHDRAW 提现
        //ACCOUNT_INFO 查询账户信息
        //AUTHORIZE_AUTO_TRANSFER 自动转账授权
        //AUTHORIZE_AUTO_REPAYMENT 自动还款授权
        //TRANSFER 资金冻结
        //LOAN 放款

        //4.2. 返回状态码
        //枚举值 枚举描述
        //1 成功
        //0 失败
        //2 xml 参数格式错误
        //3 签名验证失败
        //101 引用了不存在的对象（例如错误的订单号）
        //102 业务状态不正确
        //103 由于业务限制导致业务不能执行
        //104 实名认证失败

        //4.3. 费率模式
        //枚举值 枚举描述
        //PLATFORM 收取商户手续费
        //USER 收取用户手续费

        //4.4. 身份证类型
        //枚举值 枚举描述
        //G1_IDCARD 1 代身份证
        //G2_IDCARD 2 代身份证

        //4.5. 用户类型
        //枚举值 枚举描述
        //MEMBER 个人会员
        //MERCHANT 商户会员（理财公司

        //4.6. 绑卡状态
        //枚举值 枚举描述
        //VERIFYING 认证中
        //VERIFIED 已认证

        //4.7. 银行代码
        //枚举值 枚举描述
        //BOCO 交通银行
        //CEB 光大银行
        //SPDB 上海浦东发展银行
        //ABC 农业银行
        //ECITIC 中信银行
        //PAB 平安银行
        //CCB 建设银行
        //CMBC 民生银行
        //SDB 深圳发展银行
        //POST 中国邮政储蓄
        //CMBCHINA 招商银行
        //CIB 兴业银行
        //ICBC 中国工商银行
        //BOC 中国银行
        //BCCB 北京银行
        //GDB 广发银行
        //HXB 华夏银行
        //XACB 西安市商业银行
        //SHB 上海银行
        //TJCB 天津市商业银行
        //TYCB 太原市商业银行
        //GZCB 广州市商业银行
        //SNXS 深圳农村商业银行
        //SHRCB 上海农商银行
        //BJRCB 北京农商银行
        //CDCB 成都市商业银行
        //HZCB 杭州市商业银行
        //NOBC 南洋商业银行
        //KLB 昆仑银行
        //ZZYH 郑州银行
        //WZYH 温州银行
        //HKYH 汉口银行
        //QLYH 齐鲁银行
        //DDYH 丹东银行
        //HBC 恒生银行
        //NJCB 南京银行
        //XMYH 厦门银行
        //NCYH 南昌银行
        //DONGGUANBC 东莞银行
        //JSBCHINA 江苏银行
        //HKBEA 东亚银行(中国)
        //AYYH 安阳银行
        //CDYH 成都银行
        //NBB 宁波银行
        //CSCB 长沙银行
        //HBYH 河北银行
        //NYFZYH 农业发展银行
        //GZYH 广州银行

        //4.8. 会员激活状态
        //枚举值 枚举描述
        //ACTIVATED 已激活
        //DEACTIVATED 未激活

        //4.9. 会员类型
        //枚举值 枚举描述
        //PERSONAL 个人会员
        //ENTERPRISE 企业会员

        #endregion

        #region 4 附录V2.0

        //4.1. 服务名称
        //枚举值 枚举描述
        //REGISTER 注册
        //RECHARGE 充值
        //WITHDRAW 提现
        //ACCOUNT_INFO 查询账户信息
        //AUTHORIZE_AUTO_TRANSFER 自动转账授权
        //AUTHORIZE_AUTO_REPAYMENT 自动还款授权
        //TRANSFER 资金冻结
        //LOAN 放款
        //4.2. 返回状态码
        //枚举值 枚举描述
        //1 成功
        //0 失败
        //2 xml 参数格式错误
        //3 签名验证失败
        //101 引用了不存在的对象（例如错误的订单号）
        //102 业务状态不正确
        //103 由于业务限制导致业务不能执行
        //104 实名认证失败
        //4.3. 费率模式
        //枚举值 枚举描述
        //PLATFORM 收取商户手续费
        //USER 收取用户手续费
        //4.4. 身份证类型
        //枚举值 枚举描述
        //G1_IDCARD 1 代身份证TRANSFER 转账
        //COMMISSION 分润，仅在资金转账明细中使用BJRCB 北京农商银行
        //CDCB 成都市商业银行
        //HZCB 杭州市商业银行
        //NOBC 南洋商业银行
        //KLB 昆仑银行
        //ZZYH 郑州银行
        //WZYH 温州银行
        //HKYH 汉口银行
        //QLYH 齐鲁银行
        //DDYH 丹东银行
        //HBC 恒生银行
        //NJCB 南京银行
        //XMYH 厦门银行
        //NCYH 南昌银行
        //DONGGUANBC 东莞银行
        //JSBCHINA 江苏银行
        //HKBEA 东亚银行(中国)
        //AYYH 安阳银行
        //CDYH 成都银行
        //NBB 宁波银行
        //CSCB 长沙银行
        //HBYH 河北银行
        //NYFZYH 农业发展银行
        //GZYH 广州银行
        //4.8. 会员激活状态
        //枚举值 枚举描述
        //ACTIVATED 已激活
        //DEACTIVATED 未激活
        //4.9. 会员类型
        //枚举值 枚举描述
        //PERSONAL 个人会员
        //ENTERPRISE 企业会员
        //4.10. 业务类型
        //枚举值 枚举描述
        //TENDER 投标
        //REPAYMENT 还款
        //CREDIT_ASSIGNMENT 债权转让G2_IDCARD 2 代身份证
        //4.5. 用户类型
        //枚举值 枚举描述
        //MEMBER 个人会员
        //MERCHANT 商户
        //4.6. 绑卡状态
        //枚举值 枚举描述
        //VERIFYING 认证中
        //VERIFIED 已认证
        //4.7. 银行代码
        //枚举值 枚举描述
        //BOCO 交通银行
        //CEB 光大银行
        //SPDB 上海浦东发展银行
        //ABC 农业银行
        //ECITIC 中信银行
        //PAB 平安银行
        //CCB 建设银行
        //CMBC 民生银行
        //SDB 深圳发展银行
        //POST 中国邮政储蓄
        //CMBCHINA 招商银行
        //CIB 兴业银行
        //ICBC 中国工商银行
        //BOC 中国银行
        //BCCB 北京银行
        //GDB 广发银行
        //HXB 华夏银行
        //XACB 西安市商业银行
        //SHB 上海银行
        //TJCB 天津市商业银行
        //TYCB 太原市商业银行
        //GZCB 广州市商业银行
        //SNXS 深圳农村商业银行
        //SHRCB 上海农商银行

        #endregion

        //4 附录 
        //41 服务名称：REGISTER 注册、RECHARGE 充值、WITHDRAW 提现、ACCOUNT_INFO 查询账户信息、AUTHORIZE_AUTO_TRANSFER 自动转账授权、AUTHORIZE_AUTO_REPAYMENT 自动还款授权、TRANSFER 资金冻结、LOAN 放款、
        //42 返回状态码：1 成功、0 失败、2 xml 参数格式错误、3 签名验证失败、101 引用了不存在的对象（例如错误的订单号）、102 业务状态不正确、103 由于业务限制导致业务不能执行、104 实名认证失败、
        //43 费率模式：PLATFORM 收取商户手续费、USER 收取用户手续费、
        //44 身份证类型：G1_IDCARD 1代身份证、G2_IDCARD 2代身份证、
        //45 用户类型：MEMBER 个人会员、MERCHANT 商户、
        //46 绑卡状态：VERIFYING 认证中、VERIFIED 已认证、
        //48 会员激活状态 ：ACTIVATED 已激活、DEACTIVATED 未激活
        //49 会员类型：PERSONAL 个人会员、ENTERPRISE 企业会员
        //410 业务类型：TENDER 投标、REPAYMENT 还款、CREDIT_ASSIGNMENT 债权转让、TRANSFER 转账、COMMISSION 分润仅在资金转账明细中使用、

        #region 公共方法
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
                RequestLog("ServiceFrom:catch" + ex.ToString(), false);
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
                RequestLog("ActionFrom:catch" + ex.ToString(), false);
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
                res = e.Message.ToString();

                RequestLog("ActionFrom:catch" + res.ToString(), false);
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
        /// <summary>
        /// 记录文本日志
        /// </summary>
        /// <param name="content">记录内容</param>
        /// <param name="IsRecordRequest">是否记录 Request 参数</param>
        public void RequestLog(string errContent, bool IsRecordRequest)
        {
            try
            {
                Log.RecordLog("YeePay", errContent, IsRecordRequest);
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}