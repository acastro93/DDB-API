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
    public class AssignedToController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: AssignedTo
        public ActionResult Index()
        {
            var assignedTo = db.AssignedTo.Include(a => a.Asset).Include(a => a.Branch).Include(a => a.Employee);
            return View(assignedTo.ToList());
        }

        // GET: AssignedTo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedTo assignedTo = db.AssignedTo.Where(assigned => (assigned.employeeID == id)).FirstOrDefault();
            if (assignedTo == null)
            {
                return HttpNotFound();
            }
            return View(assignedTo);
        }

        // GET: AssignedTo/Create
        public ActionResult Create()
        {
            ViewBag.assetId = new SelectList(db.Asset, "id", "category");
            ViewBag.branchID = new SelectList(db.Branch, "id", "branchName");
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName");
            return View();
        }

        // POST: AssignedTo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "assetId,employeeID,assignationDate,branchID,assignationLocation")] AssignedTo assignedTo)
        {
            if (ModelState.IsValid)
            {
                db.AssignedTo.Add(assignedTo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.assetId = new SelectList(db.Asset, "id", "category", assignedTo.assetId);
            ViewBag.branchID = new SelectList(db.Branch, "id", "branchName", assignedTo.branchID);
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName", assignedTo.employeeID);
            return View(assignedTo);
        }

        // GET: AssignedTo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedTo assignedTo = db.AssignedTo.Where(assigned => (assigned.employeeID == id)).FirstOrDefault();
            if (assignedTo == null)
            {
                return HttpNotFound();
            }
            ViewBag.assetId = new SelectList(db.Asset, "id", "category", assignedTo.assetId);
            ViewBag.branchID = new SelectList(db.Branch, "id", "branchName", assignedTo.branchID);
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName", assignedTo.employeeID);
            return View(assignedTo);
        }

        // POST: AssignedTo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "assetId,employeeID,assignationDate,branchID,assignationLocation")] AssignedTo assignedTo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedTo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.assetId = new SelectList(db.Asset, "id", "category", assignedTo.assetId);
            ViewBag.branchID = new SelectList(db.Branch, "id", "branchName", assignedTo.branchID);
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName", assignedTo.employeeID);
            return View(assignedTo);
        }

        // GET: AssignedTo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedTo assignedTo = db.AssignedTo.Where(assigned => (assigned.employeeID == id)).FirstOrDefault();
            if (assignedTo == null)
            {
                return HttpNotFound();
            }
            return View(assignedTo);
        }

        // POST: AssignedTo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignedTo assignedTo = db.AssignedTo.Where(assigned => (assigned.employeeID == id)).FirstOrDefault();
            db.AssignedTo.Remove(assignedTo);
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
