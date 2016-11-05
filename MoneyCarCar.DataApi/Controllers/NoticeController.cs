using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyCarCar.DataApi.Controllers
{
    public class NoticeController : ApiController
    {
        BaseHelper helper = new BaseHelper();
        /// <summary>
        /// 查询一条公告
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<SystemNotice> GetNotice(int Id)
        {
            BaseResultDto<SystemNotice> resultDto = new BaseResultDto<SystemNotice>();
            try
            {
                resultDto.Tag = helper.GetModelById<SystemNotice>(Id);
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
        public BaseResultDto<string> Add(SystemNotice model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Add<SystemNotice>(model);
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
        public BaseResultDto<string> Update(SystemNotice model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Update<SystemNotice>(model) ? 1 : 0;
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
                if (helper.DeleteById<SystemNotice>(Id))
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
                    && helper.Delete<SystemNotice>(string.Format(" Id in({0}) ", string.Join(",", model.IdList.ToArray()))))
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
        public ModelByCount<SystemNotice> GetList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemNotice> list = helper.GetPagerList<SystemNotice>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemNotice> mc = new ModelByCount<SystemNotice>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
    }
}
