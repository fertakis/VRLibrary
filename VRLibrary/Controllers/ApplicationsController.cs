using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VRLibrary.Models;

namespace VRLibrary.Controllers
{
    public class ApplicationsController : Controller
    {
        private VRLibEntities db = new VRLibEntities();

        // GET: Applications
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.AspNetUser).Include(a => a.AspNetUser1).Include(a => a.Library);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.LibId = new SelectList(db.Libraries, "LibID", "Library_Name");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,LibId,AspNetUserId,DateOfApplication,DateOfVerification,AspNetLibrarianId")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", application.AspNetUserId);
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email", application.AspNetLibrarianId);
            ViewBag.LibId = new SelectList(db.Libraries, "LibID", "Library_Name", application.LibId);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", application.AspNetUserId);
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email", application.AspNetLibrarianId);
            ViewBag.LibId = new SelectList(db.Libraries, "LibID", "Library_Name", application.LibId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit([Bind(Include = "ApplicationId,LibId,AspNetUserId,DateOfApplication,DateOfVerification,AspNetLibrarianId")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", application.AspNetUserId);
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email", application.AspNetLibrarianId);
            ViewBag.LibId = new SelectList(db.Libraries, "LibID", "Library_Name", application.LibId);
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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



//SSSOOOOSSSS xreiazetai controller accept
//SSSOOOOSSSS xreiazetai controller edit & delete mono gia ka9e pendingUser
