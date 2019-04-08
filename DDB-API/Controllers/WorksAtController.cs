using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DDB_API.Models;

namespace DDB_API.Controllers
{
    public class WorksAtController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: WorksAt
        public ActionResult Index()
        {
            var worksAt = db.WorksAt.Include(w => w.Branch).Include(w => w.Employee);
            return View(worksAt.ToList());
        }

        // GET: WorksAt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksAt worksAt = db.WorksAt.Find(id);
            if (worksAt == null)
            {
                return HttpNotFound();
            }
            return View(worksAt);
        }

        // GET: WorksAt/Create
        public ActionResult Create()
        {
            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName");
            ViewBag.employeeId = new SelectList(db.Employee, "id", "firstName");
            return View();
        }

        // POST: WorksAt/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeId,branchId,department,position")] WorksAt worksAt)
        {
            if (ModelState.IsValid)
            {
                db.WorksAt.Add(worksAt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName", worksAt.branchId);
            ViewBag.employeeId = new SelectList(db.Employee, "id", "firstName", worksAt.employeeId);
            return View(worksAt);
        }

        // GET: WorksAt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksAt worksAt = db.WorksAt.Find(id);
            if (worksAt == null)
            {
                return HttpNotFound();
            }
            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName", worksAt.branchId);
            ViewBag.employeeId = new SelectList(db.Employee, "id", "firstName", worksAt.employeeId);
            return View(worksAt);
        }

        // POST: WorksAt/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employeeId,branchId,department,position")] WorksAt worksAt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worksAt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName", worksAt.branchId);
            ViewBag.employeeId = new SelectList(db.Employee, "id", "firstName", worksAt.employeeId);
            return View(worksAt);
        }

        // GET: WorksAt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksAt worksAt = db.WorksAt.Find(id);
            if (worksAt == null)
            {
                return HttpNotFound();
            }
            return View(worksAt);
        }

        // POST: WorksAt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorksAt worksAt = db.WorksAt.Find(id);
            db.WorksAt.Remove(worksAt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
