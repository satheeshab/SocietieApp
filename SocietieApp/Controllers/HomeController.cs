using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocietieApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "365Societies - all that you need to maintain for your Society is here.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can submit your query. We will reach out to you soon.";

            return View();
        }
    }
}
