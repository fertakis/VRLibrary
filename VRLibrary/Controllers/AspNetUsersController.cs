using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VRLibrary.Models;

namespace VRLibrary.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    public class AspNetUsersController : Controller
    {
        private VRLibEntities db = new VRLibEntities();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.Library);
            return View(aspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name");
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Name,Surname,LibID,StudentPass")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name", aspNetUser.LibID);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRoles = new SelectList(db.AspNetRoles.ToList(), "Name", "Name",
                aspNetUser.AspNetRoles.Any() ? aspNetUser.AspNetRoles.First().Name : string.Empty);
            ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name", aspNetUser.LibID);

            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Name,Surname,AspNetRoles,LibID,StudentPass")] AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserRoles = new SelectList(db.AspNetRoles, "Id", "Name",
                aspNetUser.AspNetRoles.Any() ? aspNetUser.AspNetRoles.First().Id : string.Empty);
                ViewBag.LibID = new SelectList(db.Libraries, "LibID", "Library_Name", aspNetUser.LibID);
                return View(aspNetUser);
            }

            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var user = await UserManager.FindByIdAsync(aspNetUser.Id);

            if (user == null)
            {
                return View(aspNetUser);
            }

            var roles = await UserManager.GetRolesAsync(user.Id);

            if (roles.Count > 0)
            {
                var roleResult = await UserManager.RemoveFromRoleAsync(user.Id,
                                roles.First());
            }         

            var addRoleResult = UserManager.AddToRole(user.Id, Request["UserRoles"].ToString());

            if (!addRoleResult.Succeeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            db.Entry(aspNetUser).State = EntityState.Modified;
            db.SaveChanges();
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
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
