using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SbaliMvcApplication.Controllers
{
    public class ServicesController : Controller
    {
        //
        // GET: /Services/

        public ActionResult Index()
        {
            ViewBag.activeTab = "Services";
            return View();
        }

    }
}
