using AnyWay.Apps.Core.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace AnyWay.Apps.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //启用压缩
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //注入 Ioc
            var container = new UnityContainer();
            DependencyRegisterType.Container(ref container, HttpContext.Current.Server.MapPath("~/Config/Unity/Unity.config"));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //配置log4net配置文件
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(HttpContext.Current.Server.MapPath("~/Config/Log4net/log4Net.config")));
        }
    }
}
