using MoneyCarCar.Commons;
using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyCarCar.DataApi.Controllers
{
    public class SmsController : ApiController
    {
        private SMSOper api = new SMSOper();

        private bool isInit = false;

        public SmsController()
        {
            //ip格式如下，不带https://
            isInit = api.init("app.cloopen.com", "8883");
            api.setAccount("aaf98f894a85eee5014a904c21e703a2", "d3585e866da54d108c4dc26f38c929a3");//主账号,主账号令牌
            api.setAppId("aaf98f894b00309b014b015057e90142");//应用ID
        }
        BaseHelper helper = new BaseHelper();

        #region 短信模板
        /// <summary>
        /// 查询一条短信模板
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<SystemSmsTemplate> GetTemplateById(int Id)
        {
            BaseResultDto<SystemSmsTemplate> resultDto = new BaseResultDto<SystemSmsTemplate>();
            try
            {
                resultDto.Tag = helper.GetModelById<SystemSmsTemplate>(Id);
                resultDto.ErrorCode = 1;
                resultDto.ErrorMsg = "查询成功";
            }
            catch (Exception ex)
            {
                resultDto.ErrorMsg = ex.Message;
                resultDto.ErrorCode = -1;
            }
            return resultDto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> Add(SystemSmsTemplate model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Add<SystemSmsTemplate>(model);
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
        public BaseResultDto<string> Update(SystemSmsTemplate model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Update<SystemSmsTemplate>(model) ? 1 : 0;
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
        public BaseResultDto<string> Delete(int Id)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (helper.DeleteById<SystemSmsTemplate>(Id))
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
        public BaseResultDto<string> DeleteAll(RQIdModel<int> model)
        {
            BaseResultDto<string> result = new BaseResultDto<string>();
            try
            {
                if (model.IdList.Count > 0
                    && helper.Delete<SystemSmsTemplate>(string.Format(" Id in({0}) ", string.Join(",", model.IdList.ToArray()))))
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
        /// 分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemSmsTemplate> GetTemplateList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemSmsTemplate> list = helper.GetPagerList<SystemSmsTemplate>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemSmsTemplate> mc = new ModelByCount<SystemSmsTemplate>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
        #endregion

        #region 发送短信记录
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [HttpPost]
        public ModelByCount<SystemSmsRecord> GetRecordList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemSmsRecord> list = helper.GetPagerList<SystemSmsRecord>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemSmsRecord> mc = new ModelByCount<SystemSmsRecord>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
        #endregion

        #region 发送短信
        [HttpPost]
        public ResponseInfo SendTemplateSMS(SendInfo model)
        {
            return api.SendTemplateSMS(model);
        }
        #endregion
    }
}
