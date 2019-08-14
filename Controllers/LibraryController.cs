using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLCPrinter.Controllers
{
    public class LibraryController : Controller
    {
        private BLCEntities1 db = new BLCEntities1();

        //
        // GET: /Library/

        public ActionResult Index()
        {
            return View(db.LIBRARIE.ToList().OrderBy(r=>r.L_TIP));
        }

        //
        // GET: /Library/Details/5

        public ActionResult Details(int id = 0)
        {
            LIBRARIE librarie = db.LIBRARIE.Find(id);
            if (librarie == null)
            {
                return HttpNotFound();
            }
            return View(librarie);
        }

        //
        // GET: /Library/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Library/Create

        [HttpPost]
        public ActionResult Create(LIBRARIE librarie)
        {
            if (ModelState.IsValid)
            {
                db.LIBRARIE.Add(librarie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(librarie);
        }

        //
        // GET: /Library/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LIBRARIE librarie = db.LIBRARIE.Find(id);
            if (librarie == null)
            {
                return HttpNotFound();
            }
            return View(librarie);
        }

        //
        // POST: /Library/Edit/5

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
        // GET: /Library/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LIBRARIE librarie = db.LIBRARIE.Find(id);
            if (librarie == null)
            {
                return HttpNotFound();
            }
            return View(librarie);
        }

        //
        // POST: /Library/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LIBRARIE librarie = db.LIBRARIE.Find(id);
            db.LIBRARIE.Remove(librarie);
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