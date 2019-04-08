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
    public class ManagesBranchesController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: ManagesBranches
        public ActionResult Index()
        {
            var managesBranch = db.ManagesBranch.Include(m => m.Branch).Include(m => m.Employee);
            return View(managesBranch.ToList());
        }

        // GET: ManagesBranches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagesBranch managesBranch = db.ManagesBranch.Where(assigned => (assigned.managerId == id)).FirstOrDefault();
            if (managesBranch == null)
            {
                return HttpNotFound();
            }
            return View(managesBranch);
        }

        // GET: ManagesBranches/Create
        public ActionResult Create()
        {
            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName");
            ViewBag.managerId = new SelectList(db.Employee, "id", "firstName");
            return View();
        }

        // POST: ManagesBranches/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "managerId,startDate,branchId")] ManagesBranch managesBranch)
        {
            if (ModelState.IsValid)
            {
                db.ManagesBranch.Add(managesBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName", managesBranch.branchId);
            ViewBag.managerId = new SelectList(db.Employee, "id", "firstName", managesBranch.managerId);
            return View(managesBranch);
        }

        // GET: ManagesBranches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagesBranch managesBranch = db.ManagesBranch.Where(assigned => (assigned.managerId == id)).FirstOrDefault(); ;
            if (managesBranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName", managesBranch.branchId);
            ViewBag.managerId = new SelectList(db.Employee, "id", "firstName", managesBranch.managerId);
            return View(managesBranch);
        }

        // POST: ManagesBranches/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "managerId,startDate,branchId")] ManagesBranch managesBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managesBranch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branchId = new SelectList(db.Branch, "id", "branchName", managesBranch.branchId);
            ViewBag.managerId = new SelectList(db.Employee, "id", "firstName", managesBranch.managerId);
            return View(managesBranch);
        }

        // GET: ManagesBranches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagesBranch managesBranch = db.ManagesBranch.Where(assigned => (assigned.managerId == id)).FirstOrDefault();
            if (managesBranch == null)
            {
                return HttpNotFound();
            }
            return View(managesBranch);
        }

        // POST: ManagesBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManagesBranch managesBranch = db.ManagesBranch.Where(assigned => (assigned.managerId == id)).FirstOrDefault();
            db.ManagesBranch.Remove(managesBranch);
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
