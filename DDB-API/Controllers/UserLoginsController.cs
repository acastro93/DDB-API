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
    public class UserLoginsController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: UserLogins
        public ActionResult Index()
        {
            var userLogin = db.UserLogin.Include(u => u.Employee);
            return View(userLogin.ToList());
        }

        // GET: UserLogins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogin.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View(userLogin);
        }

        // GET: UserLogins/Create
        public ActionResult Create()
        {
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName");
            return View();
        }

        // POST: UserLogins/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,userPassword,employeeID,accessLevel")] UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                db.UserLogin.Add(userLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName", userLogin.employeeID);
            return View(userLogin);
        }

        // GET: UserLogins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogin.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName", userLogin.employeeID);
            return View(userLogin);
        }

        // POST: UserLogins/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,userPassword,employeeID,accessLevel")] UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeID = new SelectList(db.Employee, "id", "firstName", userLogin.employeeID);
            return View(userLogin);
        }

        // GET: UserLogins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogin.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View(userLogin);
        }

        // POST: UserLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserLogin userLogin = db.UserLogin.Find(id);
            db.UserLogin.Remove(userLogin);
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

        [HttpPost]
        public ActionResult CheckLogin(UserLogin user)
        {
            UserLogin userLogin = db.UserLogin.Where(login => (user.userPassword.Equals(login.userPassword)) && (user.username.Equals(login.username))).FirstOrDefault();

            if (userLogin == null)
            {
                ModelState.AddModelError("Error", "Incorrect Password and Username combination");
                return View("../Home/Index");
            }
            if (userLogin.accessLevel == 1)
            {
                return View("EmployeeView");
            }
            if (userLogin.accessLevel == 2)
            {
                return View("AdminView");
            }
            if (userLogin.accessLevel == 3)
            {
                return View("ManagerView");
            }
            return RedirectToAction("Index");
        }
    }
}
