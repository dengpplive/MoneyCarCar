using BeIT.MemCached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyCarCar.AdminWebsite.Controllers.CommHelper
{
    public class CacheManage
    {
        public CacheManage()
        {

        }
        public MemcachedClient GetInstance()
        {
            return MemcachedClient.GetInstance("myCache");
        }


    }
}