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
    public class NewsController : ApiController
    {
        BaseHelper helper = new BaseHelper();
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResultDto<SystemNews> GetNews(int Id)
        {
            BaseResultDto<SystemNews> resultDto = new BaseResultDto<SystemNews>();
            try
            {
                resultDto.Tag = helper.GetModelById<SystemNews>(Id);
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
        /// 添加 API捕获异常数据json返回
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> Add(SystemNews model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Add<SystemNews>(model);
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
        /// 修改单条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultDto<string> Update(SystemNews model)
        {
            BaseResultDto<string> resultDto = new BaseResultDto<string>();
            try
            {
                resultDto.ErrorCode = helper.Update<SystemNews>(model) ? 1 : 0;
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
                if (helper.DeleteById<SystemNews>(Id))
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
                    && helper.Delete<SystemNews>(string.Format(" Id in({0}) ", string.Join(",", model.IdList.ToArray()))))
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
        public ModelByCount<SystemNews> GetList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemNews> list = helper.GetPagerList<SystemNews>(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemNews> mc = new ModelByCount<SystemNews>();
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
    }
}
