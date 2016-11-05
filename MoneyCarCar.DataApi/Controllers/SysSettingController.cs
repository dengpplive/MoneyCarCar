using MoneyCarCar.Commons.Enum;
using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.ResParam;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.YeePay.YeePayEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Commons;
using System.Data.SqlClient;

namespace MoneyCarCar.DataApi.Controllers
{
    public class SysSettingController : ApiController
    {
        BaseHelper helper = new BaseHelper();
        #region 日志
        /// <summary>
        /// 日志分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemLog> GetLogList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemLog> list = helper.GetPagerList<SystemLog>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemLog> mc = new ModelByCount<SystemLog>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }

        #endregion

        #region 字典
        /// <summary>
        /// 字典数据分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemDictionary> GetDicList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemDictionary> list = helper.GetPagerList<SystemDictionary>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemDictionary> mc = new ModelByCount<SystemDictionary>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> AddDic(SystemDictionary model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Add<SystemDictionary>(model);
                resultDto.ErrorMsg = "添加成功";
            }
            catch (Exception ex)
            {
                resultDto.ErrorCode = -1;
                resultDto.ErrorMsg = ex.Message;
            }
            return resultDto;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> UpdateDic(SystemDictionary model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                //要修改的字段数据
                List<string> fileds = new List<string>();
                fileds.Add("DicKey");
                fileds.Add("DicValue");
                fileds.Add("DicType");
                resultDto.ErrorCode = helper.Update<SystemDictionary>(model, fileds) ? 1 : 0;
                resultDto.ErrorMsg = "修改成功";
            }
            catch (Exception ex)
            {
                resultDto.ErrorCode = -1;
                resultDto.ErrorMsg = ex.Message;
            }
            return resultDto;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<string> DeleteDic(int Id)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (helper.DeleteById<SystemDictionary>(Id))
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> DeleteDicAll(RQIdModel<int> model)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (model.IdList.Count > 0
                    && helper.Delete<SystemDictionary>(string.Format(" Id in({0}) ", string.Join(",", model.IdList.ToArray()))))
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }
        [HttpGet]
        public BaseResultDto<List<SystemDictionary>> GetAllHelpType()
        {
            BaseResultDto<List<SystemDictionary>> result = new BaseResultDto<List<SystemDictionary>>();
            try
            {
                string sqlWhere = string.Format(" DicKey='{0}' ", EnumDictionary.HelpType);
                List<SystemDictionary> dic = helper.GetList<SystemDictionary>(sqlWhere);
                result.Tag = dic;
                result.ErrorMsg = "成功";
                result.ErrorCode = 1;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }
        [HttpGet]
        public BaseResultDto<List<SystemDictionary>> GetSmsTemplateOptions()
        {
            BaseResultDto<List<SystemDictionary>> result = new BaseResultDto<List<SystemDictionary>>();
            try
            {
                string sqlWhere = string.Format(" DicKey='{0}' ", EnumDictionary.SmsTemplate);
                List<SystemDictionary> dic = helper.GetList<SystemDictionary>(sqlWhere);
                result.Tag = dic;
                result.ErrorMsg = "成功";
                result.ErrorCode = 1;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }
        #endregion

        #region 提问帮助
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> AddHelp(SystemHelp model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Add<SystemHelp>(model);
                resultDto.ErrorMsg = "添加成功";
            }
            catch (Exception ex)
            {
                resultDto.ErrorCode = -1;
                resultDto.ErrorMsg = ex.Message;
            }
            return resultDto;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> UpdateHelp(SystemHelp model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Update<SystemHelp>(model) ? 1 : 0;
                resultDto.ErrorMsg = "修改成功";
            }
            catch (Exception ex)
            {
                resultDto.ErrorCode = -1;
                resultDto.ErrorMsg = ex.Message;
            }
            return resultDto;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<string> DeleteHelp(int Id)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (helper.DeleteById<SystemHelp>(Id))
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> DeleteHelpAll(RQIdModel<int> model)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (model.IdList.Count > 0
                    && helper.Delete<SystemHelp>(string.Format(" Id in({0}) ", string.Join(",", model.IdList.ToArray()))))
                {
                    result.ErrorMsg = "成功";
                    result.ErrorCode = 1;
                }
                else
                {
                    result.ErrorMsg = "失败";
                    result.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }
        /// <summary>
        /// 帮助分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemHelp> GetHelpList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemHelp> list = helper.GetPagerList<SystemHelp>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemHelp> mc = new ModelByCount<SystemHelp>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }


        #endregion

        #region 问题反馈
        /// <summary>
        /// 问题反馈分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemFeedback> GetFeedbackList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemFeedback> list = helper.GetPagerList<SystemFeedback>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemFeedback> mc = new ModelByCount<SystemFeedback>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }

        #endregion

        #region 请求记录日志

        /// <summary>
        /// 查询请求日志数据
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemRequestRecord> GetRequestRecordList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemRequestRecord> list = helper.GetPagerList<SystemRequestRecord>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemRequestRecord> mc = new ModelByCount<SystemRequestRecord>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
        /// <summary>
        /// 手动执行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> HandExec(RQHandExecDto Exec)
        {
            SystemRequestRecord model = Exec.RequestRecord;
            BaseResultDto<string> result = new BaseResultDto<string>();

            YeePay yeepay = new YeePay();
            YeePayOper yeePayOper = new YeePayOper();
            string strResult = "未处理";
            try
            {
                if (helper.IsExists<SystemRequestRecord>(string.Format(" Id={0} and RequestOperStatus=1", model.Id)))
                {

                    switch (model.RequestType)
                    {
                        case 1://注册
                            {

                            }
                            break;
                        case 2://充值
                            {
                                MoneyCarCar.Models.YeePay.RequestModel.Query query = new MoneyCarCar.Models.YeePay.RequestModel.Query();

                                query.mode = EnumMode.RECHARGE_RECORD.ToEnumDesc(); // 转款记录
                                query.platformUserNo = model.UserId.ToString();
                                query.requestNo = model.Id.ToString();

                                BaseResultDto<MoneyCarCar.Models.YeePay.Response.QUERY.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.QUERY.response>();
                                baseResultDtoResponse = yeepay.QUERY<MoneyCarCar.Models.YeePay.Response.QUERY.response>(query);

                                string str = baseResultDtoResponse.ErrorMsg;//XML 数据
                                // 反序列化
                                MoneyCarCar.Models.YeePay.Response.QUERY.RECHARGE_RECORD.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.QUERY.RECHARGE_RECORD.response>();

                                if (_response.status == EnumNotifyStatus.SUCCESS.ToEnumDesc())
                                {
                                    bool b_reuslt = yeePayOper.ToRecharge(model.UserId.ToString(), model.Id.ToString());
                                    if (b_reuslt)
                                    {
                                        strResult = "充值成功";
                                    }
                                    else
                                    {
                                        strResult = "未充值成功";
                                    }
                                }
                                else
                                {
                                    // 未充值成功
                                    strResult = "未充值成功";
                                }

                            }
                            break;
                        case 3:// 投资
                            {


                            }
                            break;
                        case 4://提现
                            {

                            }
                            break;
                        case 5://查询
                            {

                            }
                            break;
                        case 6://绑卡
                            {

                            }
                            break;
                        case 7://解绑
                            {

                            }
                            break;
                        case 8://结息
                            {
                                MoneyCarCar.Models.YeePay.RequestModel.Query query = new MoneyCarCar.Models.YeePay.RequestModel.Query();

                                query.mode = EnumMode.CP_TRANSACTION.ToEnumDesc(); // 转款记录
                                query.platformUserNo = model.UserId.ToString();
                                query.requestNo = model.Id.ToString();

                                BaseResultDto<MoneyCarCar.Models.YeePay.Response.QUERY.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.QUERY.response>();
                                baseResultDtoResponse = yeepay.QUERY<MoneyCarCar.Models.YeePay.Response.QUERY.response>(query);

                                string str = baseResultDtoResponse.ErrorMsg;//XML 数据
                                // 反序列化
                                MoneyCarCar.Models.YeePay.Response.QUERY.CP_TRANSACTION.response _response = baseResultDtoResponse.ErrorMsg.XmlDeserialize<MoneyCarCar.Models.YeePay.Response.QUERY.CP_TRANSACTION.response>();

                                if (_response.status == EnumNotifyStatus.DIRECT.ToEnumDesc())
                                {
                                    bool b_reuslt = yeePayOper.Direct_Transaction(model.Id.ToString());
                                    if (b_reuslt)
                                    {
                                        strResult = "结息成功";
                                    }
                                    else
                                    {
                                        strResult = "结息未成功";
                                    }
                                }
                                else
                                {
                                    //结息未成功
                                    strResult = "结息未成功";
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    result.IsSeccess = true;
                    result.ErrorCode = 1;
                    result.ErrorMsg = strResult;
                }
                else
                {
                    result.IsSeccess = true;
                    result.ErrorCode = 1;
                    result.ErrorMsg = "状态已处理";
                }
                SystemLog log = new SystemLog();
                log.OperatorUserId = Exec.OperatorUserId;
                log.OperatorUserName = Exec.OperatorUserName;
                log.OperatorType = 4;
                log.BusinessType = "手动同步数据";
                log.OperatorTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                log.OperatorContent = Exec.OperatorContent + " " + strResult;
                log.OperatorIP = Exec.IP;
                //添加日志
                helper.Add<SystemLog>(log);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }

        #endregion

        #region 首页数据
        /// <summary>
        /// 首页的统计数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<StatisticalDto> GetStatistical()
        {
            BaseResultDto<StatisticalDto> result = new BaseResultDto<StatisticalDto>();
            try
            {
                RQProcParam proc = new RQProcParam();
                proc.ProcName = "proc_Statistical";
                StatisticalDto statisticalDto = helper.GetDataByProc<StatisticalDto>(proc).FirstOrDefault();
                result.Tag = statisticalDto;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.ErrorCode = -1;
            }
            return result;
        }

        /// <summary>
        /// 账户查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>> GetPlatformAcountQuery()
        {
            BaseResultDto<BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>> resultDto = new BaseResultDto<BaseResultDto<Models.YeePay.Response.ACCOUNT_INFO.response>>();
            try
            {
                YeePay yeepay = new YeePay();
                MoneyCarCar.Models.YeePay.RequestModel.Account_Info account_Info = new MoneyCarCar.Models.YeePay.RequestModel.Account_Info();
                BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response> baseResultDtoResponse = new BaseResultDto<MoneyCarCar.Models.YeePay.Response.ACCOUNT_INFO.response>();
                baseResultDtoResponse = yeepay.ACCOUNT_INFO(account_Info);
                if (baseResultDtoResponse.IsSeccess)
                {
                    resultDto.ErrorCode = 1;
                    resultDto.IsSeccess = true;
                }
                resultDto.ErrorMsg = baseResultDtoResponse.ErrorMsg;//XML 数据
                resultDto.Tag = baseResultDtoResponse;
            }
            catch (Exception ex)
            {
                resultDto.ErrorCode = -1;
                resultDto.ErrorMsg = ex.Message;
            }
            return resultDto;
        }

        #endregion
    }
}
