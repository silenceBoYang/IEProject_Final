using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEProject_Final.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Place()
        {
            return View();
        }

        //Gets the bool data and passes to Housings/Results
        [HttpPost]
        public ActionResult Place(bool[] Blanks)
        {
            Debug.WriteLine("In Place CSHTML");
            Debug.WriteLine(Blanks[0] + "," + Blanks[1] + "," + Blanks[2] + "," + Blanks[3]);
            //TempData["preferences"] = Blanks;
            return RedirectToAction("Results", "Housings", new { @bool0 = Blanks[0], @bool1 = Blanks[1], @bool2 = Blanks[2], @bool3 = Blanks[3] });
        }
    }
}