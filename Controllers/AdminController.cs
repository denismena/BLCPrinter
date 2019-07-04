using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLCPrinter.Controllers
{
    public class AdminController : Controller
    {
        private BLCEntities db = new BLCEntities();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.LIBRARIEs.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            LIBRARIE librarie = db.LIBRARIEs.Find(id);
            if (librarie == null)
            {
                return HttpNotFound();
            }
            return View(librarie);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(LIBRARIE librarie)
        {
            if (ModelState.IsValid)
            {
                db.LIBRARIEs.Add(librarie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(librarie);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LIBRARIE librarie = db.LIBRARIEs.Find(id);
            if (librarie == null)
            {
                return HttpNotFound();
            }
            return View(librarie);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(LIBRARIE librarie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(librarie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(librarie);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LIBRARIE librarie = db.LIBRARIEs.Find(id);
            if (librarie == null)
            {
                return HttpNotFound();
            }
            return View(librarie);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LIBRARIE librarie = db.LIBRARIEs.Find(id);
            db.LIBRARIEs.Remove(librarie);
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