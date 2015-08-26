using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SbaliMvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.activeTab = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            ViewBag.activeTab = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.activeTab = "Contact";
            return View();
        }
    }
}
