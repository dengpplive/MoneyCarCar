using MoneyCarCar.Commons.Enum;
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
using MoneyCarCar.Commons;
using MoneyCarCar.DAL.DataGet;
using System.Data;
using MoneyCarCar.Models.Statisticals.Parameter;

namespace MoneyCarCar.DataApi.Controllers
{
    public class WebSiteServiceController : ApiController
    {
        WebSiteDatasOper oper = new WebSiteDatasOper();

        //前台首页数据
        [HttpGet]
        public DataTableCollection GetIndexDatas()
        {
            return oper.GetIndexDatas();
        }
        [HttpGet]
        //分页帮助
        public ModelByCount<SystemHelp> GetHelps(string key = "", int pageIndex = 1, int pageSize = 20)
        {
            return oper.GetHelps(key, pageIndex, pageSize);
        }
    }
}
