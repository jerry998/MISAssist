using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MISAssist.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "仁愛之家．資訊室 ASP.NET MVC 應用程式.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Time()
        {
            var time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            return Content(time);
        }
    }
}