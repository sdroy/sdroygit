using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OOTS.Classes;

namespace OOTS.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();           
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

      

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated && !Request.Path.Contains("/Account/UserExpiry") && !Request.Path.Contains("Content") && !Request.Path.Contains("Scripts"))
            {
                DateTime expirydate = UtilityOperations.GetExpiryDate();
                //if (expirydate < DateTime.Now)
                //{
                //    Response.Redirect("/Account/UserExpiry");
                //}
            } 
        }

      
    }
}