﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VRLibrary.Models;
using Microsoft.AspNet.Identity;

namespace VRLibrary.Controllers
{
    public class BooksController : Controller
    {
        private VRLibEntities db = new VRLibEntities();

        // GET: Books
        [Authorize]
        [AllowAnonymous]
        public ActionResult Index(string bookSubject, string searchString)
        {

            var SubjectList = new List<string>();

            var SubjectQry = from j in db.Books
                             orderby j.Subject
                             select j.Subject;

            SubjectList.AddRange(SubjectQry.Distinct());

            ViewBag.bookSubject = new SelectList(SubjectList);


            var books = db.Books.Include(b => b.BookState1).Include(b => b.Library);
            

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            //ViewBag.UserName = new SelectList(db.AspNetUsers, "Id", "Library_Name");
            return View(books.ToList());
        }

        // GET: Books
        [Authorize]
        [AllowAnonymous]
        public ActionResult SearchBook(string bookSubject, string searchString)
        {

            var SubjectList = new List<string>();

            var SubjectQry = from j in db.Books
                             orderby j.Subject
                             select j.Subject;

            SubjectList.AddRange(SubjectQry.Distinct());

            ViewBag.bookSubject = new SelectList(SubjectList);


            //ViewBag.UserName = new SelectList(db.AspNetUsers, "Id", "Library_Name");
            return View();
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Create()
        {
            ViewBag.BookState = new SelectList(db.BookStates, "BookStateId", "State");
            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Create([Bind(Include = "BookID,Title,Author,Publisher,ISBN,LibID,Shelf,Subject,Rating,BookState,ImagePath,Description,NoOfRaters")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.AspNetUserId = User.Identity.GetUserId();
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookState = new SelectList(db.BookStates, "BookStateId", "State", book.BookState);
            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name", book.LibID);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookState = new SelectList(db.BookStates, "BookStateId", "State", book.BookState);
            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name", book.LibID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit([Bind(Include = "BookID,Title,Author,Publisher,ISBN,LibID,Shelf,Subject,Rating,BookState,ImagePath,Description,AspNetUserId,NoOfRaters")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookState = new SelectList(db.BookStates, "BookStateId", "State", book.BookState);
            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name", book.LibID);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
