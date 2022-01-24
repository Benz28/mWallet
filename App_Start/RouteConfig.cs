using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mWallet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                            name: "Expenses",
                            url: "Expenses/{action}/{id}",
                            defaults: new { controller = "Expenses", action = "Expenses", id = UrlParameter.Optional }
                        );

            routes.MapRoute(
                            name: "Income",
                            url: "Income/{action}/{id}",
                            defaults: new { controller = "Income", action = "Income", id = UrlParameter.Optional }
                        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Balance", id = UrlParameter.Optional }
            );


        }
    }
}
