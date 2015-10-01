using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationMembership.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Online Document Management System!";

            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            return View();
        }
    }
}
