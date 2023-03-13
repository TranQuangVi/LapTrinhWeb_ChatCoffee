using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChatCoffee
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ChatCoffee.Controllers" }
            );
            routes.MapRoute(
     name: "ChiTietSanPham",
     url: "chi-tiet/{alias}-p{id}",
     defaults: new { controller = "COFFEEs", action = "Detail", alias = UrlParameter.Optional },
     namespaces: new[] { "ChatCoffee.Controllers" }
 );
            routes.MapRoute(
      name: "CategoryProduct",
      url: "danh-muc-san-pham/{alias}-{id}",
      defaults: new { controller = "COFFEEs", action = "Home", id = UrlParameter.Optional },
      namespaces: new[] { "ChatCoffee.Controllers" }
  );
        }
    }
}
