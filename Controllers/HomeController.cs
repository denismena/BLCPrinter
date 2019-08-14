using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLCPrinter.Models;

namespace BLCPrinter.Controllers
{
    public class HomeController : Controller
    {
        private BLCEntities1 db = new BLCEntities1();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";            
            var scadente = from c in db.CONTRACTE
                           where c.C_PRET.Value != ((c.C_AVANS ?? 0) + (c.C_AVANS2 ?? 0) + (c.C_AVANS3 ?? 0))
                           //where (c.C_AVANS.HasValue && c.C_DATA_DIFERENTA.HasValue && !c.C_AVANS2.HasValue)
                           //|| (c.C_AVANS2.HasValue && c.C_DATA_DIFERENTA2.HasValue && !c.C_AVANS3.HasValue)
                           select c;
            return View(scadente);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["Email"] = null;
            Session["UserId"] = null;
            ViewBag.UserId = "-1";

            return RedirectToAction("Login", "Account");
        }
    }
}
