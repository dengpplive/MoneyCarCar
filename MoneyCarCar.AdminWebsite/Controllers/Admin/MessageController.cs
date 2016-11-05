using MoneyCarCar.AdminWebsite.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite.Controllers.Admin
{
    /// <summary>
    /// 站内消息
    /// </summary>
    public class MessageController : BaseController
    {
        //
        // GET: /Message/

        public ActionResult Index()
        {
            return View();
        }

    }
}
