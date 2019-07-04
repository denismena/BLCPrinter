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
        private BLCEntities db = new BLCEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            #region populate incasari
            //var c1 = from c in db.CONTRACTEs
            //         where c.C_AVANS_DATA.HasValue && c.C_AVANS.HasValue
            //         select new
            //         {
            //             c.C_ID,
            //             c.C_NUMAR,
            //             c.C_DATA,
            //             c.C_AVANS_DATA,
            //             c.C_AVANS,
            //             c.C_FACTURA,
            //             c.C_CHITANTA,
            //             c.C_DATA_DIFERENTA
            //             ,
            //             P_ID = c.PERSOANE.P_ID
            //             ,
            //             P_NUME = c.PERSOANE.P_NUME
            //             ,
            //             P_PRENUME = c.PERSOANE.P_PRENUME
            //             ,
            //             P_TEL = c.PERSOANE.P_TEL
            //         };

            //var c2 = from c in db.CONTRACTEs
            //         where c.C_AVANS2_DATA.HasValue && c.C_AVANS2.HasValue
            //         select new
            //         {
            //             c.C_ID,
            //             c.C_NUMAR,
            //             c.C_DATA,
            //             C_AVANS_DATA = c.C_AVANS2_DATA,
            //             C_AVANS = c.C_AVANS2,
            //             C_FACTURA = c.C_FACTURA2,
            //             C_CHITANTA = c.C_CHITANTA2,
            //             C_DATA_DIFERENTA = c.C_DATA_DIFERENTA2
            //             ,
            //             P_ID = c.PERSOANE.P_ID
            //             ,
            //             P_NUME = c.PERSOANE.P_NUME
            //             ,
            //             P_PRENUME = c.PERSOANE.P_PRENUME
            //             ,
            //             P_TEL = c.PERSOANE.P_TEL
            //         };
            //var c3 = from c in db.CONTRACTEs
            //         where c.C_AVANS3_DATA.HasValue && c.C_AVANS3.HasValue
            //         select new
            //         {
            //             c.C_ID,
            //             c.C_NUMAR,
            //             c.C_DATA,
            //             C_AVANS_DATA = c.C_AVANS3_DATA,
            //             C_AVANS = c.C_AVANS3,
            //             C_FACTURA = c.C_FACTURA3,
            //             C_CHITANTA = c.C_CHITANTA3,
            //             C_DATA_DIFERENTA = c.C_DATA_DIFERENTA3
            //             ,
            //             P_ID = c.PERSOANE.P_ID
            //             ,
            //             P_NUME = c.PERSOANE.P_NUME
            //             ,
            //             P_PRENUME = c.PERSOANE.P_PRENUME
            //             ,
            //             P_TEL = c.PERSOANE.P_TEL
            //         };
            //var contractes = c1.Union(c2).Union(c3);

            //var ss = from c in contractes
            //         group c by c.C_ID into g
            //         select new { ID = g.Key, DATA = g.Max(x=>x.C_AVANS_DATA) };

            //var incasariModel = new List<IncasariList>();
            //foreach (var inc in ss)
            //{
            //    incasariModel.Add(new IncasariList
            //    {
            //        C_ID = inc.C_ID,
            //        C_NUMAR = inc.Scadenta.,
            //        C_DATA = inc.C_DATA,
            //        C_AVANS_DATA = inc.C_AVANS_DATA,
            //        C_AVANS = inc.C_AVANS,
            //        C_FACTURA = inc.C_FACTURA,
            //        C_CHITANTA = inc.C_CHITANTA,
            //        C_DATA_DIFERENTA = inc.C_DATA_DIFERENTA,
            //        P_ID = inc.P_ID,
            //        P_NUME = inc.P_NUME,
            //        P_PRENUME = inc.P_PRENUME,
            //        P_TEL = inc.P_TEL
            //    });
            //}

            //ViewBag.ListIncasari = incasariModel;

            //var sql = from c in contractes
            //          join p in db.PERSOANEs on c.P_ID equals p.P_ID
            //          select new
            //          {
            //              c.C_ID,
            //              c.C_NUMAR,
            //              c.C_DATA,
            //              c.C_AVANS_DATA,
            //              c.C_AVANS,
            //              c.C_FACTURA,
            //              c.C_CHITANTA,
            //              c.C_DATA_DIFERENTA,
            //              p.P_NUME,
            //              p.P_PRENUME,
            //              p.P_TEL
            //          };
            #endregion
            var scadente = from c in db.CONTRACTEs
                           where (c.C_AVANS.HasValue && c.C_DATA_DIFERENTA.HasValue && !c.C_AVANS2.HasValue)
                           || (c.C_AVANS2.HasValue && c.C_DATA_DIFERENTA2.HasValue && !c.C_AVANS3.HasValue)
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
    }
}
