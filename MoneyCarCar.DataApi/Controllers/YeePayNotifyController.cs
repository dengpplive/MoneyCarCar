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
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;
using System.Text;
namespace MoneyCarCar.DataApi.Controllers
{
    public class YeePayNotifyController : ApiController
    {
        YeePayOper yeePayOper = new YeePayOper();
        YeePay yeePay = new YeePay();
        DataFornat dataFornat = new DataFornat();
        YeePayConfig yeePayConfig = new YeePayConfig();


        /// <summary>
        /// 所有接口返回通知（除 3.7 回调通知）
        /// </summary>
        /// <param name="yeePayNotify"></param>
        /// <returns></returns>
        [HttpPost]
        public string Notify(YeePayNotify yeePayNotify)
        {
            bool reuslt = false;

            StringBuilder sbLog = new StringBuilder();

            try
            {
                //1.记录请求日志
                //sbLog.Append("\r\n 1、请求参数:" + " notify(XML): " + yeePayNotify.notify + " \r\n sign: " + yeePayNotify.sign);

                // 1.记录请求日志(不记录签名数据，只记录XML数据)
                sbLog.Append("\r\n 1、请求参数:" + " notify(XML): " + yeePayNotify.notify );

                StringBuilder postData = new StringBuilder();
                postData.Append("req=" + dataFornat.UrlEncode(yeePayNotify.notify));//编码
                postData.Append("&sign=" + dataFornat.UrlEncode(yeePayNotify.sign)); //编码
                string strHttpPost = yeePay.HttpPost(yeePayConfig._verifyUrl, postData.ToString());

                sbLog.Append("\r\n 2. 验证签名状态:" + strHttpPost);

                // 2. 验证签名
                if (strHttpPost.Contains("SUCCESS"))
                {
                    // 解析XML
                    XElement root = XElement.Parse(yeePayNotify.notify);
                    string bizType = root.Element("bizType").Value;
                    string code = root.Element("code").Value;
                    notify _baseNotify = null;

                    #region 3.业务处理

                    if (code == ((int)EnumCode.CodeTrue).ToString())
                    {
                        #region

                        if (bizType == EnumNotifyBizType.REGISTER.ToEnumDesc())//2.1 注册( 2.6企业注册) ok
                        {
                            _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();//序列化 XML 转 实体 
                            reuslt = yeePayOper.ToRegister(_baseNotify.platformUserNo);
                        }
                        else if (bizType == EnumNotifyBizType.RECHARGE.ToEnumDesc())//2.2 充值 ok
                        {
                            _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();
                            reuslt = yeePayOper.ToRecharge(_baseNotify.platformUserNo, _baseNotify.requestNo);
                        }
                        else if (bizType == EnumNotifyBizType.WITHDRAW.ToEnumDesc()) // 2.3 提现 ok
                        {
                            MoneyCarCar.Models.YeePay.NotifyModel.toWithdraw.notify _notify = yeePayNotify.notify.XmlDeserialize<MoneyCarCar.Models.YeePay.NotifyModel.toWithdraw.notify>();
                            reuslt = yeePayOper.ToWithdraw(_baseNotify.platformUserNo, _baseNotify.requestNo);
                        }
                        else if (bizType == EnumNotifyBizType.BIND_BANK_CARD.ToEnumDesc())// 2.4 绑卡 ok
                        {
                            MoneyCarCar.Models.YeePay.NotifyModel.toBindBankCard.notify _notify = yeePayNotify.notify.XmlDeserialize<MoneyCarCar.Models.YeePay.NotifyModel.toBindBankCard.notify>();
                            string BankName = _notify.bank.ToEnum<EnumBank>().ToEnumDesc();
                            reuslt = yeePayOper.ToBindBank(_notify.cardNo, BankName, _notify.platformUserNo, _notify.requestNo);
                        }
                        else if (bizType == EnumNotifyBizType.UNBIND_BANK_CARD.ToEnumDesc())
                        {
                            //2.5 同步处理
                        }
                        else if (bizType == EnumNotifyBizType.TRANSACTION.ToEnumDesc())
                        {
                            //2.7 转账、投标、还款、债权转让
                            _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();
                            if (_baseNotify.status == EnumNotifyStatus.PREAUTH.ToEnumDesc())
                            {
                                BaseHelper baseHelper = new BaseHelper();
                                // 调用存储过程 ：业务处理
                                string errorMsg = "";
                                reuslt = yeePayOper.ToCpTransaction(_baseNotify.requestNo, out errorMsg);

                                if (reuslt)
                                {
                                    #region 自动调用转账确认

                                    MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction complete_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction();

                                    complete_Transaction.platformUserNo = _baseNotify.platformUserNo;
                                    complete_Transaction.requestNo = _baseNotify.requestNo;
                                    complete_Transaction.mode = EnumModeCOMPLETETRANSACTION.CONFIRM.ToString();

                                    BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response>();
                                    YeePay yeepay = new YeePay();
                                    baseResultDtoResponse = yeepay.COMPLETE_TRANSACTION(complete_Transaction);
                                    string str = baseResultDtoResponse.ErrorMsg;//XML 数据

                                    #endregion
                                }
                                else
                                {
                                    sbLog.Append("\r\n  (2.7 转账、投标、还款、债权转让)业务处理失败,不自动调用<转账确认>接口");
                                }
                            }
                            else if (_baseNotify.status == EnumNotifyStatus.DIRECT.ToEnumDesc())
                            {
                                // 3.4 直接转账（平台转款）、
                                _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();
                                YeePayOper yeep = new YeePayOper();
                                reuslt = yeep.Direct_Transaction(_baseNotify.requestNo);

                            }
                            else if (_baseNotify.status == EnumNotifyStatus.CONFIRM.ToEnumDesc())
                            {

                                // 3.5 自动转账（自动还款）单独调用， 与 3.7 无关
                                _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();
                                YeePayOper yeep = new YeePayOper();
                                reuslt = yeep.Direct_Transaction(_baseNotify.requestNo);

                                if (reuslt)
                                {
                                    #region 自动调用转账确认

                                    MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction complete_Transaction = new MoneyCarCar.Models.YeePay.RequestModel.Complete_Transaction();

                                    complete_Transaction.platformUserNo = _baseNotify.platformUserNo;
                                    complete_Transaction.requestNo = _baseNotify.requestNo;
                                    complete_Transaction.mode = EnumModeCOMPLETETRANSACTION.CONFIRM.ToString();

                                    BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.COMPLETE_TRANSACTION.response>();
                                    YeePay yeepay = new YeePay();
                                    baseResultDtoResponse = yeepay.COMPLETE_TRANSACTION(complete_Transaction);
                                    string str = baseResultDtoResponse.ErrorMsg;//XML 数据

                                    #endregion
                                }
                                else
                                {
                                    sbLog.Append("\r\n  (3.5 自动转账)业务处理失败,不自动调用<转账确认>接口");
                                }
                            }
                            else
                            {
                                sbLog.Append("\r\n  验证状态(status)失败:不处理");
                            }
                        }
                        else if (bizType == EnumNotifyBizType.AUTHORIZE_AUTO_TRANSFER.ToEnumDesc())
                        {
                            //2.8 转账授权 
                            _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();
                            // 调用存储过程 ：业务处理

                        }
                        else if (bizType == EnumNotifyBizType.AUTHORIZE_AUTO_REPAYMENT.ToEnumDesc())
                        {
                            //2.9. 自动还款授权
                            _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();
                            // 调用存储过程 ：业务处理
                        }
                        else
                        {
                            sbLog.Append("\r\n 验证状态(bizType)失败:不处理");
                        }
                        #endregion
                    }
                    else
                    {
                        sbLog.Append("\r\n 验证状态(code)失败:不处理");
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                sbLog.Append("\r\n 业务处理异常(catch）:" + e.ToString());
            }
            finally
            {
                sbLog.Append("\r\n 返回状态:" + (reuslt == true ? "SUCCESS" : "FALE"));
                Log.RecordLog("YeePayNotifyController", sbLog.ToString(), false);
            }

            if (reuslt == true)
            {
                return "SUCCESS"; //成功
            }
            else
            {
                return "FALE"; //失败
            }
        }

        /// <summary>
        /// 3.7 接收通知，目前不做任何处理
        /// </summary>
        /// <param name="yeePayNotify"></param>
        /// <returns></returns>
        [HttpPost]
        public string Notify_Complete_Transaction(YeePayNotify yeePayNotify)
        {
            bool reuslt = false;

            StringBuilder sbLog = new StringBuilder();

            try
            {
                //1.记录请求日志
                //sbLog.Append("\r\n 1、请求参数:" + " notify(XML): " + yeePayNotify.notify + " \r\n sign: " + yeePayNotify.sign);

                // 1.记录请求日志(不记录签名数据，只记录XML数据)
                sbLog.Append("\r\n 1、(3.7 确认转账)请求参数:" + " notify(XML): " + yeePayNotify.notify);

                StringBuilder postData = new StringBuilder();
                postData.Append("req=" + dataFornat.UrlEncode(yeePayNotify.notify));//编码
                postData.Append("&sign=" + dataFornat.UrlEncode(yeePayNotify.sign)); //编码
                string strHttpPost = yeePay.HttpPost(yeePayConfig._verifyUrl, postData.ToString());

                sbLog.Append("\r\n 2. (3.7 确认转账)验证签名状态:" + strHttpPost);

                // 2. 验证签名
                if (strHttpPost.Contains("SUCCESS"))
                {
                    // 解析XML
                    XElement root = XElement.Parse(yeePayNotify.notify);
                    string bizType = root.Element("bizType").Value;
                    string code = root.Element("code").Value;
                    notify _baseNotify = null;

                    #region 3.业务处理

                    if (code == ((int)EnumCode.CodeTrue).ToString())
                    {
                        if (bizType == EnumNotifyBizType.TRANSACTION.ToEnumDesc())
                        {
                            // 3.7 状态  CONFIRM 表示解冻后完成资金划转 3.7 状态 CANCEL 表示解冻后取消转账
                            _baseNotify = yeePayNotify.notify.XmlDeserialize<notify>();

                            if (_baseNotify.status == EnumNotifyStatus.CONFIRM.ToEnumDesc() || _baseNotify.status == EnumNotifyStatus.CANCEL.ToEnumDesc())
                                reuslt = true;
                            else
                             sbLog.Append("\r\n  (3.7 确认转账)验证状态(status)失败");
                        }
                        else
                            sbLog.Append("\r\n  (3.7 确认转账)验证状态(bizType)失败");
                    }
                    else
                        sbLog.Append("\r\n (3.7 确认转账)验证状态(code)失败");
                    
                    #endregion
                }
            }
            catch (Exception e)
            {
                sbLog.Append("\r\n (3.7 确认转账)业务处理异常(catch）:" + e.ToString());
            }
            finally
            {
                sbLog.Append("\r\n (3.7 确认转账)返回状态:" + (reuslt == true ? "SUCCESS" : "FALE"));
                Log.RecordLog("YeePayNotifyController(Notify_Complete_Transaction)", sbLog.ToString(), false);
            }

            if (reuslt == true)
            {
                return "SUCCESS"; //成功
            }
            else
            {
                return "FALE"; //失败
            }
        }
    }
}