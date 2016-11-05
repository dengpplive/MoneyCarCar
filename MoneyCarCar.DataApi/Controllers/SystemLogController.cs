using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.DAL;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;

namespace MoneyCarCar.DataApi.Controllers
{
    public class SystemLogController : ApiController
    {
        SystemLogOper helper = new SystemLogOper();
        //日志分页查询
        [HttpPost]
        public ModelByCount<SystemLog> GetList(RQPagerDto pager)
        {
            int TotalCount = 0;
            List<SystemLog> list = helper.GetPagerList(out TotalCount, pager.PageSize, pager.PageIndex, pager.Where, pager.QueryFileds, pager.OrderBy);
            ModelByCount<SystemLog> mc = new ModelByCount<SystemLog>();
            mc.PageIndex = pager.PageIndex;
            mc.PageSize = pager.PageSize;
            mc.AllCount = TotalCount;
            mc.ListAll = list;
            return mc;
        }
    }
}
