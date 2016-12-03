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
    public class LoansController : Controller
    {
        private VRLibEntities db = new VRLibEntities();

        // GET: Loans
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.AspNetUser).Include(l => l.AspNetUser1).Include(l => l.Book).Include(l => l.Reservation);
            return View(loans.ToList());
        }

        // GET: Loans/Details/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title");
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationsId", "AspNetUserId");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Create([Bind(Include = "LoanId,AspNetUserId,BookId,AspNetLibrarianId,DateLended,DateReturned,DateToBeReturned,Extended,ReservationId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", loan.AspNetUserId);
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email", loan.AspNetLibrarianId);
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title", loan.BookId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationsId", "AspNetUserId", loan.ReservationId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", loan.AspNetUserId);
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email", loan.AspNetLibrarianId);
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title", loan.BookId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationsId", "AspNetUserId", loan.ReservationId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit([Bind(Include = "LoanId,AspNetUserId,BookId,AspNetLibrarianId,DateLended,DateReturned,DateToBeReturned,Extended,ReservationId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", loan.AspNetUserId);
            ViewBag.AspNetLibrarianId = new SelectList(db.AspNetUsers, "Id", "Email", loan.AspNetLibrarianId);
            ViewBag.BookId = new SelectList(db.Books, "BookID", "Title", loan.BookId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationsId", "AspNetUserId", loan.ReservationId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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


//!!!!!!!!!!!!!!sos controller gia na kanei o STUDENT EXTEND