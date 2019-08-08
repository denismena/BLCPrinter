using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLCPrinter.Models;
using System.IO;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BLCPrinter.Controllers
{
    public class ContractsController : Controller
    {
        private BLCEntities db = new BLCEntities();

        //
        // GET: /Contracts/

        public ActionResult Autocomplete(string term)
        {
            var model = (from p in db.PERSOANEs
                         where p.P_NUME.Contains(term) || p.P_PRENUME.Contains(term)
                         select new { label = p.P_NUME + " " + p.P_PRENUME + "(" + p.P_CNP + ")", value = p.P_ID }
                         ).Take(10);
            return Json(model, JsonRequestBehavior.AllowGet);                     
        }

        public ActionResult Index()
        {
            var contractes = db.CONTRACTEs.Include(c => c.PERSOANE).OrderByDescending(o=>o.C_DATA);
            return View(contractes.ToList());
        }

        //
        // GET: /Contracts/Details/5

        public ActionResult Details(int id = 0)
        {
            CONTRACTE contracte = db.CONTRACTEs.Find(id);
            
            string cale = Request.PhysicalApplicationPath + @"Temp\" + id + ".pdf";
            PdfReader reader = new PdfReader(Request.PhysicalApplicationPath + @"Temp\Contract.pdf");
            PdfStamper stamp1 = new PdfStamper(reader, new FileStream(cale, FileMode.Create));
            AcroFields form1 = stamp1.AcroFields;

            var cntr = (db.CONTRACTEs.Include(c => c.PERSOANE).Include(c=>c.PERSOANE.LIBRARIE).Where(c=>c.C_ID==id)).ToList();
            var listaServicii = from ls in db.SERVICII_CONTRACT.Include(s => s.LIBRARIE)
                                where ls.SC_C_ID == id
                                select ls.LIBRARIE.L_NUME;
            string pdfS= string.Empty;
            if (listaServicii.Count() > 0)
            {
                foreach (var s in listaServicii)
                    pdfS += s.ToString() + ",";
                pdfS = pdfS.Remove(pdfS.Length - 1);
            }

            var row = cntr[0];
            try {                
                form1.SetField("Nr", row.C_NUMAR.ToString());
                form1.SetField("DataZi", row.C_DATA.Day.ToString());
                form1.SetField("DataLuna", row.C_DATA.Month.ToString());
                form1.SetField("Nume", row.PERSOANE.P_NUME);
                form1.SetField("Prenume", row.PERSOANE.P_PRENUME);
                form1.SetField("CNP", row.PERSOANE.P_CNP);
                form1.SetField("CI", row.PERSOANE.LIBRARIE.L_NUME + " " + row.PERSOANE.P_ID_VALUE);
                form1.SetField("DataNasterii", row.PERSOANE.P_DATA_NASTERII.HasValue ? Convert.ToDateTime(row.PERSOANE.P_DATA_NASTERII).ToString("dd/MMM/yyyy") : "");
                form1.SetField("Adresa", row.PERSOANE.P_ADRESA + " " + row.PERSOANE.P_ORAS + " " + row.PERSOANE.P_JUDET);
                form1.SetField("Telefon", row.PERSOANE.P_TEL);
                form1.SetField("Servicii", pdfS + " " +
                    (!string.IsNullOrEmpty(row.C_TARA) ? row.C_TARA : "") + " " +
                    (!string.IsNullOrEmpty(row.C_ORAS) ? row.C_ORAS : "") + " " +
                    (row.C_DE_LA_DATA.HasValue ? Convert.ToDateTime(row.C_DE_LA_DATA).ToString("dd/MMM/yyyy") : "") + " " +
                    (row.C_DE_LA_DATA.HasValue ? " - " + Convert.ToDateTime(row.C_PANA_LA_DATA).ToString("dd/MMM/yyyy") : "") + " " +
                    (!string.IsNullOrEmpty(row.C_ORAS) ? "Hotel: " + row.C_HOTEL : "") + " " +
                    (row.C_HOTEL_STELE.HasValue ? row.C_HOTEL_STELE.Value + "*" : "")
                    );
                form1.SetField("Servicii1", (row.C_NR_PERS.HasValue ? row.C_NR_PERS.Value.ToString() + " persoane " : "") +
                    (row.C_NR_ADULTI.HasValue ? "(" + row.C_NR_ADULTI.Value.ToString() + " adulti " : "") +
                    (row.C_NR_COPII.HasValue ? " + " + row.C_NR_COPII.Value.ToString() + " copii " : "") +
                    (row.C_NR_ADULTI.HasValue ? ")" : "")
                    );
                form1.SetField("Servicii2", row.C_MENTIUNI);
                form1.SetField("Pret", row.C_PRET.HasValue ? Convert.ToDecimal(row.C_PRET).ToString("F2") + " " + row.LIBRARIE.L_NUME : "");
                form1.SetField("Avans", row.C_AVANS.HasValue ? Convert.ToDecimal(row.C_AVANS).ToString("F2") + " " + row.LIBRARIE.L_NUME : "");
                form1.SetField("AvansData", row.C_DATA_DIFERENTA.HasValue ? Convert.ToDateTime(row.C_DATA_DIFERENTA).ToString("dd/MMM/yyyy") : "");
                form1.SetField("Factura", row.C_FACTURA);
                form1.SetField("Chitanta", row.C_CHITANTA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stamp1.Close();
                reader.Close();
            }

            return File("~/Temp/" + id + ".pdf", "application/pdf");
        }

        //
        // GET: /Contracts/Create

        public ActionResult Create()
        {
            var servType = from lib in db.LIBRARIEs
                           where lib.L_TIP == "SERVICIU"
                         select lib;
            ViewBag.ListaServicii = servType.ToList();
            var idType = from lib in db.LIBRARIEs
                         where lib.L_TIP == "MONEDA"
                         select lib;
            ViewBag.C_MONEDA = new SelectList(idType, "L_ID", "L_NUME");
            //var contractServ = from cs in db.SERVICII_CONTRACT
            //                   select new SERVICII_CONTRACT { SC_ID = cs.SC_ID, SC_C_ID = cs.SC_C_ID, SC_S_ID = cs.SC_S_ID, CONTRACTE = cs.CONTRACTE, LIBRARIE = cs.LIBRARIE };

            //CONTRACTE contract = db.CONTRACTEs
            //        .Include(c => c.LIBRARIE);
            CONTRACTE cntr = new CONTRACTE();
            cntr.C_DATA = DateTime.Now;
            cntr.C_AVANS_DATA = DateTime.Now;
            if (db.CONTRACTEs.Count() == 0) cntr.C_NUMAR = 1;
            else cntr.C_NUMAR = db.CONTRACTEs.Max(c => c.C_NUMAR) + 1;
            return View(cntr);
        }

        //
        // POST: /Contracts/Create

        [HttpPost]
        public ActionResult Create(CONTRACTE contracte, FormCollection formCollection, string[] selectedServices)
        {
            if (ModelState.IsValid)
            {
                db.CONTRACTEs.Add(contracte);
                db.SaveChanges();
                if (selectedServices != null)
                {
                    foreach (string s in selectedServices)
                    {
                        SERVICII_CONTRACT cntr = new SERVICII_CONTRACT();
                        cntr.SC_C_ID = contracte.C_ID;
                        cntr.SC_S_ID = Convert.ToInt32(s);
                        db.SERVICII_CONTRACT.Add(cntr);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.C_PERSOANA_ID = new SelectList(db.PERSOANEs, "P_ID", "P_NUME", contracte.C_PERSOANA_ID);
            var servType = from lib in db.LIBRARIEs
                           where lib.L_TIP == "SERVICIU"
                           select lib;
            ViewBag.ListaServicii = servType.ToList();
            var idType = from lib in db.LIBRARIEs
                         where lib.L_TIP == "MONEDA"
                         select lib;
            ViewBag.C_MONEDA = new SelectList(idType, "L_ID", "L_NUME", contracte.C_MONEDA);
            return View(contracte);
        }

        //
        // GET: /Contracts/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CONTRACTE contracte = db.CONTRACTEs.Find(id);
            if (contracte == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_PERSOANA_ID = new SelectList(db.PERSOANEs, "P_ID", "P_NUME", contracte.C_PERSOANA_ID);
            var idType = from lib in db.LIBRARIEs
                         where lib.L_TIP == "MONEDA"
                         select lib;
            ViewBag.C_MONEDA = new SelectList(idType, "L_ID", "L_NUME", contracte.C_MONEDA);

            //SERVICII_CONTRACT cntr = db.SERVICII_CONTRACT
            //    .Include(i => i.LIBRARIE)
            //    .Where(i => i.SC_C_ID == id)
            //    .Single();
            PopulateAssignedCourseData(id);
            return View(contracte);
        }

        private void PopulateAssignedCourseData(int id)
        {
            var servType = from lib in db.LIBRARIEs
                           where lib.L_TIP == "SERVICIU"
                           select lib;
            var serviceContract = from serv in db.SERVICII_CONTRACT
                                  where serv.SC_C_ID == id
                                  select serv.LIBRARIE.L_ID;
            var viewModel = new List<SelectedServiceContract>();
            foreach (var serv in servType)
            {
                viewModel.Add(new SelectedServiceContract
                {
                    serviceId = serv.L_ID,
                    serviceTitle = serv.L_NUME,
                    selected = serviceContract.Contains(serv.L_ID)
                });
            }
            ViewBag.SelectService = viewModel;
        }

        //
        // POST: /Contracts/Edit/5

        [HttpPost]
        public ActionResult Edit(CONTRACTE contracte, FormCollection formCollection, string[] selectedServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contracte).State = EntityState.Modified;
                db.SaveChanges();
                //updateCoursesService(selectedServices, contracte);

                var oldServ = from ss in db.SERVICII_CONTRACT
                              where ss.SC_C_ID == contracte.C_ID
                              select ss;
                foreach (var ss in oldServ)
                {
                    SERVICII_CONTRACT cc = db.SERVICII_CONTRACT.Find(ss.SC_ID);
                    db.SERVICII_CONTRACT.Remove(cc);
                }
                foreach (string s in selectedServices)
                {
                    SERVICII_CONTRACT cntr = new SERVICII_CONTRACT();
                    cntr.SC_C_ID = contracte.C_ID;
                    cntr.SC_S_ID = Convert.ToInt32(s);
                    db.SERVICII_CONTRACT.Add(cntr);
                }
                //if (db.Entry(contracte).State == EntityState.Modified) 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_PERSOANA_ID = new SelectList(db.PERSOANEs, "P_ID", "P_NUME", contracte.C_PERSOANA_ID);
            //ViewBag.C_MONEDA = new SelectList(idType, "L_ID", "L_NUME", contracte.C_MONEDA);
            return View(contracte);
        }

        private void updateCoursesService(string[] selectedServices, CONTRACTE contracte)
        {
            var servType = from lib in db.LIBRARIEs
                           where lib.L_TIP == "SERVICIU"
                           select lib;
            var selectedServicesHS = new HashSet<string>(selectedServices);
            foreach (var s in servType)
            {
                SERVICII_CONTRACT cntr = new SERVICII_CONTRACT();
                cntr.SC_C_ID = contracte.C_ID;
                cntr.SC_S_ID = Convert.ToInt32(s.L_ID);

                if (selectedServicesHS.Contains(s.L_ID.ToString())) db.SERVICII_CONTRACT.Add(cntr);
                else
                {
                    //var idSel = from ids in db.SERVICII_CONTRACT
                    //            where ids.SC_C_ID == contracte.C_ID && ids.SC_S_ID == Convert.ToInt32(s.L_ID)
                    //            select ids.SC_ID;
                    db.SERVICII_CONTRACT.Remove(cntr);
                }
            }
            db.SaveChanges();
        }
        //
        // GET: /Contracts/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CONTRACTE contracte = db.CONTRACTEs.Find(id);
            if (contracte == null)
            {
                return HttpNotFound();
            }
            return View(contracte);
        }

        //
        // POST: /Contracts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTRACTE contracte = db.CONTRACTEs.Find(id);
            db.CONTRACTEs.Remove(contracte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Incasari(DateTime? fromDate, DateTime? toDate)
        {            
            #region populate incasari
            var c1 = from c in db.CONTRACTEs
                     where c.C_AVANS_DATA.HasValue && c.C_AVANS.HasValue
                     select new
                     {
                         c.C_ID,
                         c.C_NUMAR,
                         c.C_DATA,
                         c.C_AVANS_DATA,
                         c.C_AVANS,
                         c.C_FACTURA,
                         c.C_CHITANTA,
                         c.C_DATA_DIFERENTA
                         ,P_ID = c.PERSOANE.P_ID
                         ,
                         P_NUME = c.PERSOANE.P_NUME
                         ,
                         P_PRENUME = c.PERSOANE.P_PRENUME
                         ,
                         P_TEL = c.PERSOANE.P_TEL
                     };

            var c2 = from c in db.CONTRACTEs
                     where c.C_AVANS2_DATA.HasValue && c.C_AVANS2.HasValue
                     select new
                     {
                         c.C_ID,
                         c.C_NUMAR,
                         c.C_DATA,
                         C_AVANS_DATA = c.C_AVANS2_DATA,
                         C_AVANS = c.C_AVANS2,
                         C_FACTURA = c.C_FACTURA2,
                         C_CHITANTA = c.C_CHITANTA2,
                         C_DATA_DIFERENTA = c.C_DATA_DIFERENTA2
                         ,
                         P_ID = c.PERSOANE.P_ID
                         ,
                         P_NUME = c.PERSOANE.P_NUME
                         ,
                         P_PRENUME = c.PERSOANE.P_PRENUME
                         ,
                         P_TEL = c.PERSOANE.P_TEL
                     };
            var c3 = from c in db.CONTRACTEs
                     where c.C_AVANS3_DATA.HasValue && c.C_AVANS3.HasValue
                     select new
                     {
                         c.C_ID,
                         c.C_NUMAR,
                         c.C_DATA,
                         C_AVANS_DATA = c.C_AVANS3_DATA,
                         C_AVANS = c.C_AVANS3,
                         C_FACTURA = c.C_FACTURA3,
                         C_CHITANTA = c.C_CHITANTA3,
                         C_DATA_DIFERENTA = c.C_DATA_DIFERENTA3
                         ,
                         P_ID = c.PERSOANE.P_ID 
                         ,
                         P_NUME = c.PERSOANE.P_NUME
                         ,
                         P_PRENUME = c.PERSOANE.P_PRENUME
                         ,
                         P_TEL = c.PERSOANE.P_TEL
                     };
            var contractes = c1.Union(c2).Union(c3);

            contractes = from cnt in contractes
                         where cnt.C_AVANS_DATA > (fromDate ?? DateTime.MinValue) && cnt.C_AVANS_DATA < (toDate ?? DateTime.MaxValue)
                         orderby cnt.C_AVANS_DATA
                         select cnt;
                         

            var sql = from c in contractes
                      join p in db.PERSOANEs on c.P_ID equals p.P_ID
                      select new
                      {
                          c.C_ID,
                          c.C_NUMAR,
                          c.C_DATA,
                          c.C_AVANS_DATA,
                          c.C_AVANS,
                          c.C_FACTURA,
                          c.C_CHITANTA,
                          c.C_DATA_DIFERENTA,
                          p.P_NUME,
                          p.P_PRENUME,
                          p.P_TEL
                      };
            #endregion                      

            var incasariModel = new List<IncasariList>();
            foreach (var inc in contractes)
            {
                incasariModel.Add(new IncasariList
                {
                    C_ID = inc.C_ID,
                    C_NUMAR = inc.C_NUMAR,
                    C_DATA = inc.C_DATA,
                    C_AVANS_DATA = inc.C_AVANS_DATA,
                    C_AVANS = inc.C_AVANS,
                    C_FACTURA = inc.C_FACTURA,
                    C_CHITANTA = inc.C_CHITANTA,
                    C_DATA_DIFERENTA = inc.C_DATA_DIFERENTA,
                    P_ID = inc.P_ID,
                    P_NUME = inc.P_NUME,
                    P_PRENUME = inc.P_PRENUME,
                    P_TEL = inc.P_TEL
                });
            }
            ViewBag.ListIncasari = incasariModel.Where(w=>w.C_AVANS.Value > 0);

            var total = contractes.Sum(i => i.C_AVANS);
            ViewBag.Total = total;
            return View(contractes.ToList());
        }


        //public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
        //{

        //    return from row in source
        //           from col in row.Select((x, i) => new KeyValuePair<int, T>(i, x))
        //           group col.Value by col.Key into c
        //           select c as IEnumerable<T>;
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}