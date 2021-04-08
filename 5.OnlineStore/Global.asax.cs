﻿using OnlineStore.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IocConfig.Config();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;
            DbInitializer.Initialize();
        }
    }
}
