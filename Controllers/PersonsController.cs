using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLCPrinter.Controllers
{
    public class PersonsController : Controller
    {
        private BLCEntities db = new BLCEntities();        
        //
        // GET: /Persons/

        public ActionResult Index()
        {
            var persoanes = db.PERSOANEs.Include(p => p.LIBRARIE);
            return View(persoanes.ToList());
        }

        //
        // GET: /Persons/Details/5

        public ActionResult Details(int id = 0)
        {
            PERSOANE persoane = db.PERSOANEs.Find(id);
            if (persoane == null)
            {
                return HttpNotFound();
            }
            return View(persoane);
        }

        //
        // GET: /Persons/Create

        public ActionResult Create()
        {
            var idType = from lib in db.LIBRARIEs
                         where lib.L_TIP == "ID"
                         select lib;
            ViewBag.P_ID_TYPE = new SelectList(idType, "L_ID", "L_NUME");
            return View();
        }

        //
        // POST: /Persons/Create

        [HttpPost]
        public ActionResult Create(PERSOANE persoane)
        {
            if (ModelState.IsValid)
            {
                db.PERSOANEs.Add(persoane);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.P_ID_TYPE = new SelectList(db.LIBRARIEs, "L_ID", "L_NUME", persoane.P_ID_TYPE);
            return View(persoane);
        }

        //
        // GET: /Persons/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PERSOANE persoane = db.PERSOANEs.Find(id);
            var idType = from lib in db.LIBRARIEs
                         where lib.L_TIP == "ID"
                         select lib;
            if (persoane == null)
            {
                return HttpNotFound();
            }
            ViewBag.P_ID_TYPE = new SelectList(idType, "L_ID", "L_NUME", persoane.P_ID_TYPE);
            return View(persoane);
        }

        //
        // POST: /Persons/Edit/5

        [HttpPost]
        public ActionResult Edit(PERSOANE persoane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persoane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.P_ID_TYPE = new SelectList(db.LIBRARIEs, "L_ID", "L_NUME", persoane.P_ID_TYPE);
            return View(persoane);
        }

        //
        // GET: /Persons/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PERSOANE persoane = db.PERSOANEs.Find(id);
            if (persoane == null)
            {
                return HttpNotFound();
            }
            return View(persoane);
        }

        //
        // POST: /Persons/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PERSOANE persoane = db.PERSOANEs.Find(id);
            db.PERSOANEs.Remove(persoane);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}