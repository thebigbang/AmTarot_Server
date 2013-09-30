using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using database;

namespace Server
{   /// <summary>
    /// Informations sur les credentials serveurs:
    /// username: amtarot
    /// pwd: SuperSupinf0AmTar0tPr0jectAwes0wmeP00ny
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Thread _t = new Thread(() =>
        {
            while (true)
            {
                DateTime time = DateTime.Now.AddMinutes(10);

                foreach (Anon anon in Anon.GetAll())
                {
                    if (DateTime.Compare(anon.LastLogin, time) < 0)
                    {
                        Anon.LogOut(anon.Id);
                    }
                }
                Thread.Sleep(600000);
            }
        });
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            if (!_t.IsAlive)
            {
          //      _t.Start();
            }
        }
    }
}