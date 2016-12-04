using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class ReservationsController : Controller
    {
        private VRLibEntities db = new VRLibEntities();

        // GET: Reservations
        [Authorize(Roles = "Admin,Librarian")]

        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.AspNetUser).Include(r => r.Book);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize(Roles = "Admin,Librarian,Student")]
        public ActionResult Create()
        {
            Reservation model = new Reservation();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var name = currentUser.UserName.ToString();
            model.AspNetUserId = User.Identity.GetUserId();
            model.DateOfReservation = DateTime.Now;
            ViewBag.UserName = name;
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title");
            return View(model);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian,Student")]
        public ActionResult Create([Bind(Include = "ReservationsId,BookId,AspNetUserId,DateOfReservation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", reservation.AspNetUserId);
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title", reservation.BookId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5 
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit(int? id)
           
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", reservation.AspNetUserId);
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title", reservation.BookId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit([Bind(Include = "ReservationsId,BookId,AspNetUserId,DateOfReservation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", reservation.AspNetUserId);
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title", reservation.BookId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}


//SSSSOOOOOSSSS 2 controllers gia na mporei o STUDENT na kanei edit, delete, MONO to diko tou reservation!!!