using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{category}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
     
            );
            routes.MapRoute(
                name: "New",
                url: "tin-tuc/{newCategoryId}",
                defaults: new { controller = "New", action = "Index", id = UrlParameter.Optional, newOrBlog=1 }

            );
            routes.MapRoute(
                name: "NewDetails",
                url: "Tin-Tuc-Blog/{id}",
                defaults: new { controller = "New", action = "Details", id = UrlParameter.Optional, newOrBlog = 1 }

            );
            routes.MapRoute(
                name: "NewAbout",
                url: "Gioi-Thieu/{id}",
                defaults: new { controller = "New", action = "About", id = UrlParameter.Optional }

            );
            routes.MapRoute(
               name: "Blog",
               url: "Blog/{newCategoryId}/{newOrBlog}",
               defaults: new { controller = "New", action = "Index", id = UrlParameter.Optional, newOrBlog=2 }

           );
            routes.MapRoute(
                name: "Details1",
                url: "chi-tiet-san-pham/{id}",
                defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional }

            );
            routes.MapRoute(
                name: "HotProduct",
                url: "san-pham-hot/{id}",
                defaults: new { controller = "Product", action = "HotProduct", id = UrlParameter.Optional }

            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
