using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MoneyCarCar.DataApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "oneAPI",
                routeTemplate: "{controller}/{action}/{value}",
                defaults: new { controller = "Test", value = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "twoAPI",
               routeTemplate: "{controller}/{action}/{model}",
               defaults: new { controller = "Test" }
           );
        }
    }
}