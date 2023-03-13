using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ispan_AspNetWeb_firstPractice
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 這個是惡搞{action}/{controller}
            //routes.MapRoute(
            //    name: "Eddy",
            //    url: "{action}/{controller}/{id}",
            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
