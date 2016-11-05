using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.AdminWebsite
{
    public class ErrorHandlingActionInvoker : ControllerActionInvoker
    {
        private readonly IExceptionFilter filter;

        public ErrorHandlingActionInvoker(IExceptionFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException("Exception filter is missing");

            this.filter = filter;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filterInfo = base.GetFilters(controllerContext, actionDescriptor);
            filterInfo.ExceptionFilters.Add(this.filter);
            return filterInfo;
        }
    }
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;
            try
            {
                Controller c = ObjectFactory.GetInstance(controllerType) as Controller;
                //当返回一个错误页面，View一级异常会被触发
                c.ActionInvoker = new ErrorHandlingActionInvoker(new HandleErrorAttribute());
                return c;
            }
            catch (StructureMapException ex)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
                throw;
            }
            //return base.GetControllerInstance(requestContext, controllerType);
        }
    }
    public class Setup
    {
        public static void Initialize()
        {
            //初始化
            ObjectFactory.Initialize(x =>
            {
                //注册接口的实现
                //x.For<ITest>().Singleton().Use<B>();
                //.....

            });
            //注册StructureMapControllerFactory以代替DefaultControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}