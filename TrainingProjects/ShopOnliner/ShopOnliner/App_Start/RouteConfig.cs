using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnliner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Catalog",
              url: "Home/Catalog",
              defaults: new { controller = "Home", action = "MainCatalog" }
          );

            routes.MapRoute(
             name: "Info",
             url: "Home/Catalog/info/{id}",
             defaults: new { controller = "Home", action = "Info"},
             constraints : new {id = @"\d+"}
         );

            routes.MapRoute(
               name: "Items",
               url: "Home/Catalog/{type}/{page}",
               defaults: new { controller = "Home", action = "Catalog" , page=@"\d+" },
               constraints: new {page = @"\d+"}
           );

           routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );

           routes.MapRoute(
               name: "CatchAll",
               url: "{*route}",
               defaults: new { controller= "Home", action = "Invalid" }
           );

           
        }
    }
}