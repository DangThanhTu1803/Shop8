using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop8
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatirle}-{id}",
                defaults: new { controller = "Categories", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "Shop8.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatirle}-{id}",
                defaults: new { controller = "Categories", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "Shop8.Controllers" }
            );
            routes.MapRoute(
               name: "Register",
               url: "Dang-ky",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "Shop8.Controllers" }
           );
            routes.MapRoute(
               name: "Login",
               url: "Dang-nhap",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "Shop8.Controllers" }
           );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Shop8.Controllers" }
            );
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "Shop8.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"Shop8.Controllers"}
            );
        }
    }
}
